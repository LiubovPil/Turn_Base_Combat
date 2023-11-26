using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : StateMachine
{
    private const int maxBuffDuration = 3;
    private const int maxBuffCount = 2;

    [SerializeField] private List<BuffData> buffDatas;
    [SerializeField] private Unit firstPlayer;
    [SerializeField] private Unit secondPlayer;

    private Unit leadPlayer;
    private bool canApplyBuff;
    private bool canAttack;

    private void OnEnable()
    {
        Actions.OnPlayerChange += ResetPlayerParametrs;
    }

    private void Start()
    {
        if (firstPlayer == null || secondPlayer == null)
            throw new ArgumentNullException("Players are required!");

        canApplyBuff = true;
        canAttack = true;

        SetState(new TurnHandler(this, firstPlayer, secondPlayer, 1));
    }

    private void OnDisable()
    {
        Actions.OnPlayerChange -= ResetPlayerParametrs;
    }

    public void OnAttackButton(Unit player)
    {
        if (player == leadPlayer && canAttack)
        {
            canApplyBuff = false;
            canAttack = false;
            StartCoroutine(State.Execute());
        }
    }

    public void OnApplyBuffButton(Unit player)
    {
        if (player == leadPlayer && canApplyBuff)
            ChooseBuff();
    }

    private void ChooseBuff()
    {
        if (leadPlayer.BuffActivates.Count >= maxBuffCount) return;

        canApplyBuff = false;

        List<BuffData> tempBuffDatas = buffDatas;
        foreach (BuffActivate buff in leadPlayer.BuffActivates)
        {
            tempBuffDatas.Remove(buff.BuffType);
        }

        int index = UnityEngine.Random.Range(0, tempBuffDatas.Count);
        int duration = UnityEngine.Random.Range(1, maxBuffDuration);

        BuffActivate buffActivate = new BuffActivate(tempBuffDatas[index], duration);

        Actions.OnRandomBuffApply?.Invoke(buffActivate, leadPlayer);
    }

    private void ResetPlayerParametrs(Unit value)
    {
        leadPlayer = value;
        canApplyBuff = true;
        canAttack = true;
    }
}
