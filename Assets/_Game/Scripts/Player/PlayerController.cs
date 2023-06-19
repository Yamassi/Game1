using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxLife;
    [SerializeField] private ParticleSystem _fx;
    private IHealth _health;
    private IInput _input;
    private IMove _move;
    private IJump _jump;
    private IAnimate _animate;
    private GroundChecker _groundChecker;
    private Rigidbody _rigidbody;
    private int _currentLife;
    private bool _isDie = false;
    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _input = GetComponent<IInput>();
        _move = GetComponent<IMove>();
        _jump = GetComponent<IJump>();
        _animate = GetComponent<IAnimate>();
        _rigidbody = GetComponent<Rigidbody>();
        _groundChecker = GetComponentInChildren<GroundChecker>();

        _currentLife = _maxLife;
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
        _currentLife -= damage;
        EventHolder.PlayerTakeDamage(damage);

        if (_currentLife <= 0)
        {
            _isDie = true;
            _animate.DieAnimate();
            EventHolder.PlayerDie();
        }
        _animate.TakeDamageAnimate();
        _fx.Play();
    }
    public void ReturnToGround()
    {
        Vector3 lastGroundedPosition = _groundChecker.GetLastGroundedPosition();
        Vector3 offset = new Vector3(0, 12, 1);
        transform.position = lastGroundedPosition + offset;
    }
    public int GetMaxLife()
    {
        return _maxLife;
    }
}
