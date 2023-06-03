using System;

public static class MyEvents
{
    // PLAYER
    public static Action<int> OnDecreaseHealth;
    public static void CallDecreaseHealth(int amount) { OnDecreaseHealth?.Invoke(amount); }

    public static Action<int> OnUpdateHealth;
    public static void CallUpdateHealth(int amount) { OnUpdateHealth?.Invoke(amount); }

    public static Action<int> OnUpdateHealthUI;
    public static void CallUpdateHealthUI(int amount) { OnUpdateHealthUI?.Invoke(amount); }

    public static Action<string> OnChangeLane;
    public static void CallChangeLane(string str) { OnChangeLane?.Invoke(str); }

    // SCORE
    public static Action<float> OnIncreaseScore;
    public static void CallIncreaseScore(float amount) {  OnIncreaseScore?.Invoke(amount); }

    public static Action<float> OnGetScore;
    public static void CallGetScore(float score) { OnGetScore?.Invoke(score); }

    // GAME
    public static Action<bool> OnGameEnded;
    public static void CallGameEnded(bool status) {  OnGameEnded?.Invoke(status); }

}
