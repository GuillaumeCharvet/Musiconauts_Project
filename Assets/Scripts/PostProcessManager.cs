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

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        vp = vol.profile;

        //ASSIGNATION OVERRIDES
        vp.TryGet<ChromaticAberration>(out ca);

        //INITIALISATION VALEURS OVERRIDES
        ca.intensity.value = 0f;
    }

    private void Update()
    {
        ca.intensity.value = gm.enjaillement / 2;
    }
}