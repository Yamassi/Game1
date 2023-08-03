using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;

public abstract class GenericFactory : MonoBehaviour
{
    [SerializeField] private AssetReference _assetReference;
    private Transform _parent;
    private GameObject _prefab;
    public void InitFactory(Transform parent)
    {
        _parent = parent;
        GetAssets();
    }
    private void OnDisable()
    {
        Addressables.Release(_prefab);
    }
    public GameObject CreateUnit(Transform spawnPoint)
    {
        return Instantiate(_prefab, spawnPoint.position, Quaternion.identity, _parent);
    }
    public GameObject CreateUnit(Transform spawnPoint, Item item)
    {
        GameObject unit = Instantiate(_prefab, spawnPoint.position, Quaternion.identity, _parent);
        Reward reward = unit.GetComponent<Reward>();
        reward.SetItem(item);
        return unit;
    }
    private async void GetAssets()
    {
        _prefab = await GetAsset(_assetReference);
        EventHolder.AssetsLoad();
    }
    private async Task<GameObject> GetAsset(AssetReference asset)
    {
        Task<GameObject> asyncOperationHandler = Addressables.LoadAssetAsync<GameObject>(asset).Task;
        GameObject result = await asyncOperationHandler;
        return result;
    }
}
