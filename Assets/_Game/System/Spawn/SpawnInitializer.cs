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
    private int _assetsCount, _currentWave, _count, _aliveNPCcount;
    public void Init()
    {
        Instance = this;
        EventHolder.OnAssetsLoad += AssetLoad;
        EventHolder.OnNPCDie += NPCDie;

        GetAssets();
        InitSpawnPoints();
    }
    private void OnDisable()
    {
        EventHolder.OnAssetsLoad -= AssetLoad;
        EventHolder.OnNPCDie -= NPCDie;
    }
    private void AssetLoad()
    {
        _assetsCount++;

        if (_assetsCount == _factories.Length)
        {
            StartWave();
        }
    }
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
    }
    private void NPCDie()
    {
        _aliveNPCcount--;
        if (_aliveNPCcount <= 0)
        {
            StartWave();
        }
    }
    private void StartWave()
    {
        if (_currentWave < _spawnWaves.Length)
        {
            foreach (var spawnPoint in _spawnWaves[_currentWave].SpawnPoints)
            {
                spawnPoint.Factory.CreateUnit(spawnPoint.transform);
                _aliveNPCcount++;
                _count++;
                Progress = (float)_count / (float)_spawnWaves[_currentWave].SpawnPoints.Length;
            }
            _currentWave++;
            IsDone = true;
        }
    }

}
