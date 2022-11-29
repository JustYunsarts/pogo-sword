using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameStateManager.state = GameStateManager.CurrentState.Win;
    }
}
