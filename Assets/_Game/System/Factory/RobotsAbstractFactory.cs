using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

public abstract class RobotsAbstractFactory : MonoBehaviour
{
    public abstract GameObject CreateBasicRobot(Transform spawnPoint);
    public abstract GameObject CreateBasicRobotWithItem(Transform spawnPoint, Item item);
    public static async Task<GameObject> GetAsset(string assetName)
    {
        Task<GameObject> asyncOperationHandler = Addressables.LoadAssetAsync<GameObject>(assetName).Task;
        GameObject result = await asyncOperationHandler;
        return result;
    }
}