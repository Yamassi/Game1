using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;
    public static LoadingScreen Instance;
    private void Awake()
    {
        Instance = this;

        _loadingBar.maxValue = 100;
        _loadingBar.value = 0;
    }
    public void SetLoadingBar(int loadNumber) => _loadingBar.value = loadNumber;

}
