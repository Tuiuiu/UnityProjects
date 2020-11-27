using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemySpawnerController : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public PathController levelPath;
    private List<Transform> pathWaypoints;
    private IEnumerator coroutine;
    private int enemiesKilled = 0;
    public bool test = false;

    void OnEnable()
    {
        EnemyController.OnDeath += EnemiesKilledObserver;
    }

    private void OnDisable()
    {
        EnemyController.OnDeath -= EnemiesKilledObserver;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
        levelPath = GameObject.Find("Path").GetComponent<PathController>();
        pathWaypoints = levelPath.GetWaypoints();
        Invoke("LevelOne", 4.0f);
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

    void LevelOne()
    {
        int enemyIndex = 0;
        coroutine = SpawnEnemies(enemyIndex, pathWaypoints[0].transform.position, 2.0f, 0.5f, 40);
        StartCoroutine(coroutine);
    }

    IEnumerator SpawnEnemies(int index, Vector3 position, float delayTime, float repeatTime, int numberOfTimes)
    {
        yield return new WaitForSeconds(delayTime);
        for (int i = 0; i < numberOfTimes; i++)
        {
            GameObject clone = Instantiate(enemyPrefab[index], position, enemyPrefab[index].transform.rotation);
            PathFollower clonePath = clone.GetComponent<PathFollower>();
            clonePath.SetPath(pathWaypoints);
            yield return new WaitForSeconds(repeatTime);
        }
    }

    void EnemiesKilledObserver()
    {
        enemiesKilled++;
        if (enemiesKilled >= 40)
        {
            if (test == true)
            {
                SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
            }
        }
    }
}
