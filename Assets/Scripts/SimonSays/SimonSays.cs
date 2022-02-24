using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum colorCombi
{
    red, green, blue, yellow
}

public class SimonSays : MonoBehaviour
{
    private GameManager gm;

    [SerializeField]
    private SpriteRenderer bouton1, bouton2, bouton3, bouton4;

    [SerializeField]
    private Sprite bouton1oui, bouton2oui, bouton3oui, bouton4oui, bouton1non, bouton2non, bouton3non, bouton4non;

    private bool spriteHasChanged;

    public SpriteRenderer srAffiche;
    public Sprite spriteRouge, spriteVert, spriteBleu, spriteJaune;
    public List<colorCombi> combinaison;
    public List<colorCombi> reproduction;

    [SerializeField]
    private AnimTransform animOpen, animClose;

    public bool peutAppuyer = false;

    public void Start()
    {
        if (gm == null) gm = FindObjectOfType<GameManager>();
        combinaison = new List<colorCombi>();
        reproduction = new List<colorCombi>();

        CreationCombinaison();

        StartCoroutine(AffichageCombinaison());
    }

    private void Update()
    {
        if (peutAppuyer)
        {
            if (!spriteHasChanged)
            {
                spriteHasChanged = true;
                bouton1.sprite = bouton1oui;
                bouton2.sprite = bouton2oui;
                bouton3.sprite = bouton3oui;
                bouton4.sprite = bouton4oui;
            }
        }
        else
        {
            if (!spriteHasChanged)
            {
                spriteHasChanged = true;
                bouton1.sprite = bouton1non;
                bouton2.sprite = bouton2non;
                bouton3.sprite = bouton3non;
                bouton4.sprite = bouton4non;
            }
        }
    }

    public void CreationCombinaison()
    {
        switch (gm.nvDifficulte)
        {
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(colorCombi)).Length);
                    combinaison.Add((colorCombi)randomIndex);
                }
                break;

            case 3:
                for (int i = 0; i < 4; i++)
                {
                    int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(colorCombi)).Length);
                    combinaison.Add((colorCombi)randomIndex);
                }
                break;

            case 6:
                for (int i = 0; i < 5; i++)
                {
                    int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(colorCombi)).Length);
                    combinaison.Add((colorCombi)randomIndex);
                }
                break;
        }
    }

    public IEnumerator AffichageCombinaison()
    {
        spriteHasChanged = false;
        //gm.tm.text = "OBSERVE!";

        for (int i = 0; i < combinaison.Count; i++)
        {
            animOpen.SetCanGo();
            switch (combinaison[i])
            {
                case (colorCombi)0:
                    srAffiche.sprite = spriteRouge;
                    break;

                case (colorCombi)1:
                    srAffiche.sprite = spriteVert;
                    break;

                case (colorCombi)2:
                    srAffiche.sprite = spriteBleu;
                    break;

                case (colorCombi)3:
                    srAffiche.sprite = spriteJaune;
                    break;
            }
            switch (gm.nvDifficulte)
            {
                case 1:
                    yield return new WaitForSeconds(.8f);
                    animClose.SetCanGo();
                    yield return new WaitForSeconds(.2f);
                    break;

                case 3:
                    yield return new WaitForSeconds(0.7f);
                    animClose.SetCanGo();
                    yield return new WaitForSeconds(.2f);
                    break;

                case 6:
                    yield return new WaitForSeconds(0.5f);
                    animClose.SetCanGo();
                    yield return new WaitForSeconds(.2f);
                    break;
            }
            srAffiche.sprite = null;
            switch (gm.nvDifficulte)
            {
                case 1:
                    yield return new WaitForSeconds(0.3f);
                    break;

                case 3:
                    yield return new WaitForSeconds(0.2f);
                    break;

                case 6:
                    yield return new WaitForSeconds(0.1f);
                    break;
            }
        }
        peutAppuyer = true;
        spriteHasChanged = false;
    }

    public void AppuiBouton(string couleurAppuyee)
    {
        if (peutAppuyer)
        {
            switch (couleurAppuyee)
            {
                case "r":
                    srAffiche.sprite = spriteRouge;
                    srAffiche.GetComponent<AnimTransform>().SetCanGo();
                    reproduction.Add((colorCombi)0);
                    gm.targetColorTint = colorTint.red;
                    break;

                case "g":
                    srAffiche.sprite = spriteVert;
                    srAffiche.GetComponent<AnimTransform>().SetCanGo();
                    reproduction.Add((colorCombi)1);
                    gm.targetColorTint = colorTint.green;
                    break;

                case "b":
                    srAffiche.sprite = spriteBleu;
                    srAffiche.GetComponent<AnimTransform>().SetCanGo();
                    reproduction.Add((colorCombi)2);
                    gm.targetColorTint = colorTint.blue;
                    break;

                case "y":
                    srAffiche.sprite = spriteJaune;
                    srAffiche.GetComponent<AnimTransform>().SetCanGo();
                    reproduction.Add((colorCombi)3);
                    gm.targetColorTint = colorTint.yellow;
                    break;

                default:
                    Debug.LogWarning("SimonSays AppuiBouton() - Couleur incorrecte");
                    break;
            }
            if (CheckCorrespondance() == -1)
            {
                reproduction.Clear();
                peutAppuyer = false;
                StartCoroutine(AffichageCombinaison());
            }
            else if (CheckCorrespondance() == 1)
            {
                peutAppuyer = false;

                StartCoroutine(gm.Win());
            }
        }
    }

    public int CheckCorrespondance()
    {
        int result = 0;                     // -1 PERDU; 0 RIEN; 1 GAGNE
        for (int i = 0; i < combinaison.Count; i++)
        {
            if (result != -1)
            {
                if (i < reproduction.Count)
                {
                    if (combinaison[i] != reproduction[i])
                    {
                        result = -1;
                    }
                    else if (i == combinaison.Count - 1)
                    {
                        result = 1;
                    }
                }
            }
        }
        return result;
    }
}