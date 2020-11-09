using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public PathController levelPath;
    private List<Transform> pathWaypoints;

    // Start is called before the first frame update
    void Start()
    {
        levelPath = GameObject.Find("Path").GetComponent<PathController>();
        pathWaypoints = levelPath.GetWaypoints();
        InvokeRepeating("SpawnEnemy", 1.0f, 0.5f);
        Invoke("SpawnBoss", 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        GameObject clone = Instantiate(enemyPrefab[0], pathWaypoints[0].transform.position, enemyPrefab[0].transform.rotation, transform);
        PathFollower clonePath = clone.GetComponent<PathFollower>();
        clonePath.SetPath(pathWaypoints);
    }

    void SpawnBoss()
    {
        GameObject clone = Instantiate(enemyPrefab[1], pathWaypoints[0].transform.position, enemyPrefab[1].transform.rotation, transform);
        PathFollower clonePath = clone.GetComponent<PathFollower>();
        clonePath.SetPath(pathWaypoints);
    }
}
