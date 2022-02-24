using UnityEngine;
using UnityEngine.SceneManagement;

public class duos_Manager : MonoBehaviour
{
    [SerializeField]
    private LED_etat led1, led2, led3;

    public potentiometre potentiometre;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (led1.samePot && led2.samePot && led3.samePot)
        {
            StartCoroutine(gm.Win());
        }
    }
}