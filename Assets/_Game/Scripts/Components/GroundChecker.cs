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
    private void OnTriggerStay(Collider other)
    {
        _isGrounded = true;
        _lastGroundedPosition = other.transform.position;
    }
    private void OnTriggerExit(Collider other)
    {
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
