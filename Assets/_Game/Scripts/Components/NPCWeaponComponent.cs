using UniRx;
using UniRx.Triggers;

public class NPCWeaponComponent : WeaponComponent
{
    public override void StartAttack()
    {
        _attackCollider.OnTriggerEnterAsObservable()
            .Where(other => other.GetComponent(typeof(IDamageable)))
            .Subscribe(other =>
            {
                AddDamage(other);
                _disposable.Clear();
            }).AddTo(_disposable);
    }
}
