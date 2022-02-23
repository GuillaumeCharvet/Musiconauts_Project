using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoBehaviour
{
    private GameManager gm;
    public Volume vol;
    private VolumeProfile vp;
    private ChromaticAberration ca;
    private LensDistortion ld;
    private Bloom b;

    private float lerpT;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        vp = vol.profile;

        //ASSIGNATION OVERRIDES
        vp.TryGet<ChromaticAberration>(out ca);

        vp.TryGet<LensDistortion>(out ld);

        vp.TryGet<Bloom>(out b);

        //INITIALISATION VALEURS OVERRIDES
        ca.intensity.value = 0f;

        ld.intensity.value = 0f;

        b.threshold.value = 1.16f;
    }

    private void Update()
    {
        ca.intensity.value = gm.enjaillement / 3;

        ld.intensity.value = gm.enjaillement / -8;

        lerpT = (0 - gm.enjaillement) / (0 - 1);

        b.threshold.value = Mathf.Lerp(1.16f, 0.95f, lerpT);
    }
}