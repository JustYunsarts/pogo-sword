using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public enum CurrentState
    {
        Idle,
        Pogo,
        Jumping
        

    }

    public static CurrentState state = CurrentState.Idle;

    // Update is called once per frame
    void Update()
    {
    }
}
