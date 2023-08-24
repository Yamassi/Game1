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
        Color color;
        foreach (var renderer in _renderer.materials)
        {
            renderer.SetFloat("_Surface", 1);
            renderer.EnableKeyword("_ALPHATEST_ON");
        }

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (blinkTimer < _blinkTime)
            {
                blinkTimer += Time.deltaTime;
                float colorTransparency = Mathf.Lerp(1, 0, Mathf.PingPong(Time.time * _speed, 1));

                foreach (var renderer in _renderer.materials)
                {
                    // Debug.Log(colorTransparency);
                    color = renderer.GetColor("_BaseColor");
                    color.a = colorTransparency;
                    // Debug.Log(color.a);
                    renderer.SetColor("_BaseColor", color);
                    // renderer.SetFloat("_Tweak_transparency", Mathf.Lerp(0, -1, Mathf.PingPong(Time.time * _speed, 1)));
                }
            }
            else
            {

                foreach (var renderer in _renderer.materials)
                {
                    color = renderer.GetColor("_BaseColor");
                    color.a = 0;
                    renderer.SetColor("_BaseColor", color);
                    // Debug.Log("Blink End " + renderer.GetColor("_BaseColor"));
                }
                _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                _disposable.Clear();
            }
        }).AddTo(_disposable);
    }
}
