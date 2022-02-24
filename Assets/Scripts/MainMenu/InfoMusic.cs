using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoMusic : MonoBehaviour
{
    public bool setInfoMusique = false;
    private AudioMainMenu amm;

    [SerializeField]
    private Transition trans;

    public Level_SO lvl;

    private bool transition, transitionPassee;

    private float lerpT;

    public void StartGame()
    {
        DDOL_Variables ddol = FindObjectOfType<DDOL_Variables>();

        ddol.chosen_Level = lvl;

        transition = true;
    }

    private void Update()
    {
        if (transition)
        {
            Transition();
        }
    }

    private void Transition()
    {
        if (lerpT >= 1)
        {
            SceneManager.LoadScene("MiniGames");
        }
        else
        {
            lerpT += Time.deltaTime * 4;
        }

        if (!transitionPassee)
        {
            transitionPassee = true;
            trans.SetCanGo(8.5f, 0);
        }
    }

    public void UnactiveInfoMusique()
    {
        setInfoMusique = false;
    }

    public void AfficheInfo(bool a)
    {
        amm = FindObjectOfType<AudioMainMenu>();

        if (setInfoMusique == false)
        {
            amm.SoundBoutonMenu();
            setInfoMusique = true;
            //Debug.LogWarning(informationMusique);
            gameObject.SetActive(a);
        }
    }
}