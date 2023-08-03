using System.Collections;
using UnityEngine;
using UniRx;
public class BlinkFX : MonoBehaviour
{
    [SerializeField] private float _blinkTime = 1.5f;
    private float _speed = 8;
    private Renderer _renderer;
    private CompositeDisposable _disposable = new CompositeDisposable();
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    private void OnDisable()
    {
        _disposable.Clear();
    }
    public void InitBlinkFX()
    {
        _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        float blinkTimer = 0;
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (blinkTimer < _blinkTime)
            {
                blinkTimer += Time.deltaTime;
                foreach (var renderer in _renderer.materials)
                {
                    renderer.SetFloat("_Tweak_transparency", Mathf.Lerp(0, -1, Mathf.PingPong(Time.time * _speed, 1)));
                }
            }
            else
            {
                foreach (var renderer in _renderer.materials)
                {
                    renderer.SetFloat("_Tweak_transparency", 0);
                }
                _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                _disposable.Clear();
            }
        }).AddTo(_disposable);
    }
}
