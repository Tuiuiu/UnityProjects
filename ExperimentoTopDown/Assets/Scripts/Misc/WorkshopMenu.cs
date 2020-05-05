using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopMenu : MonoBehaviour
{
    private bool menuIsOpened = false;
    public GameObject workshopMenuUI;
    // Update is called once per frame
    void Update()
    {
        /*
        if(menuIsOpened)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (LevelControllers.GameIsPaused && menuIsOpened)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }*/
    }

    public void Resume()
    {
        menuIsOpened = false;
        workshopMenuUI.SetActive(false);
        LevelControllers.pauseCount--;
        LevelControllers.TimeScaleCheck();
    }

    public void Pause()
    {
        if (!menuIsOpened)
        {
            menuIsOpened = true;
            workshopMenuUI.SetActive(true);
            LevelControllers.pauseCount++;
            LevelControllers.TimeScaleCheck();
        }
    }
}
