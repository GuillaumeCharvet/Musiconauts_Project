using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryCondition : MonoBehaviour
{
    private float a;
    private float b;
    private FunctionDisplay child;
    public Text text;
    [SerializeField]
    private Scrollbar scrollbar_a, scrollbar_b;
    private float victory_threshold_a = 0.035f;
    private float victory_threshold_b = 0.035f;

    private GameManager game_manager;
    private void Awake()
    {
        game_manager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        child = transform.Find("WindowGraph").GetComponent<FunctionDisplay>();
        text = transform.Find("Text").GetComponent<Text>();
        //scrollbar = transform.Find("Scrollbar").GetComponent<Scrollbar>();

        text.enabled = false;
    }

    void Update()
    {
        if (!child.game_won)
        {
            //if (Mathf.Abs(child.parameter_a - child.random_start_value_a) / 6.28f + Mathf.Abs(child.parameter_b - child.random_start_value_b) < victory_threshold)
            if (Mathf.Abs(child.parameter_a - child.random_start_value_a) / 6.28f < victory_threshold_a  && Mathf.Abs(child.parameter_b - child.random_start_value_b) < victory_threshold_b
                && Mathf.Abs(child.parameter_a - child.random_start_value_a) / 6.28f + Mathf.Abs(child.parameter_b - child.random_start_value_b) < (victory_threshold_a + victory_threshold_b) * 0.6f)
            {
                text.enabled = true;
                child.game_won = true;
                StartCoroutine(game_manager.Win());
                scrollbar_a.enabled = false;
                scrollbar_b.enabled = false;
            }
        }
    }

    private void Reset()
    {
        child.game_won = false;
        text.enabled = false;
        scrollbar_a.enabled = true;
        scrollbar_b.enabled = true;
    }
}
