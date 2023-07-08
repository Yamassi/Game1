using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private LightDelay _lightDelay;
    private void Awake()
    {
        EventHolder.OnWaveStart += CheckWave;
    }

    private void CheckWave(int waveNumber)
    {
        Debug.Log(waveNumber);
        if (waveNumber == 2)
            _lightDelay.gameObject.SetActive(true);

    }
}
