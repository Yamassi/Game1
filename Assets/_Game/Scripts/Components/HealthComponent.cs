using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHealth
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
    public void AddHealth(int health)
    {
        _currentHealth += health;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        _currentHealth -= damage;

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
        Handheld.Vibrate();
    }
    public int GetMaxHealth() => _maxHealth;
    public int GetCurrentHealth() => _currentHealth;

}
