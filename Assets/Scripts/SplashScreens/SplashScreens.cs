using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreens : MonoBehaviour
{
    [SerializeField]
    private SplashScreensSquare square;

    [SerializeField]
    private SpriteRenderer srEtpa, srTrans, srMusiconauts;

    private float alphaEtpa, alphaTrans, alphaMusiconauts;

    private float timer;

    [HideInInspector]
    public int chapter = -1;

    [SerializeField]
    private float baseTime, timeToTransition, timeToAlpha, timeToStay, timeFactor;

    private float alphaSpeedFactor;

    [SerializeField]
    private string sceneToLoadName;

    [SerializeField]
    private AudioSource audioS;

    private DDOL_Variables ddol;

    private float audioVolume = 0;

    private void Start()
    {
        ddol = FindObjectOfType<DDOL_Variables>();
        alphaSpeedFactor = 1 / timeToAlpha;

        if (!ddol.firstLaunch)
        {
            chapter = 7;
        }
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
            audioS.volume = .5f;
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
                    alphaEtpa = timer * alphaSpeedFactor * timeFactor;
                    srEtpa.color = new Vector4(1, 1, 1, alphaEtpa);
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay)
                {
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay + timeToAlpha)
                {
                    alphaEtpa -= Time.deltaTime * alphaSpeedFactor * timeFactor;
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
                    alphaTrans = timer * alphaSpeedFactor * timeFactor;
                    srTrans.color = new Vector4(1, 1, 1, alphaTrans);
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay)
                {
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay + timeToAlpha)
                {
                    alphaTrans -= Time.deltaTime * alphaSpeedFactor * timeFactor;
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
                    alphaMusiconauts = timer * alphaSpeedFactor * timeFactor;
                    srMusiconauts.color = new Vector4(1, 1, 1, alphaMusiconauts);
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay)
                {
                    timer += Time.deltaTime * timeFactor;
                }
                else if (timer < timeToAlpha + timeToStay + timeToAlpha)
                {
                    alphaMusiconauts -= Time.deltaTime * alphaSpeedFactor * timeFactor;
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
            audioVolume += Time.deltaTime * 0.02f;

            audioS.volume = audioVolume;
        }
    }

    private void TransitionToMainMenu()
    {
        square.canGo = true;
    }
}