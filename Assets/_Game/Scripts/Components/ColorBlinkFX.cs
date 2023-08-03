using UnityEngine;
using UniRx;

public class ColorBlinkFX : MonoBehaviour
{
    [SerializeField] private Color _endColor;
    [SerializeField] private float _blinkTime = 0.1f;
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
        Color baseColor = new Color();
        Color shadeColor1 = new Color();
        Color shadeColor2 = new Color();

        foreach (var renderer in _renderer.materials)
        {
            baseColor = renderer.GetColor("_BaseColor");
            shadeColor1 = renderer.GetColor("_1st_ShadeColor");
            shadeColor2 = renderer.GetColor("_2nd_ShadeColor");
        }
        float blinkTimer = 0;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (blinkTimer < _blinkTime)
            {
                blinkTimer += Time.deltaTime;
                foreach (var renderer in _renderer.materials)
                {
                    renderer.SetColor("_BaseColor", Color.Lerp(baseColor, _endColor, Mathf.PingPong(Time.time * _speed, 1)));
                    renderer.SetColor("_1st_ShadeColor", Color.Lerp(shadeColor1, _endColor, Mathf.PingPong(Time.time * _speed, 1)));
                    renderer.SetColor("_2nd_ShadeColor", Color.Lerp(shadeColor2, _endColor, Mathf.PingPong(Time.time * _speed, 1)));
                    Debug.Log(renderer.name);
                }
            }
            else
            {
                foreach (var renderer in _renderer.materials)
                {
                    renderer.SetColor("_BaseColor", baseColor);
                    renderer.SetColor("_1st_ShadeColor", shadeColor1);
                    renderer.SetColor("_2nd_ShadeColor", shadeColor2);
                }
                _disposable.Clear();
            }

        }).AddTo(_disposable);
    }
}

