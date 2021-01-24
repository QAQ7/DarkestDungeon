using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    // Start is called before the first frame update
    public int round;
    public States state;

    public void init(States state,int round) {
        this.state = state;
        this.round = round;
    }

    public bool action() {
        Debug.Log(this.gameObject.name+" stateRound:"+round);
        round--;
        if (round == 0)
        {
            return false;
        }
        else {
            return true;
        }
    }
}

public enum States
{
    Bleeding,
    Unactionable
}
