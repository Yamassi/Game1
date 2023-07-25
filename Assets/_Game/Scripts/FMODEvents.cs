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
    [field: Header("NPC Damage SFX")]
    [field: SerializeField] public EventReference NPCTakeDamage { get; private set; }
    [field: Header("Swish SFX")]
    [field: SerializeField] public EventReference Swish { get; private set; }
    [field: Header("Health Item SFX")]
    [field: SerializeField] public EventReference AddHealth { get; private set; }
    [field: Header("Wall Hit SFX")]
    [field: SerializeField] public EventReference WallHit { get; private set; }
    [field: Header("Footsteps SFX")]
    [field: SerializeField] public EventReference Footsteps { get; private set; }
    [field: Header("Music3")]
    [field: SerializeField] public EventReference Music3 { get; private set; }
    public static FMODEvents Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
}
