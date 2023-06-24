using UnityEngine;
public class SpawnPoint : MonoBehaviour
{
    public GenericFactory Factory;
    public NPC Enemy;
    public Item Item;
}
public enum NPC
{
    R2H = 0,
    R2Hi = 1,
    R2H2 = 2,
    R2H2i = 3
}
