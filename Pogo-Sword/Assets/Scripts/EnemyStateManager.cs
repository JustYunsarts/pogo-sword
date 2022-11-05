using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public enum CurrentState
    {
        Idle,
        Patrolling,
        Dead


    }

    public static CurrentState state = CurrentState.Idle;

    // Update is called once per frame
    void Update()
    {
    }
}
