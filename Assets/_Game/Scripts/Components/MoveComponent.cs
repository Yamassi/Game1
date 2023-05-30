using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class MoveComponent : MonoBehaviour, IMove
{
    [SerializeField] private float _characterMovementSpeed;
    [SerializeField] private float _characterRotationSpeed;
    [Space]
    [Header("Jump:")]
    [Range(1, 3)]
    [SerializeField] private float _jumpHeight;
    [Range(1, 5)]
    [SerializeField] private float _jumpSpeed;
    [Space]
    [Header("Information:")]
    [SerializeField] private float _movementSpeed;
    private Rigidbody _rigidbody;
    private Vector3 _isoDirection;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }
    public void Look(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            _isoDirection = new Vector3(
    movementDirection.x, movementDirection.y, movementDirection.y).ToIso();

            var relative = (transform.position + _isoDirection) - transform.position;
            var rotation = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _characterRotationSpeed);
        }
    }
    public void Move(Vector3 movementDirection)
    {
        _movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0f, 1f);

        _rigidbody.velocity = _isoDirection * _movementSpeed * _characterMovementSpeed;

    }
    public void Jump(bool isJump)
    {
        if (isJump)
            _rigidbody.AddForce(new Vector3(0, _jumpHeight, 0) * _jumpSpeed, ForceMode.Impulse);
    }
}
