using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputComponent : MonoBehaviour, IInput
{
    [SerializeField] private Vector2 _movementDirection;
    [SerializeField] private bool _isAttackTrigger = false, _isAttack = false;
    [SerializeField] private bool _isJump = false;
    private PlayerInput _playerInput;
    private float _jumpTime, _jumpMaxDuration = 0.3f;
    private float _attackTime = 0f, _attackCooldown = 0.8f;
    private bool _isJumpOver = true, _isAttackOver = true;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
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
        _movementDirection = _playerInput.actions["Move"].ReadValue<Vector2>();
        _movementDirection.Normalize();
    }
    public bool GetAttack()
    {
        return _isAttack;
    }
    public bool GetAttackTrigger()
    {
#if UNITY_EDITOR
        AttackFromKeyboard();
#endif
#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IOS && !UNITY_EDITOR
        AttackFromTouch();
#endif
        return _isAttackTrigger;
    }
    private void AttackFromKeyboard()
    {
        if (!_isAttack)
        {
            if (Input.GetMouseButton(0))
            {
                _isAttack = true;
                _isAttackTrigger = true;
                _attackTime = _attackCooldown;
            }
        }
        else if (_isAttack)
        {
            if (_attackTime > 0)
            {
                _isAttackTrigger = false;
                _attackTime -= Time.deltaTime;
            }
            else if (_attackTime <= 0)
            {
                _isAttack = false;
            }
        }
    }
    private void AttackFromTouch()
    {
        if (!_isAttack)
        {
            if (_playerInput.actions["Attack"].IsPressed())
            {
                _isAttack = true;
                _isAttackTrigger = true;
                _attackTime = _attackCooldown;
            }
        }
        else if (_isAttack)
        {
            if (_attackTime > 0)
            {
                _isAttackTrigger = false;
                _attackTime -= Time.deltaTime;
            }
            else if (_attackTime <= 0)
            {
                _isAttack = false;
            }
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
        if (Input.GetKey(KeyCode.Space) && !_isJump && _isJumpOver)
        {
            _isJumpOver = false;
            _isJump = true;
            _jumpTime = 0;
        }
        else if (_jumpTime > _jumpMaxDuration)
        {
            _isJump = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
            _isJumpOver = true;

        if (_isJump)
            _jumpTime += Time.deltaTime;
    }
    private void JumpFromTouch()
    {
        if (_playerInput.actions["Jump"].IsPressed() && !_isJump && _isJumpOver)
        {
            _isJumpOver = false;
            _isJump = true;
            _jumpTime = 0;
        }
        else if (_jumpTime > _jumpMaxDuration)
        {
            _isJump = false;
        }

        if (_playerInput.actions["Jump"].WasReleasedThisFrame())
            _isJumpOver = true;

        if (_isJump)
            _jumpTime += Time.deltaTime;
    }
}
