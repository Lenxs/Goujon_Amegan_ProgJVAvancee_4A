using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField]private GameObject pauseMenu;

    void Update()
    {
        if(Input.GetKeyDown("escape") == true)
        {
            swapPauseMode();
        }
    }

    public void swapPauseMode()
    {
        if(isPaused == false)
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            isPaused = true;

        }else if(isPaused == true)
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            isPaused = false;
        }
    }


}
