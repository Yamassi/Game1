using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInitializer : MonoBehaviour
{
    [SerializeField] private SpawnWave[] _spawnWaves;
    [SerializeField] private Transform _NPCParent;
    public SpawnInitializer Instance;
    public float Progress;
    public bool IsDone;
    private RobotsAbstractFactory _r2Hfactory, _r2H2factory;
    private int _count;
    private int _assetsCount;
    private void Awake()
    {
        Instance = this;
        GetAssets();
    }
    private void GetAssets()
    {
        _r2Hfactory = new R2HFactory(_NPCParent);
        _r2H2factory = new R2H2Factory(_NPCParent);
    }
    public void StartWave(int waveNumber)
    {
        foreach (var spawnPoint in _spawnWaves[waveNumber].SpawnPoints)
        {
            _r2Hfactory.CreateBasicRobot(spawnPoint);
            _count++;
            Progress = (float)_count / (float)_spawnWaves[waveNumber].SpawnPoints.Length;
        }

        IsDone = true;
    }
}

[Serializable]
public struct SpawnWave
{
    public Transform[] SpawnPoints;
}
