using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInterfaceController : MonoBehaviour
{
    private Ray mouseRay;
    private RaycastHit2D hitObj;
    private GameObject selectedTower = null;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Globals.state == "idle")
            {
                // Mask to capture Built Towers colliders
                int builtTowersMask = 1 << 8;
                mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Raycast, looking if this tile already have a tower built
                hitObj = Physics2D.Raycast(mouseRay.origin, mouseRay.direction, Mathf.Infinity, builtTowersMask);
                if (hitObj.collider != null)
                {
                    selectedTower = hitObj.collider.transform.parent.gameObject;
                    Globals.state = "selection";
                    image.sprite = selectedTower.GetComponent<SpriteRenderer>().sprite;
                    image.enabled = true;
                }
            }

            if (Globals.state == "selection")
            {
                int builtTowersMask = 1 << 8;
                mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Raycast, looking if this tile already have a tower built
                hitObj = Physics2D.Raycast(mouseRay.origin, mouseRay.direction, Mathf.Infinity, builtTowersMask);
                if (hitObj.collider == null)
                {
                    Globals.state = "idle";
                    image.enabled = false;
                }
                else
                {
                    selectedTower = hitObj.collider.transform.parent.gameObject;
                    image.sprite = selectedTower.GetComponent<SpriteRenderer>().sprite;
                    image.enabled = true;
                }

            }
        }
    }
}
