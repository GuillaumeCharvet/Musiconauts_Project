using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DUO_MANAGER : MonoBehaviour
{
    [SerializeField]
    private GameObject led1, led2, led3;
    private SpriteRenderer led1_sprite, led2_sprite, led3_sprite;
    [SerializeField]
    private GameObject bouton_g1, bouton_g2, bouton_g3;
    [SerializeField]
    private GameObject bouton_d1, bouton_d2, bouton_d3;
    [SerializeField]
    public int angle1_init, angle2_init, angle3_init;
    private Quaternion angle1_init_quat, angle2_init_quat, angle3_init_quat;
    
    [SerializeField]
    private GameObject droite;
    private BOUTON_TOURNE bouton_tourne;

    void Start()
    {
        angle1_init = Random.Range(1, 5);
        angle2_init = Random.Range(0, 5);
        angle3_init = Random.Range(1, 5);

        angle1_init_quat.eulerAngles = new Vector3(0, 0, -angle1_init * 72);
        angle2_init_quat.eulerAngles = new Vector3(0, 0, -angle2_init * 72);
        angle3_init_quat.eulerAngles = new Vector3(0, 0, -angle3_init * 72);

        bouton_g1.transform.rotation = angle1_init_quat;
        bouton_g2.transform.rotation = angle2_init_quat;
        bouton_g3.transform.rotation = angle3_init_quat;

        led1_sprite = led1.GetComponent<SpriteRenderer>();
        led2_sprite = led2.GetComponent<SpriteRenderer>();
        led3_sprite = led3.GetComponent<SpriteRenderer>();

        bouton_tourne = droite.GetComponent<BOUTON_TOURNE>();
    }

    public bool ThisAWin()
    {
        return angle1_init == bouton_tourne.angle1_cur && angle2_init == bouton_tourne.angle2_cur && angle3_init == bouton_tourne.angle3_cur;
    }
}
