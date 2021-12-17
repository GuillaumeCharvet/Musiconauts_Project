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
    public Sprite spriteRouge, spriteVert, spriteBleu, spriteJaune;

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
        gm.tm.text = "OBSERVE!";

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
            switch (gm.nvDifficulte)
            {
                case 1:
                    yield return new WaitForSeconds(1.2f);
                    break;
                case 2:
                    yield return new WaitForSeconds(0.9f);
                    break;
                case 3:
                    yield return new WaitForSeconds(0.6f);
                    break;
            }
            srAffiche.sprite = null;
            couleurCombiAffichee = true;
            switch (gm.nvDifficulte)
            {
                case 1:
                    yield return new WaitForSeconds(0.3f);
                    break;
                case 2:
                    yield return new WaitForSeconds(0.2f);
                    break;
                case 3:
                    yield return new WaitForSeconds(0.1f);
                    break;
            }
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
                case "rouge":
                    srAffiche.sprite = spriteRouge;
                    reproduction.Add((colorCombi)0);
                    gm.targetColorTint = colorTint.red;
                    gm.LerpToColor(gm.targetColorTint, 0);
                    break;
                case "vert":
                    srAffiche.sprite = spriteVert;
                    reproduction.Add((colorCombi)1);
                    gm.targetColorTint = colorTint.green;
                    gm.LerpToColor(gm.targetColorTint, 0);
                    break;
                case "bleu":
                    srAffiche.sprite = spriteBleu;
                    reproduction.Add((colorCombi)2);
                    gm.targetColorTint = colorTint.blue;
                    gm.LerpToColor(gm.targetColorTint, 0);
                    break;
                case "jaune":
                    srAffiche.sprite = spriteJaune;
                    reproduction.Add((colorCombi)3);
                    gm.targetColorTint = colorTint.yellow;
                    gm.LerpToColor(gm.targetColorTint, 0);
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
            } else if(CheckCorrespondance() == 1)
            {
                peutAppuyer = false;
                gm.currentMiniGame = "";
                gm.enjaillement += 0.1f;
                if (gm.enjaillement >= 1)
                {
                    gm.enjaillement = 1;
                }
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
                if (i < reproduction.Count) {
                    if (combinaison[i] != reproduction[i])
                    {
                        result = -1;
                    }
                    else if (i == combinaison.Count -1)
                    {
                        result = 1;
                    }
                }
            }
        }
        return result;
    }

}
