using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimateComponent : MonoBehaviour, IAnimate
{
    private Animator _animator;
    private bool _isJumpAnimationPlaying = false;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void SetMovementDirection(Vector2 movementDirection)
    {
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        _animator.SetFloat("InputMagnitude", inputMagnitude);
    }
    public void AttackAnimate(bool isAttack)
    {
        if (isAttack)
            _animator.SetTrigger("Attack");
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
