using System.Collections;
using UnityEngine;

public class TurnHandler : State
{
    private BattleHandler battleHandler;
    private Unit attackerGO;
    private Unit targetGO;
    private int turn;

    public TurnHandler(BattleHandler battleHandler, Unit attackerGO, Unit targetGO, int turn)
    {
        this.battleHandler = battleHandler;
        this.attackerGO = attackerGO;
        this.targetGO = targetGO;
        this.turn = turn;
    }

    public override IEnumerator Enter()
    {
        Actions.OnPlayerChange?.Invoke(attackerGO);

        if (turn % 2 != 0)
            Actions.OnRoundChange?.Invoke(Mathf.CeilToInt(turn / 2.0f));

        yield return new WaitForSeconds(1.0f);
    }

    public override IEnumerator Execute()
    {
        int damageCount = attackerGO.DamageCount;
        int vampirismCount = attackerGO.VampirismCount;

        foreach (BuffActivate buff in attackerGO.BuffActivates)
        {
            targetGO.ArmorCount += buff.BuffType.ArmorEnemy;
            targetGO.VampirismCount += buff.BuffType.VampirismEnemy;
        }
        targetGO.ArmorCount = Mathf.Clamp(targetGO.ArmorCount, 0, 100);
        targetGO.VampirismCount = Mathf.Clamp(targetGO.VampirismCount, 0, 100);

        int totalDamage = CalculateHealthChangeWithArmor(damageCount, targetGO.ArmorCount);
        int heathDecrease = CalculateHealthChangWithVampirism(totalDamage, vampirismCount);

        attackerGO.OnHealthChange(heathDecrease);
        targetGO.OnHealthChange(totalDamage * (-1));

        attackerGO.UpdateHud();
        targetGO.UpdateHud();

        yield return new WaitForSeconds(1.0f);

        GetResult();
    }

    private int CalculateHealthChangeWithArmor(int value, int coeff)
    {
        return Mathf.RoundToInt(value * (1 - (coeff / 100.0f)));
    }
    private int CalculateHealthChangWithVampirism(int value, int coeff)
    {
        return Mathf.RoundToInt(value * (coeff / 100.0f));
    }

    private void GetResult()
    {
        bool targetIsDead = targetGO.IsDead;

        if (targetIsDead)
            battleHandler.SetState(new ScoreHandler());
        else
            battleHandler.SetState(new TurnHandler(battleHandler, targetGO, attackerGO, ++turn));
    }
}
