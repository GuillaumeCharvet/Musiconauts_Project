using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL_Variables : MonoBehaviour
{
    public Level_SO chosen_Level;

    [HideInInspector]
    public bool firstLaunch = true;

    public bool postProcess;

    private void Awake()
    {
        DDOL_Variables[] ddols = GameObject.FindObjectsOfType<DDOL_Variables>();

        if (ddols.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }
}