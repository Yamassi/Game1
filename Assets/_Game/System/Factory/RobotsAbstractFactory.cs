using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public abstract class RobotsAbstractFactory : MonoBehaviour
{
    public abstract GameObject CreateBasicRobot();
    public abstract GameObject CreateBasicRobotItemDropped(Item item);
}

public abstract class R2HFactory : RobotsAbstractFactory
{
    private Transform _spawnPoint;
    public R2HFactory(Transform spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }

    public override GameObject CreateBasicRobot()
    {
        // AsyncOperationHandle<GameObject> handle = Addressables.Instantiate("R2H-1");
        // await handle.Task;
        // if (handle.Status == AsyncOperationStatus.Succeeded)
        // {
        //     GameObject r2 = handle.Result;
        //     r2.transform.position = Vector3.zero;
        //     Addressables.Release(handle);
        // }
        throw new System.NotImplementedException();
    }
    public override GameObject CreateBasicRobotItemDropped(Item item)
    {
        Addressables.InstantiateAsync("R2H-1-ITEM");
        throw new System.NotImplementedException();
    }
}
