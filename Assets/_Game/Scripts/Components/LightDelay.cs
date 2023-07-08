using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LightDelay : MonoBehaviour
{
    [SerializeField] private float _maxIntensity = 5, _minIntensity = 0;
    private Light _light;
    private CompositeDisposable _disposable = new CompositeDisposable();
    private void Awake()
    {
        _light = GetComponent<Light>();
        _light.intensity = 0;

        StartCoroutine(Blinking());
    }
    private IEnumerator Blinking()
    {
        yield return new WaitForSeconds(0.5f);
        LightToMax();
        yield return new WaitForSeconds(2.0f);
        LightToMax();
    }

    private void LightToMax()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (_light.intensity < _maxIntensity)
                _light.intensity += Time.deltaTime * 10;

            if (_light.intensity >= _maxIntensity)
            {
                _disposable.Clear();
                LightToMin();
            }


        }).AddTo(_disposable);
    }
    private void LightToMin()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (_light.intensity > _minIntensity)
                _light.intensity -= Time.deltaTime * 10;

            if (_light.intensity <= _minIntensity)
                _disposable.Clear();

        }).AddTo(_disposable);
    }
}
