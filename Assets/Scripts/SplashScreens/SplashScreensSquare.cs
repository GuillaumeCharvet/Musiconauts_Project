using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreensSquare : MonoBehaviour
{
    public Transform _menu;

    [SerializeField]
    private Transform square;

    [SerializeField]
    private List<Vector3> squareScales;

    [SerializeField]
    private List<float> scaleTimes;

    private SpriteRenderer sr;

    private float timer, lerpT;

    private int index;

    public bool canGo;

    private float alphaSquare;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canGo)
        {
            if (timer < scaleTimes[scaleTimes.Count - 1])
            {
                SquareAnimation();
            }
        }
    }

    private void SquareAnimation()
    {
        if (index < squareScales.Count - 2)
        {
            if (lerpT >= 1)
            {
                lerpT = 0;
                index++;
            }
            else
            {
                timer += Time.deltaTime;
                lerpT = (scaleTimes[index] - timer) / (scaleTimes[index] - scaleTimes[index + 1]);

                Debug.Log(lerpT);

                square.localScale = Vector3.Lerp(squareScales[index], squareScales[index + 1], lerpT);
            }
        }
        else
        {
            DissolveSquare();
        }
    }

    private void DissolveSquare()
    {
        _menu.gameObject.SetActive(true);

        alphaSquare -= Time.deltaTime * 0.7f;

        sr.color = new Vector4(1, 1, 1, alphaSquare);
    }
}