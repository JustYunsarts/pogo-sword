using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //Debug.Log(state);
    }
}
