using Jiufen.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyZonesController : MonoBehaviour
{
    public GameObject rangeEnemyPrefab;
    public GameObject meleeEnemyPrefab;

    public List<EnemyZoneParams> enemyZoneParams;
    public List<GameObject> zoneLimits;

    public int currentZoneIndex = 0;
    public EnemyZoneParams currentZone;

    public int necessaryEnemiesToKill = 0;
    public int enemyKillCount = 0;

    public void SpawnEnemies()
    {
        currentZone = enemyZoneParams[currentZoneIndex];
        zoneLimits[currentZoneIndex].SetActive(true);
        necessaryEnemiesToKill = currentZone.enemyRangeSpawn + currentZone.enemyMeleeSpawn;

        for (int i = 0; i < currentZone.enemyRangeSpawn; i++)
            Instantiate(rangeEnemyPrefab, currentZone.spawnPoints[Random.Range(0, currentZone.spawnPoints.Count)], Quaternion.identity);
        for (int i = 0; i < currentZone.enemyMeleeSpawn; i++)
            Instantiate(meleeEnemyPrefab, currentZone.spawnPoints[Random.Range(0, currentZone.spawnPoints.Count)], Quaternion.identity);

        enemyKillCount = 0;
    }

    public void KillEnemy()
    {
        enemyKillCount++;
    }

    public void Update()
    {
        if (enemyKillCount == necessaryEnemiesToKill)
            EndZone();
    }

    public void EndZone()
    {
        currentZoneIndex++;
        if (currentZoneIndex > zoneLimits.Count)
        {
            EndGame();
            return;
        }

        zoneLimits[currentZoneIndex].SetActive(false);
        AudioManager.PlayAudio("OST_WALK");
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}

