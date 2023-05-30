using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputComponent : MonoBehaviour, IInput
{
    [SerializeField] private Vector3 _movementDirection;
    [SerializeField] private bool _isAttack;
    [SerializeField] private bool _isJump;
    private PlayerInput _playerInput;
    public Vector3 GetDirection()
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
        _movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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
    public bool GetJump()
    {
#if UNITY_EDITOR
        JumpFromKeyboard();
#endif
#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IOS && !UNITY_EDITOR
        JumpFromTouch();
#endif
        return _isJump;
    }

    private void JumpFromKeyboard()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _isJump = true;
            Debug.Log("Jump2");
        }
        else
        {
            _isJump = false;
        }
    }
    private void JumpFromTouch()
    {
        if (_playerInput.actions["Jump"].IsPressed())
        {
            _isJump = true;
        }
        if (_playerInput.actions["Jump"].WasReleasedThisFrame())
        {
            _isJump = false;
        }
    }
}
