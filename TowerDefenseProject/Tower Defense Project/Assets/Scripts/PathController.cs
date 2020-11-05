using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    private List<Transform> waypoints = new List<Transform>();
    //Transform[] waypoints;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints.Add(transform.GetChild(i));
        }
        PrintAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrintAll()
    {
        foreach (Transform point in waypoints)
        {
            Debug.Log(point);
        }
    }

    public List<Transform> GetWaypoints()
    {
        return waypoints;
    }
}
