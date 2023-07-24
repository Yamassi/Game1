using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Intro")]
    [field: SerializeField] public EventReference Intro { get; private set; }
    [field: Header("Player Damage SFX")]
    [field: SerializeField] public EventReference PlayerTakeDamage { get; private set; }
    public static FMODEvents Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
}
