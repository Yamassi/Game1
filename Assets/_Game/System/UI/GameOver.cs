using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private GameObject _gameOverPage;
    private void OnEnable()
    {
        EventHolder.OnAllNPCsDie += OpenLevelCompletePage;
        EventHolder.OnPlayerDie += OpenGameOverPage;
    }
    private void OnDisable()
    {
        EventHolder.OnAllNPCsDie -= OpenLevelCompletePage;
        EventHolder.OnPlayerDie -= OpenGameOverPage;
        _gameOverPage.SetActive(false);
    }
    public void OpenLevelCompletePage()
    {
        _gameOverPage.SetActive(true);
        _gameOverText.text = "Level Complete";
    }
    public void OpenGameOverPage()
    {
        _gameOverPage.SetActive(true);
        _gameOverText.text = "Game Over";
    }
}
