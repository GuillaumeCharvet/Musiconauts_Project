using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerGauge : MonoBehaviour
{
    private GameManager gm;

    private int lengthMiniGame;

    private bool canGo;

    [SerializeField]
    private Transform gaugeToScale;

    private float lerpT;

    // Start is called before the first frame update
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (canGo)
        {
            lerpT += Time.deltaTime / lengthMiniGame;
            float newSecaleX = Mathf.Lerp(1, 0, lerpT);
            gaugeToScale.localScale = new Vector3(newSecaleX, gaugeToScale.localScale.y, gaugeToScale.localScale.z);

            if (lerpT > 1)
            {
                gm.EndMiniGame();
                lerpT = 0;
            }
        }
    }

    public void TimerStart()
    {
        switch (gm.currentMiniGame)
        {
            case miniGame.simonSays:
                lengthMiniGame = gm.simonSaysTemps;
                break;

            case miniGame.equalizer:
                lengthMiniGame = gm.equalizerTemps;
                break;

            case miniGame.duo:
                lengthMiniGame = gm.duoTemps;
                break;

            case miniGame.spam:
                lengthMiniGame = gm.spamTemps;
                break;

            default:
                Debug.LogError("TimerGauge.TimerStart() - MiniGame non compris dans le switch - " + gm.currentMiniGame.ToString());
                break;
        }

        canGo = true;
    }

    public float TimerStop()
    {
        canGo = false;
        float exLerpT = lerpT;
        lerpT = 0;
        gaugeToScale.localScale = new Vector3(1, gaugeToScale.localScale.y, gaugeToScale.localScale.z);
        return exLerpT;
    }
}