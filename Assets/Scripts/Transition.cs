using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private bool canGo;

    private float posX, lerpT, departX, arriveeX;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (canGo)
        {
            StartTransition();
        }
    }

    public void SetCanGo(float depart, float arrivee)
    {
        canGo = true;
        departX = depart;
        arriveeX = arrivee;
    }

    private void StartTransition()
    {
        if (lerpT < 1)
        {
            lerpT += Time.deltaTime * 4;
            posX = Mathf.Lerp(departX, arriveeX, lerpT);
            transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        }
        else
        {
            lerpT = 0;
            canGo = false;
        }
    }
}