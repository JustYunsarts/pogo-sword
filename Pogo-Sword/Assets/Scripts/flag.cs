using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameStateManager.state == GameStateManager.CurrentState.Key)
        {
            GameStateManager.state = GameStateManager.CurrentState.Win;
        }
    }
}
