using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public enum CurrentState
    {
        Start,
        Play,
        Clear,
        Over

    }

    public static CurrentState state = CurrentState.Start;

    // Update is called once per frame
    void Update()
    {
    }

    public void AdvanceStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
