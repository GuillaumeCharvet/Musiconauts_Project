using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TextMeshToSortingLayer : MonoBehaviour
{
    [SerializeField]
    private TextMesh TM;

    [SerializeField]
    private string sortingLayer;

    private void Start()
    {
        if (TM != null)
        {
            TM.GetComponent<Renderer>().sortingLayerName = sortingLayer;
        }
        else
        {
            GetComponent<Renderer>().sortingLayerName = sortingLayer;
        }
    }
}