using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
public class WeaponComponent : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] protected BoxCollider _attackCollider;
    [SerializeField] protected ParticleSystem _fx;
    protected CompositeDisposable _disposable = new CompositeDisposable();

    public virtual void StartAttack()
    {
        _attackCollider.OnTriggerEnterAsObservable()
        .Where(other => other.GetComponent(typeof(IDamageable)))
        .Subscribe(other =>
        {
            AddDamage(other);
        }).AddTo(_disposable);
    }
    private void OnDisable()
    {
        _disposable.Clear();
    }
    public void EndAttack()
    {
        _disposable.Clear();
    }
    public void StartWeaponEffect()
    {
        if (_fx != null)
            _fx.gameObject.SetActive(true);
        _fx.Play();
    }
    public void EndWeaponEffect()
    {
        if (_fx != null)
            _fx.gameObject.SetActive(false);
    }
    protected void AddDamage(Collider other)
    {
        IDamageable damageable = other.GetComponent(typeof(IDamageable)) as IDamageable;
        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
            Debug.Log("Hit enemy");
        }
    }
    public void DisableWeapon()
    {
        _attackCollider.gameObject.SetActive(false);
    }
}
