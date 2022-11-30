using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    private GameObject winMenu;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        winMenu = this.gameObject.transform.GetChild(1).gameObject;
        winMenu.SetActive (false);
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameStateManager.state == GameStateManager.CurrentState.Win)
        {
            winMenu.SetActive(true);
            isActive = true;
        }
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) && isActive)
        {
            winMenu.SetActive(false);
            isActive = false;
            GameStateManager.state = GameStateManager.CurrentState.Play;
            PlayerPrefs.SetFloat("respawnX", 0);
            PlayerPrefs.SetFloat("respawnY", 0);
            SceneManager.LoadScene(1); //load hub world level select
        }
    }
}
