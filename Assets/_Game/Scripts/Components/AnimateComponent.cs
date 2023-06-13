using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class AnimateComponent : MonoBehaviour, IAnimate
{
    protected Animator _animator;
    protected bool _isJumpAnimationPlaying = false;
    protected bool _isAttackFirst = true;
    protected void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    protected void OnDisable()
    {
        StopAllCoroutines();
    }
    public void SetMovementDirection(Vector2 movementDirection)
    {
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        _animator.SetFloat("InputMagnitude", inputMagnitude);
    }
    public abstract void AttackAnimate(bool isAttack);
    public abstract void JumpAnimate(bool isJump);
}
