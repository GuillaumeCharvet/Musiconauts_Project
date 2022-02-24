using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ampoules : MonoBehaviour
{
    public bool etatampoule;

    public SpriteRenderer spriteAmpoule;
    public Sprite ampallumee;
    public Sprite ampeteinte;

    [SerializeField]
    private knob knob;

    private void Update()
    {
        etatampoule = knob.etatknob;
    }

    public void changeAmpoule()
    {
        if (etatampoule == false)
        {
            spriteAmpoule.sprite = ampallumee;
        }
        else
        {
            spriteAmpoule.sprite = ampeteinte;
        }
    }
}
