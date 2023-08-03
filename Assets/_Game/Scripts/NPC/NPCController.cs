using System.Collections.Generic;
using UnityEngine;
public class NPCController : MonoBehaviour, IDamageable
{
    [SerializeField] private float _attackCoolDownTime, _attackDelay;
    [SerializeField] private ParticleSystem _fx;
    [SerializeField] private Transform _gfx;
    [SerializeField] private Light _eyeLight;
    private CapsuleCollider _capsule;
    private PlayerController _target;
    private INavMeshMove _navMeshMove;
    private BlinkFX[] _blinkFXs;
    private IHealth _health;
    private IAnimate _animate;
    private ISensor _sensor;
    private WeaponComponent _weapon;
    private Vector2 _targetLastPosition;
    private bool _isDie = false;

    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _animate = GetComponent<IAnimate>();
        _sensor = GetComponentInChildren<ISensor>();
        _navMeshMove = GetComponent<INavMeshMove>();
        _weapon = GetComponent<WeaponComponent>();
        _capsule = GetComponent<CapsuleCollider>();
        _blinkFXs = GetComponentsInChildren<BlinkFX>();
    }
    private void Update()
    {
        if (!_isDie)
            NPCsBehaviour();
    }
    private void NPCsBehaviour()
    {
        _target = _sensor.GetChaser();
        if (_target != null)
        {
            Chase();
        }
        else
        {
            _attackDelay = 0.6f;
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
    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);

        if (_health.GetCurrentHealth() <= 0)
        {
            Die();
        }
        if (!_isDie)
            _animate.TakeDamageAnimate();

        _fx.Emit(1);
        AudioController.Instance.PlayOneShot(FMODEvents.Instance.NPCTakeDamage, transform.position);
    }

    private void Die()
    {
        _isDie = true;
        _animate.DieAnimate();
        _capsule.enabled = false;
        _eyeLight.gameObject.SetActive(false);
        _weapon.EndAttack();
        _navMeshMove.StopMove();
        _gfx.SetParent(null);

        Reward reward = GetComponent<Reward>();
        if (reward != null)
            reward.DropItem();

        _weapon.DisableWeapon();

        EventHolder.NPCDie();

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