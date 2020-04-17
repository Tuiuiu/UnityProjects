﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControllers : MonoBehaviour
{
    public SpawnController spawn;
    public ScoreController score;
    public LootController loot;

    private int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.Find("EnemySpawner").GetComponent<SpawnController>();
        score = transform.Find("ScoreController").GetComponent<ScoreController>();
        loot = transform.Find("LootController").GetComponent<LootController>();

        enemyCount = 0;
        InvokeRepeating("SpawnLevel", 1.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called by enemy when it's health reaches 0. 
    // It will request other controllers (Score and Loot) to give
    // the corespondet rewards based on the properties of the
    // killed enemy
    public void EnemyKilled (Vector2 position, int[] enemyRewards)
    {
        score.AddScore(enemyRewards[0]);
        loot.DropLoot(enemyRewards[1], position);
        enemyCount--;
        if (enemyCount == 0)
        {
            FinishLevel();
        }
    }

    // Spawn enemies in the Level
    private void SpawnLevel()
    {
        Vector2 position = new Vector2();
        for (int i = 0; i < 3; i++)
        {
            position.x = Random.Range(-22, 22);
            position.y = Random.Range(-12, 12);
            SpawnEnemy(position);
        }
    }

    // Spawn an enemy by calling the Spawn Controller with the correspondent parameters
    private void SpawnEnemy(Vector2 position)
    {
        spawn.Summon(position);
        enemyCount++;
    }

    private void FinishLevel()
    {
        Debug.Log("Naisu!");
    }
}
