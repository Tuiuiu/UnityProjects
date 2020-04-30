using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public GameObject nanobots;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropLoot(int rewardTier, Vector3 position)
    {
        if (Random.value > 0.7)
        {
            int numberOfRewards = Random.Range(rewardTier, rewardTier + 3);
            for (int i = 0; i < numberOfRewards; i++)
            {
                Vector3 offset = new Vector3(Random.value, Random.value, 0);
                GameObject clone = Instantiate(nanobots, position + offset, nanobots.transform.rotation);
                clone.GetComponent<Nanobots>().SetPlayer(player);
                
            }
            Debug.Log("Dropou " + numberOfRewards + " nanobôs na posição (" + position.x + ", " + position.y + ").");
        }
        else
        {
            Debug.Log("Que pena, não dropou nada!");
        }
    }
}
