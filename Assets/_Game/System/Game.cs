using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private LifeIndicator _lifeIndicator;
    [SerializeField] private GameObject _uiInputs;

    private void Awake()
    {
        EventHolder.OnPlayerFall += PlayerFall;
        EventHolder.OnPlayerDie += PlayerDie;
        EventHolder.OnPlayerTakeDamage += TakeDamage;
        EventHolder.OnPlayerTakeHealth += TakeHealt;

        _lifeIndicator.SetMaxLifes(_player.GetComponent<IHealth>().GetMaxHealth());
    }

    private void PlayerFall()
    {
        _player.TakeDamage(5);
        _player.ReturnToGround();
    }

    private void TakeHealt(int health)
    {
        _lifeIndicator.AddLifes(health);
    }

    private void TakeDamage(int damage)
    {
        _lifeIndicator.RemoveLifes(damage);
    }

    private void PlayerDie()
    {
        _uiInputs.SetActive(false);
    }

    private void OnDisable()
    {
        EventHolder.OnPlayerDie -= PlayerDie;
        EventHolder.OnPlayerTakeDamage -= TakeDamage;
        EventHolder.OnPlayerTakeHealth -= TakeHealt;
    }
}