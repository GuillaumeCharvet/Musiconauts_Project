using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leds : MonoBehaviour
{
    public Sprite ledallumee;
    public Sprite ledeteinte;
    public SpriteRenderer led;
    public bool etatled;
    private int etattemp;
    void Start()
    {

    }

    public void resetleds()
    {
        etattemp = Random.Range(0,2);
        if (etattemp == 0)
        {
            etatled = false;
            led.sprite = ledeteinte;
        }
        else
        {
            etatled = true;
            led.sprite = ledallumee;
        }
    }
}
