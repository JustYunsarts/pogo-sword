using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public enum CurrentState
    {
        Play,
        Win,


    }

    public static CurrentState state = CurrentState.Play;
    public void AdvanceStage()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.sceneCountInBuildSettings-1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
