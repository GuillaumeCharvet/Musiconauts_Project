using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class refCursor : MonoBehaviour
{
    public Rigidbody2D rb;

    public Equalizer eq;

    public bool upwards;
    public bool cursorCollid;
    public bool cursorStop;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        upwards = (Random.Range(0, 1) < 0.5f);
    }

    public void Update()
    {
        if (transform.localPosition.y >= 6)
        {
            upwards = false;
        }
        else if (transform.localPosition.y <= -6)
        {
            upwards = true;
        }
    }

    public void FixedUpdate()
    {
        if (!cursorStop)
        {
            switch (upwards)
            {
                case true:
                    rb.velocity = Vector2.up * eq.speed * Time.deltaTime;
                    break;

                case false:
                    rb.velocity = Vector2.down * eq.speed * Time.deltaTime;
                    break;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Clicbouton()
    {
        if (!cursorStop)
        {
            cursorStop = true;
            eq.CursorSituation();
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
}