using UnityEngine;

[CreateAssetMenu(fileName = "New BuffData", menuName = "Buff Data")]
public class BuffData : ScriptableObject
{
    [SerializeField] private string buffName;
    [SerializeField] private bool doubleDamage;
    [SerializeField] private int armorSelf;
    [SerializeField] private int armorEnemy;
    [SerializeField] private int vampirismSelf;
    [SerializeField] private int vampirismEnemy;

    public string BuffName => buffName;
    public bool DoubleDamage => doubleDamage;
    public int ArmorSelf => armorSelf;
    public int ArmorEnemy => armorEnemy;
    public int VampirismSelf => vampirismSelf;
    public int VampirismEnemy => vampirismEnemy;
}

public class BuffActivate
{
    public BuffData BuffType { get; set; }
    public int BuffDuration { get; set; }

    public BuffActivate(BuffData buffType, int buffDuration)
    {
        BuffType = buffType;
        BuffDuration = buffDuration;
    }
}
