using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NPCAnimateComponent : AnimateComponent, INPCAnimate
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
    public override void TakeDamageAnimate()
    {
        _animator.SetTrigger("GotHit");
    }
    public void BlockAnimate()
    {
        _animator.SetTrigger("BlockHit");
    }
}
