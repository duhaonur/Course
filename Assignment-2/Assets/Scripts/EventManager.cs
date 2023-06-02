using System;

public static class EventManager
{
    // GAME
    public static Action OnStartGame;
    public static void CallStartGame() { OnStartGame?.Invoke(); }

    // END GAME
    public static Action OnStopGame;
    public static void CallStopGame() { OnStopGame?.Invoke(); }
    public static Action OnLevelFailed;
    public static void CallLevelFailed() { OnLevelFailed?.Invoke(); }
    public static Action OnLevelCompleted;
    public static void CallLevelCompleted() { OnLevelCompleted?.Invoke(); }

    // SCORE
    public static Action OnScoreIncreased;
    public static void CallIncreaseScore() { OnScoreIncreased?.Invoke();}
}
