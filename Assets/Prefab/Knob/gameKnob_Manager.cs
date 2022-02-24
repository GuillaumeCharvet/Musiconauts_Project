using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameKnob_Manager : MonoBehaviour
{
    public int rounds = 1;
    public int totalrounds = 3;
    public rounds round1;
    public rounds round2;
    public rounds round3;

    private int total_led;
    private int led_round_precedent = 0;
    private int _egaux;

    [SerializeField]
    private knob[] knob;

    [SerializeField]
    private Leds[] leds;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        rounds = 0;
        for (int m = 0; m < leds.Length; m++)
        {
            leds[m].resetleds();
        }
        countLeds();
        compareAmpLed();
    }

    private void finround()
    {
        rounds += 1;
        if (rounds < totalrounds)
        {
            if (rounds == 1)
            {
                round1.changeRound();
            }
            else if (rounds == 2)
            {
                round2.changeRound();
            }
            total_led = 0;
            for (int m = 0; m < leds.Length; m++)
            {
                leds[m].resetleds();
            }
            compareAmpLed();
            countLeds();
        }
        else if (rounds == 3)
        {
            StartCoroutine(gm.Win());
        }
    }

    public void compareAmpLed()
    {
        _egaux = 0;
        for (int l = 0; l < leds.Length; l++)
        {
            bool amp = knob[l].etatknob;
            bool led = leds[l].etatled;
            if (amp == led)
            {
                _egaux += 1;
            }
        }
        if (_egaux == 4)
        {
            _egaux = 0;
            finround();
        }
    }

    private void countLeds()
    {
        total_led = 0;
        for (int i = 0; i < 4; i++)
        {
            if (leds[i].etatled == true)
            {
                total_led += 1;
            }
        }
        if (total_led < 2 || total_led == led_round_precedent)
        {
            total_led = 0;
            for (int j = 0; j < leds.Length; j++)
            {
                leds[j].resetleds();
            }
            countLeds();
        }
        if (total_led < 2)
        {
            led_round_precedent = 2;
        }
        else
        {
            led_round_precedent = total_led;
        }
    }
}