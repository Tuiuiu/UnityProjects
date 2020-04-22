using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score;
    public Text UIscore;
    private string scorePreamble = "SCORE: ";
    // Start is called before the first frame update
    void Start()
    {
        UIscore = GameObject.Find("UI_HighscoreText").GetComponent<Text>();
        score = 0;
        UIscore.text = scorePreamble + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add points to player's score
    public void AddScore(int points)
    {
        score += points;
        UIscore.text = scorePreamble + score;
    }

    // Return actual score
    public int GetScore()
    {
        return score;
    }
}
