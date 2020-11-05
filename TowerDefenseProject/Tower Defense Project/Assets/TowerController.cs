using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    //public GameObject enemies;

    protected List<GameObject> targets = new List<GameObject>();
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //enemies = GameObject.Find("EnemySpawner");
        InvokeRepeating("ShootBehaviour", 1.0f, 1f);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Instantiate()
    }

    protected virtual void ShootBehaviour()
    {
        Debug.Log("Shoot");
    }

    protected void ShootTarget(GameObject target)
    {
        GameObject clone = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        ProjectileController projectile = clone.GetComponent<ProjectileController>();
        projectile.SetTarget(target);
    }

    // Add new enemies to the list of targets when they enter the tower LoS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            targets.Add(collision.gameObject);
        }
    }

    // Remove enemies of the list of targets when they leave the tower LoS
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            targets.Remove(collision.gameObject);
        }
    }
}
