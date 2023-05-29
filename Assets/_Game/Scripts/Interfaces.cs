using UnityEngine;
public interface IInput
{
    Vector2 GetDirection();
    // bool GetFire();
}

public interface IMove
{
    void Move(Vector2 direction);
    void Look(Vector2 direction);
}
public interface IHealth
{
    void AddHealth(int health);
    void TakeDamage(int damage);
}
public interface IAnimate
{
    void SetMovementDirection(Vector2 movementDirection);
}