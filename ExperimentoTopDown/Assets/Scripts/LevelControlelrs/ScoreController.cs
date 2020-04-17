using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add points to player's score
    public void AddScore(int points)
    {
        score += points;
    }

    // Return actual score
    public int GetScore()
    {
        return score;
    }
}
