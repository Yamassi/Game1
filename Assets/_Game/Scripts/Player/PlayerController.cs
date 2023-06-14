using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxLife;
    private IHealth _health;
    private IInput _input;
    private IMove _move;
    private IJump _jump;
    private IAnimate _animate;
    private int _currentLife;
    private bool _isDie = false;
    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _input = GetComponent<IInput>();
        _move = GetComponent<IMove>();
        _jump = GetComponent<IJump>();
        _animate = GetComponent<IAnimate>();

        _currentLife = _maxLife;
    }
    private void Update()
    {
        if (!_isDie)
            PlayerBehaviour();
    }

    private void PlayerBehaviour()
    {
        if (!_input.GetAttack())
            _move.Move(_input.GetDirection());

        _animate.AttackAnimate(_input.GetAttackTrigger());
        _animate.JumpAnimate(_input.GetJump());
        _animate.SetMovementDirection(_input.GetDirection());
    }

    private void FixedUpdate()
    {
        _jump.Jump(_input.GetJump());
        _jump.Gravity();
    }

    public void TakeDamage(int damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            _isDie = true;
            _animate.DieAnimate();
        }
    }
}
