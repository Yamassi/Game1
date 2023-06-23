using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class R2H2Factory : RobotsAbstractFactory
{
    private GameObject _basicRobot, _basicRobotWithItem;
    private Transform _NPCParent;
    public R2H2Factory(Transform NPCParent)
    {
        _NPCParent = NPCParent;
        GetAssets();
    }
    async void GetAssets()
    {
        _basicRobot = await RobotsAbstractFactory.GetAsset("R2H2");
        _basicRobotWithItem = await RobotsAbstractFactory.GetAsset("R2H2WithItem");
    }
    private void OnDisable()
    {
        Addressables.Release(_basicRobot);
        Addressables.Release(_basicRobotWithItem);
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
