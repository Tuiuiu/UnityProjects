using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilderController : MonoBehaviour
{
    public GameObject[] towersPrefabs;
    private GameObject selectedTower;
    private GameObject buildProjector;
    private bool isBuilding = false;
    private bool overTower = false;
    private SpriteRenderer buildingSR;

    private Ray mouseRay;
    private RaycastHit2D hitObj;
    private int builtTowersMask = 1 << 8;
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isBuilding == true)
            {
                buildingSR.enabled = false;
                isBuilding = false;
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
            checkRay(mouseRay, hitObj);   
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isBuilding)
            {
                if (!overTower)
                {
                    Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    position.z = 0;
                    position = SnapToGrid(position);
                    Instantiate(selectedTower, position, selectedTower.transform.rotation, transform);
                    isBuilding = false;
                    overTower = false;
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

    private void checkRay(Ray ray, RaycastHit2D hit)
    {
        // Raycast, looking for any collider in Layer 8
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, builtTowersMask);

        // If no colliders were hit
        if (hit.collider == null)
        {
            //Debug.Log("NAO BATEU");
            if (overTower)
            {
                overTower = false;
                Color spriteColor = Color.green;
                spriteColor.a = 0.8f;
                buildingSR.color = spriteColor;
            }
        }
        // If hit at least one collider
        else
        {
            if (!overTower)
            {
                overTower = true;
                Color spriteColor = Color.red;
                spriteColor.a = 0.8f;
                buildingSR.color = spriteColor;
            }
        }
    }
}
