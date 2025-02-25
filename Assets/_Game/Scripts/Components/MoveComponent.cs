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
    private void LookDirection()
    {
        if (_isoDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(_isoDirection, Vector3.up);
            transform.localRotation = Quaternion.RotateTowards(transform.rotation, rotation, _characterRotationSpeed);
        }
    }
    public void Move(Vector2 movementDirection)
    {
        _isoDirection = new Vector3(movementDirection.x, 0, movementDirection.y).ToIso();

        _movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0f, 1f);

        transform.Translate(new Vector3(_isoDirection.x, 0, _isoDirection.z)
            * _movementSpeed * _characterMovementSpeed * Time.deltaTime, Space.World);

        LookDirection();

    }
}
