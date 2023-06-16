using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NPCAnimateComponent : AnimateComponent, IAnimate
{
    public override void AttackAnimate(bool isAttack)
    {
        if (isAttack)
        {
            _animator.SetTrigger("Attack");
        }
    }
    public override void DieAnimate()
    {
        _animator.SetTrigger("Die");
    }
    public override void GotHitAnimate()
    {
        _animator.SetTrigger("GotHit");
    }
    public override void JumpAnimate(bool isJump)
    {

    }
}
