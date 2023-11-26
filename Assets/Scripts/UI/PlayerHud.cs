using TMPro;
using UnityEngine;

public class PlayerHud : Hud
{
    private const string healthTextBegin = "HEALTH: ";
    private const string armorTextBegin = "ARMOR: ";
    private const string damageTextBegin = "DAMAGE: ";
    private const string vampirismTextBegin = "VAMPIRISM: ";

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI vampirismText;
    [SerializeField] private TextMeshProUGUI buffsnameText;

    public void OnHealthChange(int value) 
    {
        SetText(healthText, healthTextBegin + value.ToString());
    } 

    public void OnArmorChange(int value)
    {
        SetText(armorText, armorTextBegin + value.ToString());
    }

    public void OnDamageChange(int value)
    {
        SetText(damageText, damageTextBegin + value.ToString());
    }

    public void OnVampirismChange(int value)
    {
        SetText(vampirismText, vampirismTextBegin + value.ToString());
    }

    public void OnBuffChange(string value)
    {
        SetText(buffsnameText, value);
    }
}
