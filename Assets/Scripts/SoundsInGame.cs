using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsInGame : MonoBehaviour
{
    [SerializeField]
    private AudioSource fouleAS;

    private GameManager gm;

    // Start is called before the first frame update
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        fouleAS.volume = gm.enjaillement / 8;
    }
}