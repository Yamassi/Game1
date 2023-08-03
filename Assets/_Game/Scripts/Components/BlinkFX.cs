using System.Collections;
using UnityEngine;
using UniRx;
public class BlinkFX : MonoBehaviour
{
    [SerializeField] private Color _endColor;
    private float _speed = 8;
    private SkinnedMeshRenderer _renderer;
    private CompositeDisposable _disposable = new CompositeDisposable();
    private void Awake()
    {
        _renderer = GetComponent<SkinnedMeshRenderer>();
    }
    private void OnDisable()
    {
        _disposable.Clear();
    }
    public void InitBlinkFX()
    {
        // Color baseColor = new Color();
        // Color shadeColor1 = new Color();
        // Color shadeColor2 = new Color();

        // foreach (var renderer in _renderer.materials)
        // {
        //     baseColor = renderer.GetColor("_BaseColor");
        //     shadeColor1 = renderer.GetColor("_1st_ShadeColor");
        //     shadeColor2 = renderer.GetColor("_2nd_ShadeColor");
        // }
        _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            foreach (var renderer in _renderer.materials)
            {
                renderer.SetFloat("_Tweak_transparency", Mathf.Lerp(0, -1, Mathf.PingPong(Time.time * _speed, 1)));
                // renderer.SetColor("_BaseColor", Color.Lerp(baseColor, _endColor, Mathf.PingPong(Time.time * _speed, 1)));
                // renderer.SetColor("_1st_ShadeColor", Color.Lerp(shadeColor1, _endColor, Mathf.PingPong(Time.time * _speed, 1)));
                // renderer.SetColor("_2nd_ShadeColor", Color.Lerp(shadeColor2, _endColor, Mathf.PingPong(Time.time * _speed, 1)));
                Debug.Log(renderer.name);
            }
        }).AddTo(_disposable);
    }
}

