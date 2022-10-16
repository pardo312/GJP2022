using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyZones", menuName = "GJP2022/EnemyZones", order = 0)]
public class EnemyZoneParams : ScriptableObject
{
    public int enemyRangeSpawn;
    public int enemyMeleeSpawn;
    public List<Vector3> spawnPoints;
}
