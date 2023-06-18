using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class WeaponComponent : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private BoxCollider _attackCollider;
    [SerializeField] private ParticleSystem _fx;
    private CompositeDisposable _disposable = new CompositeDisposable();

    public void StartAttack()
    {
        _attackCollider.OnTriggerEnterAsObservable()
        .Where(other => other.GetComponent(typeof(IDamageable)))
        .Subscribe(other =>
        {
            AddDamage(other);
            // _disposable.Clear();
        }).AddTo(_disposable);
    }
    public void EndAttack()
    {
        _disposable.Clear();
    }
    public void StartWeaponEffect()
    {
        if (_fx != null)
            _fx.gameObject.SetActive(true);
    }
    public void EndWeaponEffect()
    {
        if (_fx != null)
            _fx.gameObject.SetActive(false);
    }
    private void AddDamage(Collider other)
    {
        IDamageable damageable = other.GetComponent(typeof(IDamageable)) as IDamageable;
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
            Debug.Log("Hit enemy");
        }
    }

}
