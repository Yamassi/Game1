using UnityEngine;
public class MoveComponent : MonoBehaviour, IMove
{
    [SerializeField] private float _characterMovementSpeed, _characterRotationSpeed;
    [Space]
    [Header("Information:")]
    [SerializeField] private float _movementSpeed;
    private Rigidbody _rigidbody;
    private Vector3 _isoDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Look(Vector2 movementDirection)
    {
        _isoDirection = new Vector3(movementDirection.x, 0, movementDirection.y).ToIso();

        if (movementDirection != Vector2.zero)
        {
            var relative = (transform.position + _isoDirection) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _characterRotationSpeed);
        }
    }
    public void Move(Vector2 movementDirection)
    {
        _movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0f, 1f);
        _rigidbody.velocity = _isoDirection * _movementSpeed * _characterMovementSpeed;
    }
}
