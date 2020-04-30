using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nanobots : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D body;
    private bool playerIsClose = false;
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsClose)
        {
            Vector3 direction = player.transform.position - transform.position;
            body.velocity = direction.normalized * speed;
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 4)
            {
                playerIsClose = true;
            }
        }
    }

    public void SetPlayer(GameObject playerRef)
    {
        player = playerRef;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
