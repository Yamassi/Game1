using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class R2HFactory : RobotsAbstractFactory
{
    private GameObject _basicRobot, _basicRobotWithItem;
    private Transform _NPCParent;
    public R2HFactory(Transform NPCParent)
    {
        _NPCParent = NPCParent;
        GetAssets();
    }
    private void OnDisable()
    {
        Addressables.Release(_basicRobot);
        Addressables.Release(_basicRobotWithItem);
    }
    async void GetAssets()
    {
        _basicRobot = await RobotsAbstractFactory.GetAsset("R2H");
        _basicRobotWithItem = await RobotsAbstractFactory.GetAsset("R2HWithItem");
        Debug.Log("assets ready");
    }
    public override GameObject CreateBasicRobot(Transform spawnPoint)
    {
        if (_basicRobot != null)
        {
            GameObject robot = Instantiate(_basicRobot, spawnPoint.position, Quaternion.identity, _NPCParent);
            return robot;
        }
        return null;
    }
    public override GameObject CreateBasicRobotWithItem(Transform spawnPoint, Item item)
    {
        if (_basicRobotWithItem != null)
        {
            GameObject robot = Instantiate(_basicRobotWithItem, spawnPoint.position, Quaternion.identity, _NPCParent);
            Reward reward = robot.GetComponent<Reward>();
            reward.SetItem(item);
            return robot;
        }
        return null;
    }
}
