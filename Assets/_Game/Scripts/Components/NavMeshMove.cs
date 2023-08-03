using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMove : MonoBehaviour, INavMeshMove
{
    [SerializeField] private float _displacementDist = 3f;
    private NavMeshAgent _agent;
    private NavMeshObstacle _obstacle;
    private float _randomAngle;
    private float _randomAngleCoolDown = 5f;
    private float _randomAngleTimer;
    private bool _isFirstChase = true;
    private bool _isStopped = true;
    private float _randomPositionTimer;
    private float _randomPositionCoolDown;
    private bool _isResetRandomPosition = true;
    private bool _isWalk = true;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _obstacle = GetComponent<NavMeshObstacle>();
        _obstacle.enabled = false;
    }
    public void StopMove()
    {
        _agent.enabled = false;
        _obstacle.enabled = true;
        // _agent.ResetPath();
    }
    public bool GetIsStopped()
    {
        if (_agent.remainingDistance > 0)
            return _isStopped = false;
        else
            return _isStopped = true;
    }
    public void Walk()
    {
        if (GetIsStopped())
        {
            GetPosition();
        }
    }
    public void Chase(Transform targetPosition)
    {
        _obstacle.enabled = false;
        _agent.enabled = true;
        _isWalk = false;
        if (targetPosition == null)
        {
            _isFirstChase = true;
            return;
        }
        MoveToPosition(targetPosition.position);
    }
    public void RotateToTarget(Vector3 targetPos)
    {
        var rotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
    }
    public void RunAway(Transform chaserPosition)
    {
        _isWalk = false;
        if (chaserPosition == null)
        {
            _randomAngleTimer = 0;
            _isFirstChase = true;
            return;
        }
        GetAngle();

        Vector3 normDirection = (chaserPosition.position - transform.position).normalized;
        normDirection = Quaternion.AngleAxis(_randomAngle, Vector3.up) * normDirection;
        MoveToPosition(transform.position - (normDirection * _displacementDist));
    }
    private void GetAngle()
    {
        if (_isFirstChase)
        {
            GetRandomAngle();

            _isFirstChase = false;
        }
        else
        {
            if (_randomAngleTimer > _randomAngleCoolDown)
            {
                GetRandomAngle();
                _randomAngleTimer = 0;
            }
        }
        _randomAngleTimer += Time.deltaTime;
    }
    private void GetRandomAngle()
    {
        _randomAngle = Random.Range(-89, 91);
    }
    private void GetPosition()
    {
        if (_isResetRandomPosition)
        {
            _randomPositionCoolDown = Random.Range(1, 5);
            MoveToPosition(GetRandomPosition());
            _isResetRandomPosition = false;
            _isWalk = true;
        }
        else
        {
            if (_randomPositionTimer > _randomPositionCoolDown)
            {
                _isResetRandomPosition = true;
                _randomPositionTimer = 0;
            }
        }
        _randomPositionTimer += Time.deltaTime;
    }
    private Vector3 GetRandomPosition()
    {
        return new Vector3(transform.position.x + (Random.Range(-1f, 1f)), 0, transform.position.z + (Random.Range(-1f, 1f)));
    }
    private void MoveToPosition(Vector3 position)
    {
        _agent.SetDestination(position);
        _agent.isStopped = false;
    }
    public void RunToggle(bool isRun)
    {
        if (isRun)
        {
            _agent.speed = 3;
            _agent.angularSpeed = 1200;
            _agent.acceleration = 8;
        }
        else
        {
            _agent.speed = 1;
            _agent.angularSpeed = 700;
            _agent.acceleration = 3;
        }

    }
    public bool GetIsWalk()
    {
        return _isWalk;
    }
}
