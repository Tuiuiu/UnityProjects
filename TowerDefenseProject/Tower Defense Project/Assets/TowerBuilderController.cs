using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilderController : MonoBehaviour
{
    public GameObject[] towersPrefabs;
    GameObject selectedTower;
    GameObject buildProjector;
    bool isBuilding = false;
    SpriteRenderer buildingSR;
    // Start is called before the first frame update
    void Start()
    {
        buildProjector = transform.GetChild(0).gameObject;
        buildingSR = buildProjector.GetComponent<SpriteRenderer>();
        buildingSR.enabled = false;
        selectedTower = towersPrefabs[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedTower = towersPrefabs[0];
            LoadSprite();
            buildingSR.enabled = true;
            isBuilding = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTower = towersPrefabs[1];
            LoadSprite();
            buildingSR.enabled = true;
            isBuilding = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (isBuilding)
            {
                Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0;
                position = SnapToGrid(position);
                Instantiate(selectedTower, position, selectedTower.transform.rotation, transform);
                isBuilding = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isBuilding == true)
            {
                isBuilding = false;
            }
        }

        if (isBuilding)
        {
            Vector3 projectedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            projectedPos.z = -2;
            buildProjector.transform.position = SnapToGrid(projectedPos);
        }
    }

    private void LoadSprite()
    {
        buildingSR.sprite = selectedTower.GetComponent<SpriteRenderer>().sprite;
        Color spriteColor = buildingSR.color;
        spriteColor.a = 0.4f;
        buildingSR.color = spriteColor;
    }

    private Vector3 SnapToGrid(Vector3 pos)
    {
        Vector3 finalPos = pos;
        finalPos.x = Mathf.RoundToInt(finalPos.x);
        finalPos.y = Mathf.RoundToInt(finalPos.y);
        return finalPos;
    }
}
