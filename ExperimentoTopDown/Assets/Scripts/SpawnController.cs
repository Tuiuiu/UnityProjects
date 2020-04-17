using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("summonTest", 1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void summonTest()
    {
        Summon(enemies[0]);
    }

    private void Summon(GameObject enemyToSummon)
    {
        GameObject clone = Instantiate(enemyToSummon, transform.position, enemyToSummon.transform.rotation);
        Enemy enemy = clone.GetComponent<Enemy>();
        enemy.setTarget(player);
    }
}
