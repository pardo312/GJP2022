using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "GJP2022/EnemyStats", order = 0)]
public class EnemyStats : ScriptableObject
{
    public float strength;
    public float cooldownAttacks;
    public Vector2Int emeraldDropMinMax;
}
