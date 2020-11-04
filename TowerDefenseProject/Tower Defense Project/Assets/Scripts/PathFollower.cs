using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private List<Transform> waypoints;
    public float moveSpeed = 5.0f;
    public int currentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints != null)
        {
            Move();
        }
        else
        {
            waypoints = GameObject.Find("Path").GetComponent<PathController>().GetWaypoints();
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[currentWaypointIndex].transform.position)
        {
            currentWaypointIndex += 1;
        }

        if (currentWaypointIndex == waypoints.Count)
        {
            ReachEndOfPath();
        }
    }

    public void SetPath(List<Transform> path)
    {
        waypoints = path;
    }

    public void SetWaypointIndex(int index)
    {
        currentWaypointIndex = index;
    }

    private void ReachEndOfPath()
    {
        Destroy(gameObject);
    }
}
