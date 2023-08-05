using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInitializer : MonoBehaviour
{
    [SerializeField] private GenericFactory[] _factories;
    [SerializeField] private SpawnWave[] _spawnWaves;
    [SerializeField] private NPCTacticController _nPCTactic;
    [SerializeField] private Transform _NPCParent;
    public static SpawnInitializer Instance;
    public float Progress;
    public bool IsDone;
    private int _assetsCount, _currentWave, _count, _aliveNPCcount, _assetsCountForLoad;
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
            StartCoroutine(StartWave());
        }
    }
    private void InitSpawnPoints()
    {
        foreach (var spawnWave in _spawnWaves)
        {
            spawnWave.Init();

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
        for (int i = 0; i < _factories.Length; i++)
        {
            _factories[i].InitFactory(_NPCParent);
        }
    }
    private void NPCDie(NPCController nPC)
    {
        _nPCTactic.RemoveNPC(nPC);
        _nPCTactic.SendNPCToAttack();

        _aliveNPCcount--;
        if (_aliveNPCcount <= 0)
        {
            StartCoroutine(StartNextWaves());
            EventHolder.WaveStart(_currentWave);
        }
    }
    private IEnumerator StartNextWaves()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(StartWave());
    }
    private IEnumerator StartWave()
    {
        if (_currentWave < _spawnWaves.Length)
        {
            int randomNumber = UnityEngine.Random.Range(1, 4);
            _nPCTactic.SetCountNPCsAttackAtOnce(randomNumber);

            foreach (var spawnPoint in _spawnWaves[_currentWave].SpawnPoints)
            {
                GameObject npc = spawnPoint.Factory.CreateUnit(spawnPoint.transform);
                if (spawnPoint.Item != null)
                {
                    Reward reward = npc.GetComponent<Reward>();
                    reward.SetItem(spawnPoint.Item);
                }
                _nPCTactic.AddNPC(npc.GetComponent<NPCController>());
                _aliveNPCcount++;

                yield return new WaitForEndOfFrame();

                Progress = (float)_count / (float)_spawnWaves[_currentWave].SpawnPoints.Length;

                _count++;
            }
            _currentWave++;
        }
        yield return new WaitForSeconds(0.5f);
        IsDone = true;

        _nPCTactic.Init();
    }
}
