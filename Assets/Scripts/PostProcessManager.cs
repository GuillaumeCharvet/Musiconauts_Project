using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoBehaviour
{
    private GameManager gm;

    [SerializeField]
    private Volume vol;

    private VolumeProfile vp;
    private ChromaticAberration ca;
    private LensDistortion ld;
    private Bloom b;

    private bool postProcesActivated;
    private DDOL_Variables ddol;

    private float lerpT;

    public void Start()
    {
        ddol = FindObjectOfType<DDOL_Variables>();
        postProcesActivated = ddol.postProcess;

        gm = FindObjectOfType<GameManager>();
        vp = vol.profile;

        //ASSIGNATION OVERRIDES
        vp.TryGet<ChromaticAberration>(out ca);

        vp.TryGet<LensDistortion>(out ld);

        vp.TryGet<Bloom>(out b);

        if (postProcesActivated)
        {
            vol.enabled = true;

            //INITIALISATION VALEURS OVERRIDES
            ca.intensity.value = 0f;

            ld.intensity.value = 0f;

            b.threshold.value = 1.16f;
        }
        else
        {
            vol.enabled = false;
        }
    }

    private void Update()
    {
        if (postProcesActivated)
        {
            ca.intensity.value = gm.enjaillement / 3;

            ld.intensity.value = gm.enjaillement / -8;

            lerpT = (0 - gm.enjaillement) / (0 - 1);

            b.threshold.value = Mathf.Lerp(1.16f, 0.95f, lerpT);
        }
    }
}