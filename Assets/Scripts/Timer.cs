using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{

    private float t;
    private float targetTimer;

    public Timer(float timerTime)
    {
        targetTimer = timerTime;
    }

    public bool Evaluate()
    {
        t += Time.deltaTime;
        if(t >= targetTimer)
        {
            return true;
        }
        return false;
    }

}
