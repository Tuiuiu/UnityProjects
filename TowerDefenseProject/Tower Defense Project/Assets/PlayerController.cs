using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2.0f;
    public int currentWaypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[currentWaypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[currentWaypointIndex].transform.position)
        {
            currentWaypointIndex += 1;
        }

        if (currentWaypointIndex == waypoints.Length)
        {
            currentWaypointIndex = 0;
        }
    }
}
