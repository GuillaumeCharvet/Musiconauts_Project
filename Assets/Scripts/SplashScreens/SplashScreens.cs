using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreens : MonoBehaviour
{
    [SerializeField]
    public Transform menu;

    [SerializeField]
    private SplashScreensSquare square;

    [SerializeField]
    private SpriteRenderer srEtpa, srTrans, srMusiconauts;

    private float alphaEtpa, alphaTrans, alphaMusiconauts;

    private float timer;
    private int chapter;

    [SerializeField]
    private float baseTime, timeToTransition, timeToAlpha, timeToStay, timeFactor;

    private float alphaSpeedFactor;

    [SerializeField]
    private string sceneToLoadName;

    [SerializeField]
    private AudioSource audioS;

    private float audioVolume = 0;

    private void Start()
    {
        square._menu = menu;
        alphaSpeedFactor = 1 / timeToAlpha;
    }

    private void Update()
    {
        if (!square.canGo)
        {
            DisplayLogos();

            AudioVolumeManager();
        }
        else
        {
            this.enabled = false;
        }
    }

    private void DisplayLogos()
    {
        switch (chapter)
        {
            case 0:
                if (timer < baseTime)
                {
                    timer += Time.deltaTime * timeFactor;
                }
                else
                {
                    timer = 0;
                    chapter++;
                }
                break;

            case 1:
                if (timer < timeToAlpha)
                {
                    alphaEtpa = timer * alphaSpeedFactor;
                    srEtpa.color = new Vector4(1, 1, 1, alphaEtpa);
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay)
                {
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay + timeToAlpha)
                {
                    alphaEtpa -= Time.deltaTime * alphaSpeedFactor;
                    srEtpa.color = new Vector4(1, 1, 1, alphaEtpa);
                    timer += Time.deltaTime * timeFactor;
                }
                else
                {
                    timer = 0;
                    chapter++;
                }
                break;

            case 2:
                if (timer < timeToTransition)
                {
                    timer += Time.deltaTime * timeFactor;
                }
                else
                {
                    timer = 0;
                    chapter++;
                }
                break;

            case 3:
                if (timer < timeToAlpha)
                {
                    alphaTrans = timer * alphaSpeedFactor;
                    srTrans.color = new Vector4(1, 1, 1, alphaTrans);
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay)
                {
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay + timeToAlpha)
                {
                    alphaTrans -= Time.deltaTime * alphaSpeedFactor;
                    srTrans.color = new Vector4(1, 1, 1, alphaTrans);
                    timer += Time.deltaTime * timeFactor;
                }
                else
                {
                    timer = 0;
                    chapter++;
                }
                break;

            case 4:
                if (timer < timeToTransition)
                {
                    timer += Time.deltaTime * timeFactor;
                }
                else
                {
                    timer = 0;
                    chapter++;
                }
                break;

            case 5:
                if (timer < timeToAlpha)
                {
                    alphaMusiconauts = timer * alphaSpeedFactor;
                    srMusiconauts.color = new Vector4(1, 1, 1, alphaMusiconauts);
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay)
                {
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay + timeToAlpha)
                {
                    alphaMusiconauts -= Time.deltaTime * alphaSpeedFactor;
                    srMusiconauts.color = new Vector4(1, 1, 1, alphaMusiconauts);
                    timer += Time.deltaTime * timeFactor;
                }
                else
                {
                    timer = 0;
                    chapter++;
                }
                break;

            case 6:
                if (timer < timeToTransition * 2)
                {
                    timer += Time.deltaTime * timeFactor;
                }
                else
                {
                    timer = 0;
                    chapter++;
                }
                break;

            case 7:
                TransitionToMainMenu();
                break;
        }
    }

    private void AudioVolumeManager()
    {
        if (audioVolume < 0.5)
        {
            audioVolume += Time.deltaTime * 0.05f;

            audioS.volume = audioVolume;
        }
    }

    private void TransitionToMainMenu()
    {
        square.canGo = true;
    }
}