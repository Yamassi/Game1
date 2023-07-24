using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private Button _startButton;
    public static GameInitializer Instance;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private float _totalSceneProgress, _spawnProgress;
    private void Awake()
    {
        Instance = this;

        _startButton.onClick.AddListener(StartLoading);
    }
    private void Start()
    {
        AudioController.Instance.StartMusic(FMODEvents.Instance.Intro);
    }
    private void OnDisable()
    {
        _startButton.onClick.RemoveAllListeners();
    }
    private void StartLoading()
    {
        _loadingScreen.gameObject.SetActive(true);
        _mainMenu.gameObject.SetActive(false);
        _startButton.onClick.RemoveAllListeners();

        _scenesLoading.Add(SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
        StartCoroutine(GetSpawnProgress());
    }
    private IEnumerator GetSceneLoadProgress()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < _scenesLoading.Count; i++)
        {
            while (!_scenesLoading[i].isDone)
            {
                _totalSceneProgress = 0;

                foreach (AsyncOperation operation in _scenesLoading)
                {
                    _totalSceneProgress += operation.progress;
                }
                _totalSceneProgress = (_totalSceneProgress / _scenesLoading.Count) * 100f;
                yield return null;
            }
        }
        yield return null;
    }
    private IEnumerator GetSpawnProgress()
    {
        float totalProgress = 0;

        for (int i = 0; i < _scenesLoading.Count; i++)
        {
            while (SpawnInitializer.Instance == null || !SpawnInitializer.Instance.IsDone)
            {
                if (SpawnInitializer.Instance == null)
                {
                    _spawnProgress = 0;
                }
                else
                {
                    _spawnProgress = Mathf.Round(SpawnInitializer.Instance.Progress * 100f);
                }

                totalProgress = Mathf.Round((_totalSceneProgress + _spawnProgress) / 2f);

                _loadingScreen.SetLoadingBar(Mathf.RoundToInt(totalProgress));
                yield return null;
            }
        }

        _loadingScreen.gameObject.SetActive(false);
        SceneManager.UnloadSceneAsync(0);

        yield return null;
    }
}
