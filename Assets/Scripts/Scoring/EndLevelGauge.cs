using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelGauge : MonoBehaviour
{
    private GameManager gm;

    private int musicLength;

    [SerializeField]
    private Transform gaugeToScale;

    private float lerpT;

    // Start is called before the first frame update
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        musicLength = gm.musicDuration;
    }

    // Update is called once per frame
    private void Update()
    {
        lerpT += Time.deltaTime / musicLength;
        float newSecaleX = Mathf.Lerp(1, 0, lerpT);
        gaugeToScale.localScale = new Vector3(newSecaleX, gaugeToScale.localScale.y, gaugeToScale.localScale.z);

        if (lerpT > 1)
        {
            gm.EndLvl();
        }
    }
}