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

    public SpriteRenderer srAffiche;
    public Sprite spriteBleu;
    public Sprite spriteJaune;
    public Sprite spriteRouge;
    public Sprite spriteVert;

    public bool peutAppuyer = false;
    public bool couleurCombiAffichee = false;

    public List<colorCombi> combinaison;
    public List<colorCombi> reproduction;


    public void GameStart()
    {
        if (gm == null) gm = FindObjectOfType<GameManager>();
        combinaison = new List<colorCombi>();
        reproduction = new List<colorCombi>();

        CreationCombinaison();

        StartCoroutine(AffichageCombinaison());

        
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
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(colorCombi)).Length);
                    combinaison.Add((colorCombi)randomIndex);
                }
                break;
            case 3:
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
        gm.tm.text = "OBESERVE!";

        for (int i = 0; i < combinaison.Count; i++)
        {
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
            couleurCombiAffichee = true;
            yield return new WaitForSeconds(1.2f);             //ATTEND N SECONDES
            srAffiche.sprite = null;
            couleurCombiAffichee = true;
            yield return new WaitForSeconds(0.3f);
        }
        gm.tm.text = "DO THE SAME!";
        peutAppuyer = true;
    }

    public void AppuiBouton(string truc)
    {
        if (peutAppuyer)
        {
            switch (truc)
            {
                case "bleu":
                    srAffiche.sprite = spriteBleu;
                    reproduction.Add((colorCombi)0);
                    break;
                case "jaune":
                    srAffiche.sprite = spriteJaune;
                    reproduction.Add((colorCombi)1);
                    break;
                case "rouge":
                    srAffiche.sprite = spriteRouge;
                    reproduction.Add((colorCombi)2);
                    break;
                case "vert":
                    srAffiche.sprite = spriteVert;
                    reproduction.Add((colorCombi)3);
                    break;
                default:
                    Debug.LogWarning("SimonSays AppuiBouton() - Couleur incorrecte");
                    break;
            }
            if (CheckCorrespondance() == -1)
            {
                //PERDU
            } else if(CheckCorrespondance() == 1)
            {
                //GAGNE
            }
        }
    }

    public int CheckCorrespondance()
    {
        int result = 0;                     // -1 PERDU; 0 RIEN; 1 GAGNE
        for (int i = 0; i < combinaison.Count; i++)
        {
            Debug.Log(combinaison.Count);
            if (combinaison[i] != reproduction[i])
            {
                Debug.Log("PERDU");
            }
            else
            {
                Debug.Log("OUI");
            }
        }
        return result;
    }

}
