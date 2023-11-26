using System;

public static class Actions 
{
    public static Action<int> OnRoundChange;
    public static Action<Unit> OnPlayerChange;
    public static Action<BuffActivate, Unit> OnRandomBuffApply;
}
