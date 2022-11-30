using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFlags : MonoBehaviour
{
    //Script for level select
    /*Expected behavior:
     * When you are nearby, it shows a button prompt to show you it can be interacted with
     * when you hit the button, it prompts you to enter the level
     * When you press the interact button again, you enter the level
     * The Interact button is "E." 
     */

    [SerializeField]
    private string label;

    [SerializeField]
    private string LevelName;

    bool isCollided = false;

    Animator fadeAnim;
    GameObject manager;


    private void Awake()
    {
        fadeAnim = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isCollided)
        {
            fadeAnim.SetTrigger("fadeOut");
            SceneManager.LoadSceneAsync(LevelName);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isCollided)
        {
            isCollided = false;
        }
    }

    private void OnGUI() //called many times per frams
    {
        if (isCollided)
        {
            GUI.Box(new Rect(140, Screen.height - 50, Screen.width - 300, 120), (label));
        }
    }


}
