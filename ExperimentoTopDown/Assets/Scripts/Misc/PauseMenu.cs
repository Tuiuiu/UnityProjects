using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool menuIsOpened = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuIsOpened)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        menuIsOpened = false;
        pauseMenuUI.SetActive(false);
        LevelControllers.pauseCount--;
        LevelControllers.TimeScaleCheck();
    }

    public void Pause()
    {
        menuIsOpened = true;
        pauseMenuUI.SetActive(true);
        LevelControllers.pauseCount++;
        LevelControllers.TimeScaleCheck();
    }
}
