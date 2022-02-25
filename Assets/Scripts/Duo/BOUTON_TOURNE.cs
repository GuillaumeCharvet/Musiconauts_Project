using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOUTON_TOURNE : MonoBehaviour
{
    [SerializeField]
    private GameObject led1, led2, led3;

    private SpriteRenderer led1_sprite, led2_sprite, led3_sprite;

    [SerializeField]
    private Sprite sprite_on, sprite_off;

    [SerializeField]
    private GameObject bouton_g1, bouton_g2, bouton_g3;

    [SerializeField]
    private GameObject bouton_d1, bouton_d2, bouton_d3;

    [SerializeField]
    public int angle1_cur, angle2_cur, angle3_cur;

    private Quaternion angle1_cur_quat, angle2_cur_quat, angle3_cur_quat;

    [SerializeField]
    private DUO_MANAGER duo_manager;

    private GameManager gm;

    // Start is called before the first frame update
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        angle1_cur = 0;
        angle2_cur = 0;
        angle3_cur = 0;

        angle1_cur_quat.eulerAngles = new Vector3(0, 0, angle1_cur * 72);
        angle2_cur_quat.eulerAngles = new Vector3(0, 0, angle2_cur * 72);
        angle3_cur_quat.eulerAngles = new Vector3(0, 0, angle3_cur * 72);

        bouton_d1.transform.rotation = angle1_cur_quat;
        bouton_d2.transform.rotation = angle2_cur_quat;
        bouton_d3.transform.rotation = angle3_cur_quat;

        led1_sprite = led1.GetComponent<SpriteRenderer>();
        led2_sprite = led2.GetComponent<SpriteRenderer>();
        led3_sprite = led3.GetComponent<SpriteRenderer>();

        if (angle1_cur == duo_manager.angle1_init)
        {
            led1_sprite.sprite = sprite_on;
        }
        if (bouton_d2.transform.rotation == bouton_g2.transform.rotation)
        {
            led2_sprite.sprite = sprite_on;
        }
        if (bouton_d3.transform.rotation == bouton_g3.transform.rotation)
        {
            led3_sprite.sprite = sprite_on;
        }
    }

    public void Turn_Button(int index)
    {
        if (index == 1)
        {
            if (angle1_cur == 4)
            {
                angle1_cur = 0;
            }
            else
            {
                angle1_cur += 1;
            }
            angle1_cur_quat.eulerAngles = new Vector3(0, 0, -angle1_cur * 72);
            bouton_d1.transform.rotation = angle1_cur_quat;
            Debug.Log("1");
            if (angle1_cur == duo_manager.angle1_init)
            {
                led1_sprite.sprite = sprite_on;
            }
            else
            {
                led1_sprite.sprite = sprite_off;
            }
        }
        else if (index == 2)
        {
            if (angle2_cur == 4)
            {
                angle2_cur = 0;
            }
            else
            {
                angle2_cur += 1;
            }
            angle2_cur_quat.eulerAngles = new Vector3(0, 0, -angle2_cur * 72);
            bouton_d2.transform.rotation = angle2_cur_quat;
            Debug.Log("2");
            if (angle2_cur == duo_manager.angle2_init)
            {
                led2_sprite.sprite = sprite_on;
            }
            else
            {
                led2_sprite.sprite = sprite_off;
            }
        }
        else
        {
            if (angle3_cur == 4)
            {
                angle3_cur = 0;
            }
            else
            {
                angle3_cur += 1;
            }
            angle3_cur_quat.eulerAngles = new Vector3(0, 0, -angle3_cur * 72);
            bouton_d3.transform.rotation = angle3_cur_quat;
            Debug.Log("3");
            if (angle3_cur == duo_manager.angle3_init)
            {
                led3_sprite.sprite = sprite_on;
            }
            else
            {
                led3_sprite.sprite = sprite_off;
            }
        }
        if (duo_manager.ThisAWin())
        {
            StartCoroutine(gm.Win());
        }
    }
}