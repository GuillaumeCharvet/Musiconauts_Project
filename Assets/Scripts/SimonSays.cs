using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MonoBehaviour
{
    public SpriteRenderer srAffiche;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
    }


    public void AppuieBouton(string truc)
    {
        GameObject[] gos = FindObjectsOfType<GameObject>();
        foreach (GameObject go in gos)
        {
            if (go.transform.name.Contains("Bouton"))
            {
                switch (go.transform.name)
                {
                    case "Bouton1":
                        //

                        break;
                }
            }
        }
        //srAffiche.sprite =  
    }

}
