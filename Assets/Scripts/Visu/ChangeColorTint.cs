using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorTint : MonoBehaviour
{
    public GameManager gm;
    public SpriteRenderer sr;

    public bool transitionCouleurFinie = false;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.currentColorTint != gm.targetColorTint)
        {
            sr.color = gm.globalColorTint;
            if ((Color)gm.globalColorTint == gm.vectorMatchingTints[(int)gm.targetColorTint])
            {
                transitionCouleurFinie = true;
            }
        }
    }
}
