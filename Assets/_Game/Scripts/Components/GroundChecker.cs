using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private CapsuleCollider _capsuleCollider;
    private bool _isGrounded;
    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _isGrounded = false;
    }
    public bool GetIsGrounded()
    {
        return _isGrounded;
    }
}
