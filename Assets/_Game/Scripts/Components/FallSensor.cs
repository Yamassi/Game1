using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            EventHolder.PlayerFall();
            Debug.Log("Player Fall");
            AudioController.Instance.PlayOneShot(FMODEvents.Instance.FallHit, transform.position);
        }
    }
}
