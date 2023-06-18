using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpComponent : MonoBehaviour, IJump
{
    [SerializeField] private float _jumpAmount = 20f, _gravityScale = 10f, _fallingGravityScale = 20f, _coyoteTime = 0.2f;
    [Space]
    [Header("Information:")]
    [SerializeField] private float _currentGravityScale;
    private float _coyoteTimeCounter;
    private Rigidbody _rigidbody;
    private GroundChecker _groundChecker;
    private bool _isJumping = false, _isJumpFirstFrame = true;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundChecker = GetComponentInChildren<GroundChecker>();
        _currentGravityScale = _gravityScale;
    }
    public void Jump(bool isJumpPressed)
    {
        if (_groundChecker.GetIsGrounded())
        {
            _coyoteTimeCounter = _coyoteTime;
            _isJumping = false;
            _isJumpFirstFrame = true;
        }
        else
        {
            _coyoteTimeCounter -= Time.fixedDeltaTime;
            _isJumping = true;
        }

        if (isJumpPressed && _coyoteTimeCounter > 0f && _isJumping)
        {
            AddForce();
        }
        else if (isJumpPressed && _isJumpFirstFrame)
        {
            AddForce();
            _isJumpFirstFrame = false;
        }

        // if (_groundChecker.GetIsGrounded())
        // {
        //     _coyoteTimeCounter = _coyoteTime;
        //     _isJumping = false;
        // }
        // else
        // {
        //     _coyoteTimeCounter -= Time.fixedDeltaTime;
        //     _isJumping = true;
        // }

        // if (isJumpPressed && _coyoteTimeCounter > 0f)
        // {
        //     AddForce();
        // }

        if (_rigidbody.velocity.y >= 0)
            _currentGravityScale = _gravityScale;
        else if (_rigidbody.velocity.y < 0)
            _currentGravityScale = _fallingGravityScale;
    }
    private void AddForce()
    {
        _rigidbody.AddForce(Vector3.up * _jumpAmount, ForceMode.Impulse);
    }
    public void Gravity()
    {
        _rigidbody.AddForce(Physics.gravity * (_gravityScale - 1) * _rigidbody.mass);
    }
}
