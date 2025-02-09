using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeIndicator : MonoBehaviour
{
    [SerializeField] protected Slider _slider;
    [SerializeField] private TextMeshProUGUI _name;
    public void SetName(string name)
    {
        _name.text = name;
    }
    public void SetMaxLifes(int value)
    {
        _slider.maxValue = value;
        _slider.value = value;
    }
    public void RemoveLifes(int value)
    {
        _slider.value -= value;
    }
    public void AddLifes(int value)
    {
        _slider.value += value;
    }
}
