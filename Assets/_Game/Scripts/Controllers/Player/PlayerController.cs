using UnityEngine;
using FMODUnity;
public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem _fx;
    private IHealth _health;
    private IInput _input;
    private IMove _move;
    private IJump _jump;
    private IPlayerAnimate _animate;
    private BlinkFX[] _blinkFXs;
    private GroundChecker _groundChecker;
    private Rigidbody _rigidbody;
    private bool _isDie = false;
    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _input = GetComponent<IInput>();
        _move = GetComponent<IMove>();
        _jump = GetComponent<IJump>();
        _animate = GetComponent<IPlayerAnimate>();
        _rigidbody = GetComponent<Rigidbody>();
        _groundChecker = GetComponentInChildren<GroundChecker>();
        _blinkFXs = GetComponentsInChildren<BlinkFX>();
    }
    private void Update()
    {
        PlayerBehaviour();
    }
    private void PlayerBehaviour()
    {
        if (!_input.GetAttack())
            _move.Move(_input.GetDirection());

        _animate.JumpAnimate(_input.GetJump());
        _animate.SetMovementDirection(_input.GetDirection());
        _animate.AttackAnimate(_input.GetAttackTrigger());
    }
    private void FixedUpdate()
    {
        _jump.Jump(_input.GetJump());
        _jump.Gravity();
    }
    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);

        EventHolder.PlayerTakeDamage(damage);

        if (_health.GetCurrentHealth() <= 0)
        {
            _isDie = true;
            _animate.DieAnimate();
            EventHolder.PlayerDie();
            this.enabled = false;
        }
        _animate.TakeDamageAnimate();
        _fx.Play();
        AudioController.Instance.PlayOneShot(FMODEvents.Instance.PlayerTakeDamage, transform.position);
    }
    public void ReturnToGround()
    {
        Vector3 lastGroundedPosition = _groundChecker.GetLastGroundedPosition();
        Vector3 offset = new Vector3(0, 9, 0);
        transform.position = lastGroundedPosition + offset;

        foreach (var blinkFX in _blinkFXs)
        {
            blinkFX.InitBlinkFX();
        }
    }
    public void FootstepsFX()
    {
        // Debug.Log("Footsteps Sound Play");
        AudioController.Instance.PlayOneShot(FMODEvents.Instance.Footsteps, transform.position);
    }
}
