using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryCondition : MonoBehaviour
{

    private float a;
    private float b;
    private FunctionDisplay child;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.Find("WindowGraph").GetComponent<FunctionDisplay>();
        text = transform.Find("Text").GetComponent<Text>();

        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(child.parameter_a - child.random_start_value_a)/6.28f + Mathf.Abs(child.parameter_b - child.random_start_value_b) < 0.01f)
        {
            text.enabled = true;
        }

    }
}
