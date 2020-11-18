using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilderController : MonoBehaviour
{
    public GameObject[] towersPrefabs;
    private GameObject selectedTower;
    private GameObject buildProjector;
    private bool isBuilding = false;
    private bool validTile = true;
    private SpriteRenderer buildingSR;

    private Ray mouseRay;
    private RaycastHit2D hitObj;
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
            Globals.state = "building";
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTower = towersPrefabs[1];
            LoadSprite();
            buildingSR.enabled = true;
            isBuilding = true;
            Globals.state = "building";
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isBuilding == true)
            {
                buildingSR.enabled = false;
                isBuilding = false;
                Globals.state = "idle";
            }
        }

        if (isBuilding)
        {
            Vector3 projectedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            projectedPos.z = -1;
            buildProjector.transform.position = SnapToGrid(projectedPos);

            // Updates Ray
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Check if it's hitting any tower 
            CheckRay(mouseRay, hitObj);   
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isBuilding)
            {
                if (validTile)
                {
                    Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    position.z = 0;
                    position = SnapToGrid(position);
                    Instantiate(selectedTower, position, selectedTower.transform.rotation, transform);
                    isBuilding = false;
                    Globals.state = "idle";
                    validTile = true;
                    buildingSR.enabled = false;
                }
            }
        }
    }

    private void LoadSprite()
    {
        buildingSR.sprite = selectedTower.GetComponent<SpriteRenderer>().sprite;
        Color spriteColor = Color.green;
        spriteColor.a = 0.8f;
        buildingSR.color = spriteColor;
    }

    private Vector3 SnapToGrid(Vector3 pos)
    {
        Vector3 finalPos = pos;
        finalPos.x = Mathf.RoundToInt(finalPos.x);
        finalPos.y = Mathf.RoundToInt(finalPos.y);
        return finalPos;
    }

    private void CheckRay(Ray ray, RaycastHit2D hit)
    {
        // Mask to capture Built Towers colliders
        int builtTowersMask = 1 << 8;
        // Mask to capture Buildable Tiles colliders
        int buildTerrainMask = 1 << 9;
        
        // Check if it's a viable terrain
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, buildTerrainMask);
        // If it does not hit a terrain collider
        if (hit.collider == null)
        {
            if (validTile)
            {
                validTile = false;
                Color spriteColor = Color.red;
                spriteColor.a = 0.8f;
                buildingSR.color = spriteColor;
            }
        }
        // If it's a buildable terrain
        else
        {
            // Raycast, looking if this tile already have a tower built
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, builtTowersMask);
            // If it's an empty tile, then a tower can be placed here
            if (hit.collider == null)
            {
                if (!validTile)
                {
                    validTile = true;
                    Color spriteColor = Color.green;
                    spriteColor.a = 0.8f;
                    buildingSR.color = spriteColor;
                }
            }
            // If there's a tower, then it's an invalid tile
            else
            {
                if (validTile)
                {
                    validTile = false;
                    Color spriteColor = Color.red;
                    spriteColor.a = 0.8f;
                    buildingSR.color = spriteColor;
                }
            }
        }
    }
}
