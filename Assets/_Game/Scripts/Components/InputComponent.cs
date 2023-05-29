using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputComponent : MonoBehaviour, IInput
{
    [SerializeField] private Vector2 _movementDirection;
    [SerializeField] private bool _isAttack;
    private PlayerInput _playerInput;
    public Vector2 GetDirection()
    {
#if UNITY_EDITOR
        DirectionFromKeyboard();
#endif
#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IOS && !UNITY_EDITOR
        DirectionFromTouch();
#endif
        return _movementDirection;
    }
    private void DirectionFromKeyboard()
    {
        _movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _movementDirection.Normalize();
    }
    private void DirectionFromTouch()
    {
        _movementDirection = _playerInput.actions["Move"].ReadValue<Vector3>();
        _movementDirection.Normalize();
    }
    public bool GetAttack()
    {
#if UNITY_EDITOR
        AttackFromKeyboard();
#endif
#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IOS && !UNITY_EDITOR
        AttackFromTouch();
#endif
        return _isAttack;
    }
    private void AttackFromKeyboard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isAttack = true;
        }
        else
        {
            _isAttack = false;
        }
    }
    private void AttackFromTouch()
    {
        if (_playerInput.actions["Attack"].IsPressed())
        {
            _isAttack = true;
        }
        if (_playerInput.actions["Attack"].WasReleasedThisFrame())
        {
            _isAttack = false;
        }
    }
}
