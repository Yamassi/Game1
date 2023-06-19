using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private bool _isGrounded;
    private BoxCollider _boxCollider;
    private Vector3 _lastGroundedPosition;
    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _isGrounded = true;
            _lastGroundedPosition = other.transform.position;
        }

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
    public Vector3 GetLastGroundedPosition()
    {
        return _lastGroundedPosition;
    }
}
