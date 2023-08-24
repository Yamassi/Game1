using UnityEngine;
using DG.Tweening;
public class FenceComponent : MonoBehaviour, IDamageable
{
    private IHealth _health;
    private ColorBlinkFX[] _blinkFXs;
    private Rigidbody _rigidbody;
    private Vector3 _punchPosition = new Vector3(0.1f, 0.1f, 0.1f);
    private Vector3 _punchRotation = new Vector3(15f, 15f, 15f);
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _health = GetComponent<IHealth>();
        _blinkFXs = GetComponentsInChildren<ColorBlinkFX>();
    }
    public void TakeDamage(int damage)
    {
        _health.TakeDamage(5);
        this.transform.DOPunchPosition(_punchPosition, 0.5f);
        foreach (var blinkFX in _blinkFXs)
        {
            blinkFX.InitBlinkFX();
        }
        AudioController.Instance?.PlayOneShot(FMODEvents.Instance.WallHit, transform.position);

        if (_health.GetCurrentHealth() <= 0)
        {
            transform.DOLocalRotate(_punchRotation, 0.5f);
            _rigidbody.isKinematic = false;
        }
    }
}
