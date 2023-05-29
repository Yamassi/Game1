using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputComponent : MonoBehaviour, IInput
{
    [SerializeField] private Vector2 _movementDirection;
    // [SerializeField] private bool _isFiring;
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
    //     public bool GetFire()
    //     {
    // #if UNITY_EDITOR
    //         FireFromKeyboard();
    // #endif
    // #if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IOS && !UNITY_EDITOR
    //         FireFromTouch();
    // #endif
    //         return _isFiring;
    //     }
    // private void FireFromKeyboard()
    // {
    //     if (Input.GetMouseButton(0))
    //     {
    //         _isFiring = true;
    //     }
    //     else
    //     {
    //         _isFiring = false;
    //     }
    // }
    // private void FireFromTouch()
    // {
    //     if (_playerInput.actions["Fire"].IsPressed())
    //     {
    //         _isFiring = true;
    //     }
    //     if (_playerInput.actions["Fire"].WasReleasedThisFrame())
    //     {
    //         _isFiring = false;
    //     }
    // }
}
