using UnityEngine;
using TMPro;

public class BattleHud : Hud
{
    private const string roundTextBegin = "ROUND: ";

    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI playerText;

    private void OnEnable()
    {
        Actions.OnRoundChange += OnChangeRound;
        Actions.OnPlayerChange += OnPlayerChange;
    }

    private void OnDisable()
    {
        Actions.OnRoundChange -= OnChangeRound;
        Actions.OnPlayerChange -= OnPlayerChange;
    }

    public void OnChangeRound(int value)
    {
        SetText(roundText, roundTextBegin + value.ToString());
    }

    public void OnPlayerChange(Unit value)
    {
        SetText(playerText, value.PlayerName);
    }
}
