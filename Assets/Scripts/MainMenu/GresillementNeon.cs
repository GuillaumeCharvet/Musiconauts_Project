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

    public float delayCoupure = 0;
    public bool diminuedelayNeon = true;
    public float LancementNeon = 2;
    private bool AfficheNeon = false ;


    public void Update()
    {
        if(LancementNeon > 0)
        {
            LancementNeon -= Time.deltaTime;
        }
        else
        {
            LancementNeon = 0;
        }
        


        if (delayCoupure <= 0)
        {
            LancementNeon = 2;
            amm.SoundNeonAllumage();
            delayCoupure = Random.Range(10, 15);
           
        }
        else
        {
            delayCoupure -= Time.deltaTime;
        }


    
        if (LancementNeon <= 1.57 && LancementNeon > 1.34)
        { 
            Neonaffiche.sprite = NeonAllume;
        }
        if (LancementNeon <= 1.34 && LancementNeon > 1)
        {
            Neonaffiche.sprite = NeonEteint;
        }
        if (LancementNeon <=1 && LancementNeon >0.80)
        { 
            Neonaffiche.sprite = NeonAllume;
        }
        if (LancementNeon <= 0.80 && LancementNeon > 0.56)
        {
            Neonaffiche.sprite = NeonEteint;
        }
        if (LancementNeon <= 0.56 && LancementNeon > 0.35)
        {
            Neonaffiche.sprite = NeonAllume;
        }
        if (LancementNeon <= 0.35 && LancementNeon > 0.05)
        {
            Neonaffiche.sprite = NeonEteint;
        }
        if(LancementNeon <= 0.05)
        {
            Neonaffiche.sprite = NeonAllume;
        }

    }
}
