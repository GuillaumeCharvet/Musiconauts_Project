using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class knob : MonoBehaviour
{
    public bool etatknob;

    public SpriteRenderer knob_sprite;
    public Sprite knobhaut;
    public Sprite knobbas;

    [SerializeField]
    private Ampoules ampoule;
    [SerializeField]
    private gameKnob_Manager gameManager;

    public AudioSource click;

    void Start()
    {
        etatknob = false;
    }

    public void changeKnobStatus()
    {
        if (etatknob == false)
        {
            etatknob = true;
            click.Play();
            knob_sprite.sprite = knobhaut;
        }
        else
        {
            etatknob = false;
            click.Play();
            knob_sprite.sprite = knobbas;
        }
    }

    public void resetKnob()
    {
        etatknob = false;
    }
}
