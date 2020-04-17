using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float life = 10.0f;
    private float speed = 5.0f;

    private int score = 100;
    private int rewardTier = 1;

    public GameObject target;
    public LevelControllers level;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        body.velocity = direction.normalized * speed;
    }

    public void takeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        int[] rewards = { score, rewardTier };
        level.EnemyKilled(transform.position, rewards);
        Destroy(gameObject);
    }

    public void setTarget(GameObject targetReference)
    {
        target = targetReference;
    }
    
    public void setLevel(LevelControllers levelReference)
    {
        level = levelReference;
    }
}
