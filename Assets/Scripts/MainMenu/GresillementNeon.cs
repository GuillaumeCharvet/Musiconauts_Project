using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GresillementNeon : MonoBehaviour
{
    public SpriteRenderer Neonaffiche;
    public Sprite NeonAllume;
    public Sprite NeonEteint;
    public AudioMainMenu amm;

    public float delayCoupure = 10;
    public bool diminuedelayNeon = true;
    public float gresillement = 1.5f;

    public float Timercoupure = 2;
    private bool startNeon = true;

    public void Update()
    {

        if (startNeon)
        {
            startNeon = false;
            amm.SoundNeonAllumage();
        }

        if(Timercoupure > 0)
        {
           Timercoupure -= Time.deltaTime;
        }
        else
        {
           Timercoupure = 0;
        }


        if (Timercoupure <= 1.57 && Timercoupure > 1.34)
        { 
            Neonaffiche.sprite = NeonAllume;
        }
        if (Timercoupure <= 1.34 && Timercoupure > 1)
        {
            Neonaffiche.sprite = NeonEteint;
        }
        if (Timercoupure <=1 && Timercoupure >0.80)
        { 
            Neonaffiche.sprite = NeonAllume;
        }
        if (Timercoupure <= 0.80 && Timercoupure > 0.56)
        {
            Neonaffiche.sprite = NeonEteint;
        }
        if (Timercoupure <= 0.56 && Timercoupure > 0.35)
        {
            Neonaffiche.sprite = NeonAllume;
        }
        if (Timercoupure <= 0.35 && Timercoupure > 0.05)
        {
            Neonaffiche.sprite = NeonEteint;
        }
        if(Timercoupure <= 0.05)
        {
            Neonaffiche.sprite = NeonAllume;
        }



    
        if (delayCoupure <= 0)
        {
            gresillement = 0.3f ;
            amm.SonNeonBug();
            delayCoupure = Random.Range(15, 20);
        }
        else
        {
            delayCoupure -= Time.deltaTime;
        }

        if (gresillement > 0)
        {
            gresillement -= Time.deltaTime;
        }
        else
        {
            gresillement = 0;
        }

        if (gresillement < 0.3 && gresillement > 0)
        {
            Neonaffiche.sprite = NeonEteint;
        }

        




    }
}
