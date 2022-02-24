using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButton : MonoBehaviour
{
    private DDOL_Variables ddol;

    private bool canGo;

    private float lerpT;

    private float timer;

    [SerializeField]
    private Transition trans;

    private void Update()
    {
        if (canGo)
        {
            if (lerpT < 1)
            {
                lerpT += Time.deltaTime * 4;
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            if (timer < 2)
            {
                timer += Time.deltaTime;
            }
        }
    }

    public void SetCanGo()
    {
        if (timer >= 2)
        {
            canGo = true;
            trans.SetCanGo(-8.5f, 0);
            ddol = FindObjectOfType<DDOL_Variables>();

            if (ddol != null)
            {
                ddol.firstLaunch = false;
            }
        }
    }
}