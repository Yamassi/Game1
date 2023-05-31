using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputComponent : MonoBehaviour, IInput
{
    [SerializeField] private Vector2 _movementDirection;
    [SerializeField] private bool _isAttack;
    [SerializeField] private bool _isJump;
    private PlayerInput _playerInput;
    private float _jumpTime, _jumpMaxDuration = 0.3f;
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
        _movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     _isJump = true;

        // }
        // else
        // {
        //     _isJump = false;
        // }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJump = true;
            _jumpTime = 0;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || _jumpTime > _jumpMaxDuration)
        {
            _isJump = false;
        }
        if (_isJump)
            _jumpTime += Time.deltaTime;
    }
    private void JumpFromTouch()
    {
        // if (_playerInput.actions["Jump"].IsPressed())
        // {
        //     _isJump = true;
        // }
        // else if (_playerInput.actions["Jump"].WasReleasedThisFrame())
        // {
        //     _isJump = false;
        // }
        if (_playerInput.actions["Jump"].IsPressed())
        {
            _isJump = true;
            _jumpTime = 0;
        }
        else if (_playerInput.actions["Jump"].WasReleasedThisFrame() || _jumpTime > _jumpMaxDuration)
        {
            _isJump = false;
        }
        if (_isJump)
            _jumpTime += Time.deltaTime;
    }
}
