using UnityEngine;
public interface IInput
{
    Vector3 GetDirection();
    bool GetAttack();
    bool GetJump();
}

public interface IMove
{
    void Move(Vector3 direction);
    void Look(Vector3 direction);
    void Jump(bool isJump);
}
public interface IHealth
{
    void AddHealth(int health);
    void TakeDamage(int damage);
}
public interface IAnimate
{
    void SetMovementDirection(Vector3 movementDirection);
    void AttackAnimate(bool isAttack);
}