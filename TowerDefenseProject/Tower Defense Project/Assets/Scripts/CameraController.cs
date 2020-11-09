using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    bool rightClickPressed = false;
    Vector3 mousePosOld = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rightClickPressed = true;
            mousePosOld = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            rightClickPressed = false;
        }

        if (rightClickPressed)
        {
            Vector3 newPos = Input.mousePosition;
            Vector3 delta = Camera.main.ScreenToWorldPoint(newPos) - Camera.main.ScreenToWorldPoint(mousePosOld);
            transform.position = transform.position - delta;
            mousePosOld = newPos;
        }
    }
}
