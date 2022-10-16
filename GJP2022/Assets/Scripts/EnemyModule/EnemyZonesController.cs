using Jiufen.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZonesController : MonoBehaviour
{
    public EnemyStateMachine rangeEnemyPrefab;
    public EnemyStateMachine meleeEnemyPrefab;

    public List<EnemyZoneParams> enemyZoneParams;
    public List<List<GameObject>> zoneLimits;

    public int currentZoneIndex;
    public EnemyZoneParams currentZone;

    public int necessaryEnemiesToKill = 0;
    public int enemyKillCount = 0;

    public void SpawnEnemies(int zone)
    {
        currentZone = enemyZoneParams[zone];
        foreach (var zoneLimit in zoneLimits[zone])
            zoneLimit.SetActive(true);
        necessaryEnemiesToKill = currentZone.enemyRangeSpawn + currentZone.enemyMeleeSpawn;

        for (int i = 0; i < currentZone.enemyRangeSpawn; i++)
            Instantiate(rangeEnemyPrefab, currentZone.spawnPoints[Random.Range(0, currentZone.spawnPoints.Count)], Quaternion.identity);
        for (int i = 0; i < currentZone.enemyMeleeSpawn; i++)
            Instantiate(meleeEnemyPrefab, currentZone.spawnPoints[Random.Range(0, currentZone.spawnPoints.Count)], Quaternion.identity);
    }
    public void Update()
    {
        if (enemyKillCount == necessaryEnemiesToKill)
            EndZone();
    }

    public void EndZone()
    {
        foreach (var zoneLimit in zoneLimits[currentZoneIndex])
            zoneLimit.SetActive(false);
        AudioManager.PlayAudio("OST_WALK");
    }


}
