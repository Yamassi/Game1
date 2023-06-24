using UnityEngine;
public class SpawnWave : MonoBehaviour
{
    public SpawnPoint[] SpawnPoints;
    public void Init()
    {
        SpawnPoints = GetComponentsInChildren<SpawnPoint>();
    }
}
