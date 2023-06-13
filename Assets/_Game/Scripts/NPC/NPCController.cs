using UnityEngine;


public class NPCController : MonoBehaviour
{
    private PlayerController _target;
    private INavMeshMove _navMeshMove;
    private IHealth _health;
    private IAnimate _animate;
    private ISensor _sensor;
    private Vector2 _targetLastPosition;

    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _animate = GetComponent<IAnimate>();
        _sensor = GetComponentInChildren<ISensor>();
        _navMeshMove = GetComponent<INavMeshMove>();
    }
    private void Update()
    {
        NPCsBehaviour();
    }
    private void NPCsBehaviour()
    {
        _target = _sensor.GetChaser();
        if (_target != null)
        {
            Chase();
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
            _navMeshMove.RotateToTarget(_target.transform.position);
            _navMeshMove.StopMove();
        }
    }
}
