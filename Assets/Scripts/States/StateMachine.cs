using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State State;

    public void SetState(State value)
    {
        State = value;
        StartCoroutine(value.Enter());
    }
}
