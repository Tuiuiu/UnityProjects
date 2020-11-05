using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    //public GameObject enemies;

    List<GameObject> targets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //enemies = GameObject.Find("EnemySpawner");
        InvokeRepeating("ShootAll", 1.0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //Instantiate()
    }

    private void ShootAll()
    {
        for (int i = targets.Count-1; i >= 0; i--)
        {
            if (targets[i] == null) {
                targets.RemoveAt(i);
            }
            else
            {
                ShootTarget(targets[i]);
            }
        }
    }

    private void ShootTarget(GameObject target)
    {
        GameObject clone = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        ProjectileController projectile = clone.GetComponent<ProjectileController>();
        projectile.SetTarget(target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            targets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bool teste;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            teste = targets.Remove(collision.gameObject);
        }
    }
}
