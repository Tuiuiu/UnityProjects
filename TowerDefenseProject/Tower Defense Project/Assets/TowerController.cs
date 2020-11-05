using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("EnemySpawner");
        InvokeRepeating("Shoot", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Instantiate()
    }

    void Shoot()
    {
        int count = enemies.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject clone = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            ProjectileController projectile = clone.GetComponent<ProjectileController>();
            projectile.SetTarget(enemies.transform.GetChild(i).gameObject);
        }
    }
}
