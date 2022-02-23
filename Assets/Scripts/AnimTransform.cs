using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTransform : MonoBehaviour
{
    [SerializeField]
    private string animationName;

    [SerializeField]
    private List<Vector3> squareScales;

    [SerializeField]
    private List<float> scaleTimes;

    private float timer, lerpT;

    private int index;

    public bool canGo;

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

                transform.localScale = Vector3.Lerp(squareScales[index], squareScales[index + 1], lerpT);
            }
        }
        else
        {
            canGo = false;
            index = 0;
            lerpT = 0;
            timer = 0;
        }
    }

    public void SetCanGo()
    {
        canGo = true;
    }
}