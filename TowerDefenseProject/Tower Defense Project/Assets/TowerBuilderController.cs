using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilderController : MonoBehaviour
{
    public GameObject[] towersPrefabs;
    int selectedTower = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedTower = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTower = 1;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Instantiate(towersPrefabs[selectedTower], position, towersPrefabs[selectedTower].transform.rotation, transform);
        }
    }
}
