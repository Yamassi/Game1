using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IHealth _health;
    private IInput _input;
    private IMove _move;
    private IAnimate _animate;
    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _input = GetComponent<IInput>();
        _move = GetComponent<IMove>();
        _animate = GetComponent<IAnimate>();
    }
    private void Update()
    {
        Debug.Log(_input.GetDirection());
        _move.Look(_input.GetDirection());
        _animate.AttackAnimate(_input.GetAttack());
    }
    private void FixedUpdate()
    {
        _move.Jump(_input.GetJump());
        _move.Move(_input.GetDirection());
        _animate.SetMovementDirection(_input.GetDirection());
    }
}
