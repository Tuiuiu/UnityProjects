using System.Collections;
using UnityEngine;

public class JumpSensor : MonoBehaviour
{
    private int colisionCount = 0;

    private float disableTimer;

    private void OnEnable()
    {
        colisionCount = 0;
    }

    public bool State()
    {
        if (disableTimer > 0)
            return false;
        return colisionCount > 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        colisionCount++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        colisionCount--;
    }

    void Update()
    {
        disableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        disableTimer = duration;
    }
}
