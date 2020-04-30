using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score;
    private int nanobotsCount;
    public Text UIscore;
    public Text UInanobots;
    private string scorePreamble = "HIGHSCORE: ";
    private string nanobotsPreamble = "NANOBOTS: ";
    // Start is called before the first frame update
    void Start()
    {
        UIscore = GameObject.Find("UIHighscoreText").GetComponent<Text>();
        UInanobots = GameObject.Find("UINanobotsText").GetComponent<Text>();
        score = 0;
        nanobotsCount = 0;
        UIscore.text = scorePreamble + score;
        UInanobots.text = nanobotsPreamble + nanobotsCount;
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

    // Add one nanobot to player's nanobot counter
    public void AddNanobot()
    {
        nanobotsCount++;
        UInanobots.text = nanobotsPreamble + nanobotsCount;
    }
}
