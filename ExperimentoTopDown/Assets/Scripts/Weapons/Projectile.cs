using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector2 direction = Vector2.right;
    protected Rigidbody2D body;
    protected int damage = 5;
    protected float speed = 20.0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        body.velocity = direction * speed;

        if (transform.position.x > 22 || transform.position.x < -22 || transform.position.y > 12 || transform.position.y < -12)
            Destroy(gameObject);
    }

    public void setDirection(Vector2 newDir)
    {
        direction = newDir;
    }

    public void setSpeed(float newSpd)
    {
        speed = newSpd;
    }

    public void setDamage(int newDmg)
    {
        damage = newDmg;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("ENTROU");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            OnHitConfirmed(enemy);
        }
    }

    protected virtual void OnHitConfirmed(Enemy enemy)
    {
        Debug.Log("Projectile hit an enemy");
    }
}

