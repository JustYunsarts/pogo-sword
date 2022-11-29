using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;


    bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = this.gameObject.transform.GetChild(0).gameObject;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                resumeGame();
            }
            else if(!isPaused)
            {
                pauseGame();
            }
        }
    }

    void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resumeGame()
    {
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    public void restartLevel()
    {
        Time.timeScale = 1f;

        PlayerPrefs.SetFloat("respawnX", 0);
        PlayerPrefs.SetFloat("respawnY", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void loadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
