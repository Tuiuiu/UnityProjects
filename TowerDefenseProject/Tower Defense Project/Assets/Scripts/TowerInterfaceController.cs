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
    private bool safe = true;
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
                // Show Tower in interface and make it selected
                if (hitObj.collider != null)
                {
                    Globals.state = "selection";
                    selectedTower = hitObj.collider.transform.parent.gameObject;
                    EnableSelection();
                    safe = false;
                }
            }
            else if (Globals.state == "selection")
            {
                int builtTowersMask = 1 << 8;
                mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Raycast, looking if this tile already have a tower built
                hitObj = Physics2D.Raycast(mouseRay.origin, mouseRay.direction, Mathf.Infinity, builtTowersMask);
                // Click outside of a tower, hiding the selection info
                if (hitObj.collider == null)
                {
                    Globals.state = "idle";
                    ExitSelection();
                    safe = true;
                }
                // Another Tower selected, refresh displayed info
                else
                {
                    GameObject collidedTower = hitObj.collider.transform.parent.gameObject;
                    if (selectedTower != collidedTower)
                    {
                        ExitSelection();
                        selectedTower = collidedTower;
                        EnableSelection();
                        safe = false;
                    }
                }

            }
        }

        if (Globals.state != "selection" && !safe)
        {
            ExitSelection();
            safe = true;
        }
    }

    void EnableSelection()
    {
        image.sprite = selectedTower.GetComponent<SpriteRenderer>().sprite;
        image.enabled = true;
        selectedTower.GetComponent<TowerController>().ShowSelection();
    }

    void ExitSelection()
    {
        image.enabled = false;
        selectedTower.GetComponent<TowerController>().HideSelection();
    }
}
