using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursormove : MonoBehaviour
{
    public GameManager child;

    [Range(0.000001f, 0.1f)]
    public float speed = 0.01f;


    public Vector2 directionCursor;
    public bool cursorStop = false;
    public bool cursorCollid = false;
    public bool winCursor = false;
    public bool loseCursor = false;
    public bool rebootcursor = false;
    public bool rebootAffect = false;


public void Start()
    {
        child = FindObjectOfType<GameManager>();
        directionCursor = Vector2.up * speed;
    }



    public void Update()
    {
        if (transform.localPosition.y >= 2.15)
        {
            directionCursor = Vector2.down * speed;
        }
        if (transform.localPosition.y <= -1.85)
        {
            directionCursor = Vector2.up * speed;
        }
        if (cursorStop == false)
        {
            transform.Translate(directionCursor);
        }
    }


    public void OnTriggerEnter2D(Collider2D collisionEnter)
    {
        if (collisionEnter.gameObject.CompareTag("WinZone"))
        {
            cursorCollid = true;   
        }
    }
    public void OnTriggerExit2D(Collider2D collisionExit)
    {
        if (collisionExit.gameObject.CompareTag("WinZone"))
        {
            cursorCollid = false; 
        }
    }

    public void Clicbouton()
    {
        if (cursorCollid)
        {
            cursorStop = true;
            WinCursor();
        }
        else { LoseCursor(); }

    }

    public void WinCursor()
    {
        Debug.Log("Win");

        child.WinCount() ;
    }
    public void LoseCursor()
    {
        Debug.Log("Loose");
        child.Lose();
    }
}