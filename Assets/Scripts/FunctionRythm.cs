using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionRythm : MonoBehaviour
{
    private int numberOfCircles;
    private List<Vector2> listPosCircles;

    void Start()
    {
        numberOfCircles = Random.Range(3, 5);
        for( int i = 0; i < numberOfCircles; i++)
        {
            float xPos = Random.Range(0f, Screen.width);
            float yPos = Random.Range(0f, Screen.height);
            listPosCircles.Add(new Vector2(xPos, yPos));
        }

        //private Vector2 position = new Vector2;
    }
    void Update()
    {
        
    }
}
