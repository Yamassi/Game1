using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreen;
    public static GameInitializer Instance;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private float _loadingSceneProgress, _loadingSpawnProgress;
    private void Awake()
    {
        Instance = this;

        StartLoading();
    }

    private void StartLoading()
    {
        _loadingScreen.gameObject.SetActive(true);

        SceneManager.UnloadSceneAsync(0);
        _scenesLoading.Add(SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    private IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < _scenesLoading.Count; i++)
        {
            while (!_scenesLoading[i].isDone)
            {
                foreach (AsyncOperation operation in _scenesLoading)
                {
                    _loadingSceneProgress += operation.progress;
                }
                _loadingSceneProgress = (_loadingSceneProgress / _scenesLoading.Count) * 100f;

                _loadingScreen.SetLoadingBar(Mathf.RoundToInt(_loadingSceneProgress));

                yield return null;
            }
        }
        _loadingScreen.gameObject.SetActive(false);
    }
    private IEnumerator GetSpawnProgress()
    {
        yield return null;
    }
}
