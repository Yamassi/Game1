using UnityEngine;


public class NPCController : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxLife;
    [SerializeField] private float _attackCoolDownTime;
    private PlayerController _target;
    private INavMeshMove _navMeshMove;
    private IHealth _health;
    private IAnimate _animate;
    private ISensor _sensor;
    private Vector2 _targetLastPosition;
    private int _currentLife;
    private float _attackCoolDown;
    private bool _isDie = false;

    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _animate = GetComponent<IAnimate>();
        _sensor = GetComponentInChildren<ISensor>();
        _navMeshMove = GetComponent<INavMeshMove>();

        _currentLife = _maxLife;
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
            _attackCoolDown = 0.6f;
        }
    }
    private void Chase()
    {
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        if (distance > 1.5)
        {
            _navMeshMove.Chase(_target.transform);
        }
        else if (distance < 1.5)
        {
            _navMeshMove.StopMove();
            _navMeshMove.RotateToTarget(_target.transform.position);

            _attackCoolDown -= Time.deltaTime;

            if (_attackCoolDown <= 0)
            {
                _animate.AttackAnimate(true);
                _attackCoolDown = _attackCoolDownTime;
            }

        }
    }
    public void TakeDamage(int damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            _isDie = true;
            _animate.DieAnimate();
        }
    }
}
