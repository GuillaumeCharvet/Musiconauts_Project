using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursormove : MonoBehaviour
{
    public GameManager child;

    [Range(100, 300)]
    public float speed = 100;


    public Vector2 directionCursor;
    public bool cursorStop = false;
    public bool cursorCollid = false;
    public bool winCursor = false;
    public bool loseCursor = false;
    public bool rebootcursor = false;
    public bool rebootAffect = false;

    public Rigidbody2D rb;


public void Start()
    {
        child = FindObjectOfType<GameManager>();
        directionCursor = Vector2.up * speed;
    }



    public void FixedUpdate()
    {
        if (transform.localPosition.y >= 2.15)
        {
            directionCursor = Vector2.down;
        }
        if (transform.localPosition.y <= -1.85)
        {
            directionCursor = Vector2.up;
        }
        if (!cursorStop)
        {
            rb.velocity = directionCursor * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.velocity = Vector2.zero;
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
        if (!cursorStop)
        {
            if (cursorCollid)
            {
                cursorStop = true;
                WinCursor();
            }
            else { LoseCursor(); }
        }

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