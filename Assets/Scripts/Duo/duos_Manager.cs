using UnityEngine;
using UnityEngine.SceneManagement;

public class duos_Manager : MonoBehaviour
{
    [SerializeField]
    private LED_etat led1, led2, led3;
    public potentiometre potentiometre;

    private void Update()
    {
        if (led1.samePot == true && led2.samePot == true && led3.samePot == true)
        {
            //Changement de minijeu
            Debug.Log("Fin du jeu");
        }
    }
}
