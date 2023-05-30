using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpComponent : MonoBehaviour, IJump
{
    [SerializeField] private float _jumpAmount = 20f, _gravityScale = 10f, _maxDuration = 0.3f, _fallingGravityScale = 20f;
    [Space]
    [Header("Information:")]
    [SerializeField] private float _currentGravityScale;
    [SerializeField] private float _jumpTime = 0;
    private Rigidbody _rigidbody;
    private GroundChecker _groundChecker;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundChecker = GetComponentInChildren<GroundChecker>();
        _currentGravityScale = _gravityScale;
    }
    public void Jump(bool isJump)
    {
        if (!_groundChecker.GetIsGrounded())
        {
            _jumpTime += Time.deltaTime;
        }
        else
        {
            _jumpTime = 0;
        }

        if (isJump && _jumpTime < _maxDuration)
        {
            Debug.Log("Jumping! ");
            _rigidbody.AddForce(Vector3.up * _jumpAmount, ForceMode.VelocityChange);
        }

        if (_rigidbody.velocity.y >= 0)
            _currentGravityScale = _gravityScale;
        else if (_rigidbody.velocity.y < 0)
            _currentGravityScale = _fallingGravityScale;
    }
    public void Gravity()
    {
        _rigidbody.AddForce(Physics.gravity * (_gravityScale - 1) * _rigidbody.mass);
    }
}
