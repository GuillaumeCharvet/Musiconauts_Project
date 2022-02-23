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

    void Start()
    {
        initializePot();
    }

    void Update()
    {
        if (etatPot == 1)
        {
            spritepot.sprite = pot1;
            changement = false;
            verif.verifEtat();
        }
        else if (etatPot == 2)
        {
            spritepot.sprite = pot2;
            changement = false;
            verif.verifEtat();
        }
        else if (etatPot == 3)
        {
            spritepot.sprite = pot3;
            changement = false;
            verif.verifEtat();
        }
        else if (etatPot == 4)
        {
            spritepot.sprite = pot4;
            changement = false;
            verif.verifEtat();
        }
        else if (etatPot == 5)
        {
            spritepot.sprite = pot5;
            changement = false;
            verif.verifEtat();
        }
    }
    
    public void changePot()
    {
        if (changement == false && interactable == true)
        {
            changement = true;
            etatPot += 1;
            click.Play();
            if (etatPot == 6)
            {
                etatPot = 1;
            }
        }
    }

    public void initializePot()
    {
        etatPot = Random.Range(1, 6);
        if (etatPot == duopot.etatPot)
        {
            initializePot();
        }
    }
}
