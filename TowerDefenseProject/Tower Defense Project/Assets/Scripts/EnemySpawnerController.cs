using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public PathController levelPath;
    private List<Transform> pathWaypoints;

    // Start is called before the first frame update
    void Start()
    {
        levelPath = GameObject.Find("Path").GetComponent<PathController>();
        pathWaypoints = levelPath.GetWaypoints();
        InvokeRepeating("SpawnEnemy", 1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        GameObject clone = Instantiate(enemyPrefab, pathWaypoints[0].transform.position, enemyPrefab.transform.rotation, transform);
        PathFollower clonePath = clone.GetComponent<PathFollower>();
        clonePath.SetPath(pathWaypoints);
    }
}
