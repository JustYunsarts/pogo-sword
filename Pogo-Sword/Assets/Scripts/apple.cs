using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameStateManager.state = GameStateManager.CurrentState.Key;
        Destroy(obj);
    }
}
