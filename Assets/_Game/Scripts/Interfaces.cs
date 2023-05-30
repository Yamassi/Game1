using UnityEngine;
public interface IInput
{
    Vector2 GetDirection();
    bool GetAttack();
    bool GetJump();
}

public interface IMove
{
    void Move(Vector2 direction);
    void Look(Vector2 direction);
}
public interface IJump
{
    void Jump(bool isJump);
    void Gravity();
}
public interface IHealth
{
    void AddHealth(int health);
    void TakeDamage(int damage);
}
public interface IAnimate
{
    void SetMovementDirection(Vector2 movementDirection);
    void AttackAnimate(bool isAttack);
}