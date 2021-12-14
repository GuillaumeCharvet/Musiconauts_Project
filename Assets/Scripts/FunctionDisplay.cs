using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionDisplay : MonoBehaviour
{

    private float deltaT = 0.2f;
    private int timeStep = 80;
    private List<float> valueList;

    public float parameter_a = 0f;
    public float parameter_b = 0f;
    private Color color_trace1 = new Color (0,1,0, 1f);
    private Color color_trace2 = new Color (0.2f,0.35f,0.2f, 1f);

    public float random_start_value_a;
    public float random_start_value_b;

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        random_start_value_a = Random.Range(0f, 6.28f);
        random_start_value_b = Random.Range(0.3f, 1f);
        //List<float> valueList = new List<float>() {5, 98, 56, 45, 30, 22, 14, 10, 15, 13, 12, 11, 37};
    }

    private void Update()
    {
        Clear();
        valueList = CreateList(random_start_value_a, random_start_value_b);
        ShowGraph(valueList, color_trace2, 11f);
        //parameter_a += 0.1f;
        valueList = CreateList(parameter_a, parameter_b);
        ShowGraph(valueList, color_trace1, 3f);
    }

    private void Clear()
    {
        foreach (Transform child in graphContainer.transform)
        {
            if (child.name != "background"){GameObject.Destroy(child.gameObject);};
        }
    }

    public void Change_Value_a(float button_value)
    {
        parameter_a = button_value * 6.28f;
    }
    public void Change_Value_b(float button_value)
    {
        parameter_b = 0.1f + button_value * .9f;
    }

    private List<float> CreateList(float parameter_a, float parameter_b)
    {
        List<float> valueList = new List<float>() {};
        for (int i = 0; i < timeStep; i++)
        {
            valueList.Add( 50 + 25 * Mathf.Sin(deltaT*i + parameter_a) + parameter_b * 16 * Mathf.Sin(2f*deltaT*i));
        }
        return valueList;
    }
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(0, 0);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }
    private void ShowGraph(List<float> valueList, Color color, float sizeDelta)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 12f;

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = (i + 1) * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, color, sizeDelta);
            }
            lastCircleGameObject = circleGameObject;
        }
    }
    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Color color, float sizeDelta)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, sizeDelta);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, Vector2.SignedAngle(new Vector2(1, 0), dir));
    }
}
