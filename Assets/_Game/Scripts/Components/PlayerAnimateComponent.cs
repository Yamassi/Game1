using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimateComponent : AnimateComponent, IPlayerAnimate
{
    public override void AttackAnimate(bool isAttack)
    {
        if (isAttack && _isFirstAttack)
        {
            _animator.SetTrigger("Attack");
            _isFirstAttack = false;
            StartCoroutine(AttackSoundFX());
        }
        else if (isAttack && !_isFirstAttack)
        {
            _animator.SetTrigger("Attack2");
            _isFirstAttack = true;
            StartCoroutine(AttackSoundFX());
        }
    }
    private IEnumerator AttackSoundFX()
    {
        yield return new WaitForSeconds(0.2f);
        AudioController.Instance?.PlayOneShot(FMODEvents.Instance.Swish, transform.position);
    }

    public override void DieAnimate()
    {
        _animator.SetTrigger("Die");
    }
    public override void TakeDamageAnimate()
    {
        // AnimatorStateInfo stateInfo;
        // stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        // if (!stateInfo.IsName("GotHit"))
        _animator.SetTrigger("GotHit");
    }
    public void JumpAnimate(bool isJump)
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
