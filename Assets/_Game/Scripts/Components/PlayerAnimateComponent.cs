using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimateComponent : AnimateComponent, IAnimate
{
    public override void AttackAnimate(bool isAttack)
    {
        if (isAttack && _isAttackFirst)
        {
            _animator.SetTrigger("Attack");
            _isAttackFirst = false;
        }
        else if (isAttack && !_isAttackFirst)
        {
            _animator.SetTrigger("Attack2");
            _isAttackFirst = true;
        }
    }
    public override void DieAnimate()
    {
        _animator.SetTrigger("Die");
    }
    public override void JumpAnimate(bool isJump)
    {
        if (isJump && !_isJumpAnimationPlaying)
        {
            _animator.SetTrigger("Jump");
            _isJumpAnimationPlaying = true;
            StartCoroutine(EnableJumpCoroutine(0.5f));
        }
    }
    private IEnumerator EnableJumpCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _isJumpAnimationPlaying = false;
    }
}
