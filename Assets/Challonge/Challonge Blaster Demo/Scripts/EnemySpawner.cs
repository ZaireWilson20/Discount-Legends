using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Properties

    public GameObject enemyPrefab;
    public float startingSpawnRate;
    public float currentSpawnRate;
    public float maxSpawnRate;
    public float spawnRateIncrease;
    public float difficultyTimeInterval = 30;
    public Vector3 spawnRanges;

    #endregion

    #region Unity Methods


    #endregion

    #region Helper Methods


    /// Function:   StartEnemySpawning
    ///
    /// Summary:    Starts enemy spawning.
    ///
    /// Author: Khalil
    ///
    /// Date:   11/26/2021
    public void StartEnemySpawning()
    {
        currentSpawnRate = startingSpawnRate;
        InvokeRepeating("HandleNextSpawn", 0, currentSpawnRate);
        InvokeRepeating("IncreaseSpawnRate", 0, difficultyTimeInterval);
    }


    /// Function:   StopEnemySpawning
    ///
    /// Summary:    Stops enemy spawning.
    ///
    /// Author: Khalil
    ///
    /// Date:   11/26/2021
    public void StopEnemySpawning()
    {
        CancelInvoke("HandleNextSpawn");
    }


    /// Function:   HandleNextSpawn
    ///
    /// Summary:    Handles the next enemy spawn.
    ///
    /// Author: Khalil
    ///
    /// Date:   11/26/2021
    private void HandleNextSpawn()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRanges.x, spawnRanges.x), spawnRanges.y, spawnRanges.z);
        Quaternion spawnRotation = Quaternion.identity;
        GameObject nextEnemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);

        nextEnemy.transform.parent = this.transform;
    }

    public void IncreaseSpawnRate()
    {
        currentSpawnRate = Mathf.Clamp(currentSpawnRate - spawnRateIncrease, maxSpawnRate, startingSpawnRate);
        CancelInvoke("HandleNextSpawn");
        InvokeRepeating("HandleNextSpawn", 0, currentSpawnRate);
    }

    #endregion
}
