using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysButtonAnimIfPeutAppuyer : MonoBehaviour
{
    private AnimTransform bouton;

    [SerializeField]
    private SimonSays ss;

    private void Awake()
    {
        bouton = GetComponent<AnimTransform>();
    }

    public void CheckIfCanPress()
    {
        if (ss.peutAppuyer)
        {
            bouton.SetCanGo();
        }
    }
}