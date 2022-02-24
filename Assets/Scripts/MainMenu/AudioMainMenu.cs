using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMainMenu : MonoBehaviour
{
    public AudioSource fondMenu;
    public AudioSource boutonMenu;
    public AudioSource sonNeon;
    public AudioSource sonNeonBug;



    public void SoundBoutonMenu()
    {
        boutonMenu.Play();
    }
    
    public void SonNeonBug()
    {
        sonNeonBug.Play();
    }

    public void MusicFondMenu()
    {
        fondMenu.Play();
    }

    public void SoundNeonAllumage()
    {
        sonNeon.Play();
    }

}
