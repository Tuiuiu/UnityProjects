using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject player;
    public LevelControllers level;
    
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SummonTest", 1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SummonTest()
    {
        Summon(enemies[0]);
    }

    private void Summon(GameObject enemyToSummon)
    {
        GameObject clone = Instantiate(enemyToSummon, transform.position, enemyToSummon.transform.rotation);
        Enemy enemy = clone.GetComponent<Enemy>();
        enemy.setTarget(player);
        enemy.setLevel(level);
    }

    private void Summon(GameObject enemyToSummon, Vector2 spawnPos)
    {
        GameObject clone = Instantiate(enemyToSummon, spawnPos, enemyToSummon.transform.rotation);
        Enemy enemy = clone.GetComponent<Enemy>();
        enemy.setTarget(player);
        enemy.setLevel(level);
    }

    public void Summon(Vector2 spawnPos)
    {
        GameObject clone = Instantiate(enemies[0], spawnPos, enemies[0].transform.rotation);
        Enemy enemy = clone.GetComponent<Enemy>();
        //enemy.setTarget(player);
        enemy.setLevel(level);
    }
}
