using System.Collections.Generic;
using UnityEngine;
public class NPCController : MonoBehaviour, IDamageable
{
    [SerializeField] private float _attackCoolDownTime, _attackDelay, _blockCoolDownTime;
    [SerializeField] private ParticleSystem _fx, _fx2;
    [SerializeField] private Transform _gfx;
    [SerializeField] private Light _eyeLight;
    private CapsuleCollider _capsule;
    private PlayerController _target;
    private INavMeshMove _navMeshMove;
    private BlinkFX[] _blinkFXs;
    private IHealth _health;
    private INPCAnimate _animate;
    private ISensor _sensor;
    private WeaponComponent _weapon;
    private Vector2 _targetLastPosition;
    private bool _isDie = false;
    private float _blockTimer = 0, _waitDistance;
    [field: SerializeField] public NPCBehaviour Behaviour { get; private set; } = 0;
    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _animate = GetComponent<INPCAnimate>();
        _sensor = GetComponentInChildren<ISensor>();
        _navMeshMove = GetComponent<INavMeshMove>();
        _weapon = GetComponent<WeaponComponent>();
        _capsule = GetComponent<CapsuleCollider>();
        _blinkFXs = GetComponentsInChildren<BlinkFX>();
        _waitDistance = Random.Range(1.5f, 3.1f);
    }
    private void Update()
    {
        _blockTimer += Time.deltaTime;

        if (!_isDie)
            NPCsBehaviour();
    }
    private void NPCsBehaviour()
    {
        _target = _sensor.GetChaser();
        if (_target != null)
        {
            if (Behaviour == NPCBehaviour.Wait)
                Wait();

            if (Behaviour == NPCBehaviour.Attack)
                Chase();
        }
        else
        {
            _attackDelay = 0.6f;
        }
    }
    private void RunAway()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);

        _navMeshMove.RotateToTarget(_target.transform.position);

        if (distance <= 2)
        {
            _navMeshMove.JumpBack(_target.transform);
        }
        else if (distance > 4)
        {
            Behaviour = NPCBehaviour.Wait;
        }
    }

    private void Wait()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        if (distance > _waitDistance)
        {
            _navMeshMove.Chase(_target.transform);
        }
        else if (distance <= _waitDistance)
        {
            _navMeshMove.StopMove();
            _navMeshMove.RotateToTarget(_target.transform.position);

            _attackDelay -= Time.deltaTime;

            if (distance <= 1.2)
            {
                if (_attackDelay <= 0)
                {
                    _animate.AttackAnimate(true);
                    _attackDelay = _attackCoolDownTime;
                }
            }
        }
    }
    private void Chase()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        if (distance > 1.2)
        {
            _navMeshMove.Chase(_target.transform);
        }
        else if (distance <= 1.2)
        {
            _navMeshMove.StopMove();
            _navMeshMove.RotateToTarget(_target.transform.position);

            _attackDelay -= Time.deltaTime;

            if (_attackDelay <= 0)
            {
                _animate.AttackAnimate(true);
                _attackDelay = _attackCoolDownTime;
            }
        }
    }
    public void ChangeBehaviour(NPCBehaviour behaviour)
    {
        Behaviour = behaviour;
    }
    public void TakeDamage(int damage)
    {
        int randomNumber;
        if (_blockTimer > _blockCoolDownTime)
        {
            randomNumber = Random.Range(0, 2);
            _blockTimer = 0;
        }
        else
            randomNumber = 0;

        switch (randomNumber)
        {
            case 0:
                _health.TakeDamage(damage);

                EventHolder.NPCTakeDamage(this);

                if (_health.GetCurrentHealth() <= 0)
                {
                    Die();
                }
                if (!_isDie)
                    _animate.TakeDamageAnimate();

                _fx.Emit(1);
                AudioController.Instance.PlayOneShot(FMODEvents.Instance.NPCTakeDamage, transform.position);
                break;
            case 1:
                _animate.BlockAnimate();
                _fx2.Emit(10);
                AudioController.Instance.PlayOneShot(FMODEvents.Instance.NPCBlock, transform.position);
                break;
        }
    }

    private void Die()
    {
        _isDie = true;
        _animate.DieAnimate();
        _capsule.enabled = false;

        if (_eyeLight != null)
            _eyeLight.gameObject.SetActive(false);

        _weapon.EndAttack();
        _navMeshMove.StopMove();
        _gfx.SetParent(null);

        Reward reward = GetComponent<Reward>();
        if (reward != null)
            reward.DropItem();

        _weapon.DisableWeapon();

        EventHolder.NPCDie(this);

        if (_blinkFXs != null)
        {
            foreach (var blinkFX in _blinkFXs)
            {
                blinkFX.InitBlinkFX();
            }
        }

        Destroy(_gfx.gameObject, 1.3f);
        Destroy(this.gameObject, 1.3f);
    }
}
public enum NPCBehaviour
{
    Wait,
    Attack,
    RunAway,
}