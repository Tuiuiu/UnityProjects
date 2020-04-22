using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life = 10;
    private float speed = 5.0f;

    private int score = 100;
    private int rewardTier = 1;

    public GameObject target;
    public LevelControllers level;
    private Rigidbody2D body;
    public HealthBar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        healthbar.SetMaxHealth(life);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        body.velocity = direction.normalized * speed;
    }

    public void takeDamage(int damage)
    {
        life -= damage;
        healthbar.SetHealth(life);
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

    public void resolveEffect(ShotEffect effect)
    {
        Debug.Log("Dealing with effect " + effect.type + " of value " + effect.value + " and duration " + effect.duration);
        switch (effect.type)
        {
            case EffectType.PhysicalDamage:
            case EffectType.ExplosionDamage:
                takeDamage(effect.value);
                break;
            default:
                Debug.Log("Default case! Your effect went wrong");
                break;
        }
    }
}
