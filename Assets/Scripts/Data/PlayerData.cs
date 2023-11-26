using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private string playerName;
    [SerializeField] private int health;
    [SerializeField] private int armor;
    [SerializeField] private int vampirism;
    [SerializeField] private int damage;
    [SerializeField] private int buffCount;

    public string PlayerName => playerName;
    public int Health => health;
    public int Armor => armor;
    public int Vampirism => vampirism;
    public int Damage => damage;
    public int BuffCount => buffCount;
}