using System;

public static class EventHolder
{
    public static Action OnPlayerFall;
    public static void PlayerFall() => OnPlayerFall?.Invoke();
    public static Action OnPlayerDie;
    public static void PlayerDie() => OnPlayerDie?.Invoke();

    public static Action OnNPCDie;
    public static void NPCDie() => OnNPCDie?.Invoke();

    public static Action<int> OnPlayerTakeDamage;
    public static void PlayerTakeDamage(int damage) => OnPlayerTakeDamage?.Invoke(damage);
    public static Action<int> OnPlayerTakeHealth;
    public static void PlayerTakeHealth(int healt) => OnPlayerTakeHealth?.Invoke(healt);

    public static Action OnAssetsLoad;
    public static void AssetsLoad() => OnAssetsLoad?.Invoke();
}
