using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelDisplayer : MonoBehaviour
{
    public Text enemiesKilledText;
    public Text levelTimer;

    private int enemiesKilled = 0;
    private float timeElapsed = 0f;

    void OnEnable()
    {
        EnemyController.OnDeath += ShowInfo;
    }

    private void OnDisable()
    {
        EnemyController.OnDeath -= ShowInfo;
    }
    // Start is called before the first frame update
    void Start()
    {
        //enemiesKilledText = gameObject.transform.Find("Text").GetComponent<Text>();
        enemiesKilledText.text = "Inimigos Mortos: " + enemiesKilled;
        //levelTimer = gameObject.transform.Find("Timer").GetComponent<Text>();
        levelTimer.text = "00:00";

    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        UpdateTimer();
    }

    void ShowInfo()
    {
        enemiesKilled++;
        enemiesKilledText.text = "Inimigos Mortos: " + enemiesKilled;
        Debug.Log("Inimigo Morto");
    }

    void UpdateTimer()
    {
        float minutes = Mathf.FloorToInt(timeElapsed / 60);
        float seconds = Mathf.FloorToInt(timeElapsed % 60);

        levelTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
