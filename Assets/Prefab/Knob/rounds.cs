using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rounds : MonoBehaviour
{
    public SpriteRenderer spriteRound;
    public Sprite roundallumee;
    public Sprite roundeteinte;

    public AudioSource son;

    public void changeRound()
    {
        spriteRound.sprite = roundallumee;
        son.Play();
    }
}
