using UnityEngine;
public class Reward : MonoBehaviour
{
    [SerializeField] private Item _item;
    private Vector3 offset = new Vector3(0, 0.5f, 0);
    public void DropItem()
    {
        if (_item != null)
            Instantiate(_item, transform.position + offset, Quaternion.identity);
    }
    public void SetItem(Item item) => _item = item;

}
