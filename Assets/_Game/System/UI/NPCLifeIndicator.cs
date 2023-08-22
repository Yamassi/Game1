using System.Collections;
using UnityEngine;

public class NPCLifeIndicator : LifeIndicator
{
    private float _lifeTime = 1.5f, _timer = 0;
    private void OnEnable()
    {
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (_timer < _lifeTime)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public void SetCurrentLife(int value)
    {
        _slider.value = value;
        _timer += 0.5f;
    }
}
