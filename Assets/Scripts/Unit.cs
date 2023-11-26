using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour
{
    private const string TakeDamageAnimation = "TakeDamage";

    [SerializeField] private PlayerHud playerHud;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Animator playerAnimator;

    private List<BuffActivate> buffsActivate = new List<BuffActivate>();

    private string buffsActivateName = "";

    private string playerName;
    private int healthCount;
    private int armorCount;
    private int vampirismCount;
    private int damageCount;
    private bool isDead;

    public List<BuffActivate> BuffActivates { get => buffsActivate; }
    public string PlayerName { get => playerName;  }
    public int HealthCount { get => healthCount; }
    public int ArmorCount { get => armorCount; set => armorCount = value; }
    public int VampirismCount { get => vampirismCount; set => vampirismCount = value; }
    public int DamageCount { get => damageCount; }
    public bool IsDead { get => isDead; }

    private void OnEnable()
    {
        Actions.OnRoundChange += OnRoundChange;
        Actions.OnRandomBuffApply += OnAddBuff;
    }

    private void Awake()
    {
        playerName = playerData.PlayerName;
        healthCount = playerData.Health;
        armorCount = playerData.Armor;
        vampirismCount = playerData.Vampirism;
        damageCount = playerData.Damage;
    }

    private void Start()
    {
        UpdateHud();
    }

    private void OnDisable()
    {
        Actions.OnRoundChange -= OnRoundChange;
        Actions.OnRandomBuffApply -= OnAddBuff;
    }

    private void OnTakeDamage()
    {
        playerAnimator.SetTrigger(TakeDamageAnimation);
    }
    public void UpdateHud()
    {
        playerHud.OnHealthChange(healthCount);
        playerHud.OnArmorChange(armorCount);
        playerHud.OnDamageChange(damageCount);
        playerHud.OnVampirismChange(vampirismCount);
        playerHud.OnBuffChange(buffsActivateName);
    }
    public void OnAddBuff(BuffActivate value, Unit currentPlayer)
    {
        if (this != currentPlayer) return;

        buffsActivate.Add(value);

        if (value.BuffType.DoubleDamage)
            damageCount *= 2;

        int newArmor = armorCount + value.BuffType.ArmorSelf;
        int newVampirism = vampirismCount + value.BuffType.VampirismSelf;

        armorCount = Mathf.Clamp(newArmor, 0, 100);
        vampirismCount = Mathf.Clamp(newVampirism, 0, 100);

        buffsActivateName += (value.BuffType.BuffName + ": " + value.BuffDuration);
        buffsActivateName += "  ";

        UpdateHud();
    }

    public void OnRoundChange(int value)
    {
        buffsActivateName = "";

        for (int i = 0; i < buffsActivate.Count; ++i)
        {
            if (--buffsActivate[i].BuffDuration == 0)
            {
                if (buffsActivate[i].BuffType.DoubleDamage)
                    damageCount /= 2;

                int newVampirism = vampirismCount - buffsActivate[i].BuffType.VampirismSelf;
                vampirismCount = Mathf.Clamp(newVampirism, 0, 100);

                if (buffsActivate[i].BuffType.ArmorSelf > 0)
                {
                    int newArmor = armorCount - buffsActivate[i].BuffType.ArmorSelf;
                    armorCount = Mathf.Clamp(newArmor, 0, 100);
                }

                buffsActivate.RemoveAt(i--);
            }
            else
            {
                buffsActivateName += (buffsActivate[i].BuffType.BuffName + ": " + buffsActivate[i].BuffDuration);
                buffsActivateName += "  ";
            }
        }

        UpdateHud();
    }

    public void OnHealthChange(int value)
    {
        int newHealthCount = healthCount + value;
        if (newHealthCount < healthCount && newHealthCount > 0)
            OnTakeDamage();

        healthCount = Mathf.Clamp(newHealthCount, 0, 100);
        isDead = healthCount <= 0;
    }
}
