using UnityEngine;

public class potentiometre : MonoBehaviour
{
    [SerializeField]
    private potentiometre duopot;

    private bool changement;
    public bool interactable;
    public int etatPot;
    public SpriteRenderer spritepot;

    public Sprite pot1;
    public Sprite pot2;
    public Sprite pot3;
    public Sprite pot4;
    public Sprite pot5;

    public LED_etat verif;

    public AudioSource click;

    private void Start()
    {
        initializePot();
    }

    public void changePot()
    {
        if (changement == false && interactable == true)
        {
            //click.Play();
            if (etatPot == 5)
            {
                etatPot = 1;
            }
            else
            {
                etatPot++;
            }
            changement = true;
            verif.verifEtat();
            UpdatePos();
        }
    }

    public void initializePot()
    {
        etatPot = Random.Range(1, 6);
        if (etatPot == duopot.etatPot)
        {
            initializePot();
        }
        else
        {
            UpdatePos();
        }
    }

    private void UpdatePos()
    {
        switch (etatPot)
        {
            case 1:
                spritepot.sprite = pot1;
                changement = false;
                break;

            case 2:
                spritepot.sprite = pot2;
                changement = false;
                break;

            case 3:
                spritepot.sprite = pot3;
                changement = false;
                break;

            case 4:
                spritepot.sprite = pot4;
                changement = false;
                break;

            case 5:
                spritepot.sprite = pot5;
                changement = false;
                break;
        }
    }
}