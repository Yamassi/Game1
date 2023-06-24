using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInitializer : MonoBehaviour
{
    [SerializeField] private GenericFactory[] _factories;
    [SerializeField] private SpawnWave[] _spawnWaves;
    [SerializeField] private Transform _NPCParent;
    public SpawnInitializer Instance;
    public float Progress;
    public bool IsDone;
    private int _currentWave;
    private int _count;
    private int _assetsCount;
    private void Awake()
    {
        Instance = this;
        GetAssets();
        InitSpawnPoints();
    }
    // private void Start()
    // {
    //     EventHolder.OnAssetsLoad += StartFirstWave;
    // }
    // private void OnDisable()
    // {
    //     EventHolder.OnAssetsLoad -= StartFirstWave;
    // }
    // private void StartFirstWave()
    // {
    //     StartWave(0);
    // }
    private void InitSpawnPoints()
    {
        foreach (var spawnWave in _spawnWaves)
        {
            foreach (var spawnPoint in spawnWave.SpawnPoints)
            {
                if (spawnPoint.Enemy == NPC.R2H || spawnPoint.Enemy == NPC.R2Hi)
                {
                    spawnPoint.Factory = _factories[0];
                }
                else if (spawnPoint.Enemy == NPC.R2H2 || spawnPoint.Enemy == NPC.R2H2i)
                {
                    spawnPoint.Factory = _factories[1];
                }
            }
        }
    }
    private void GetAssets()
    {
        foreach (var factory in _factories)
        {
            factory.InitFactory(_NPCParent);
        }
        EventHolder.AssetsLoad();
    }
    public void StartWave(int waveNumber)
    {
        foreach (var spawnPoint in _spawnWaves[waveNumber].SpawnPoints)
        {
            spawnPoint.Factory.CreateUnit(spawnPoint.transform);
            _count++;
            Progress = (float)_count / (float)_spawnWaves[waveNumber].SpawnPoints.Length;
        }
        _currentWave++;
        IsDone = true;
    }
}
