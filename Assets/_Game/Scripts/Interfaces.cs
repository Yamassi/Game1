using UnityEngine;
public interface IInput
{
    Vector2 GetDirection();
    bool GetAttack();
    bool GetAttackTrigger();
    bool GetJump();
}

public interface IMove
{
    void Move(Vector2 direction);
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
    int GetMaxHealth();
    int GetCurrentHealth();
}
public interface INPCAnimate
{
    void SetMovementDirection(Vector2 movementDirection);
    void AttackAnimate(bool isAttack);
    void BlockAnimate();
    void DieAnimate();
    void TakeDamageAnimate();
}
public interface IPlayerAnimate
{
    void SetMovementDirection(Vector2 movementDirection);
    void AttackAnimate(bool isAttack);
    void JumpAnimate(bool isJump);
    void DieAnimate();
    void TakeDamageAnimate();
}
public interface ISensor
{
    PlayerController GetChaser();
}
public interface INavMeshMove
{
    void RunAway(Transform chaserPosition);
    void Chase(Transform target);
    bool GetIsStopped();
    bool GetIsWalk();
    void Walk();
    void RunToggle(bool isRun);
    void StopMove();
    void RotateToTarget(Vector3 target);
}
public interface IDamageable
{
    void TakeDamage(int damage);
}