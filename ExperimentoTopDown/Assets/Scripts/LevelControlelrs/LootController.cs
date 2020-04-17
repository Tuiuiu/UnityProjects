using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropLoot(int rewardTier, Vector2 position)
    {
        Debug.Log("Dropou um item de tier " + rewardTier + "na posição (" + position.x + ", " + position.y + ").");
    }
}
