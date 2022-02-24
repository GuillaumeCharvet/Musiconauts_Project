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

    public void verifEtat()
    {
        if (etatpot1 == etatpot2)
        {
            samePot = true;
            changeLEDColor();
        }
        else
        {
            samePot = false;
            changeLEDColor();
        }
    }

    private void changeLEDColor()
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