using UnityEngine;

public class LED_etat : MonoBehaviour
{
    public bool samePot;
    public SpriteRenderer led;
    [SerializeField]
    private potentiometre pot1, pot2;
    public int etatpot1;
    public int etatpot2;
    public Sprite rouge;
    public Sprite vert;
    public AudioSource lightup;
    private bool soundplayed;

    private void Awake()
    {

    }

    private void Update()
    {

    }

    public void verifEtat()
    {
        if (etatpot1 == etatpot2)
        {
            samePot = true;
            changeLEDColor();
            if (soundplayed == false)
            {
                lightup.Play();
                soundplayed = true;
            }
        }
        else
        {
            samePot = false;
            changeLEDColor();
            soundplayed = false;
        }
    }

    void changeLEDColor()
    {
        if (samePot == true)
        {
            led.sprite = vert;
            etatpot1 = pot1.etatPot;
            etatpot2 = pot2.etatPot;
        }
        else if (samePot == false)
        {
            led.sprite = rouge;
            etatpot1 = pot1.etatPot;
            etatpot2 = pot2.etatPot;
        }
    }
}
