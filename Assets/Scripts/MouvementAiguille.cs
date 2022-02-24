using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementAiguille : MonoBehaviour
{
    [SerializeField]
    private float angle_aiguille;

    [SerializeField]
    private GameObject aiguille;

    [SerializeField]
    private float decroissance = 0.05f;

    [SerializeField]
    private float croissance = 12f;

    [SerializeField]
    private List<float> boost_list;

    [SerializeField]
    private GameObject compteur_light;

    private SpriteRenderer sr_compteur_light;

    [SerializeField]
    private GameObject compteur;

    private SpriteRenderer sr_compteur;

    [SerializeField]
    private Sprite sprite_compteur_on;

    [SerializeField]
    private Sprite sprite_compteur_off;

    [SerializeField]
    private GameObject button;

    private SpriteRenderer sr_button;

    [SerializeField]
    private Sprite sprite_button_on;

    [SerializeField]
    private Sprite sprite_button_off;

    private float angle_limit_inf = 120f;
    private float angle_limit_sup = 160f;

    private float duration_in_right_range = 0f;
    private float duration_in_right_range_required = 150f;

    private Quaternion rotation_aiguille;

    private GameManager game_manager;

    private float difficulty;

    private void Awake()
    {
        game_manager = FindObjectOfType<GameManager>();
        difficulty = game_manager.nvDifficulte;

        decroissance = 0.047f + difficulty * 4f / 1000f;

        sr_compteur = compteur.GetComponent<SpriteRenderer>();
        sr_button = button.GetComponent<SpriteRenderer>();
        sr_compteur_light = compteur_light.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Reset();
    }

    // Update is called once per frame
    private void Update()
    {
        if (boost_list.Count > 0)
        {
            angle_aiguille += boost_list[0];
            boost_list.RemoveAt(0);
        }
        angle_aiguille -= Time.deltaTime * decroissance * 650;
        angle_aiguille = Mathf.Min(Mathf.Max(angle_aiguille, 0f), 180f);
        rotation_aiguille.eulerAngles = new Vector3(0, 0, -angle_aiguille);
        aiguille.transform.rotation = rotation_aiguille;

        if (angle_aiguille >= angle_limit_inf && angle_aiguille <= angle_limit_sup)
        {
            duration_in_right_range += Time.deltaTime * 50f;
            //sr_compteur_light.enabled = true;
            //sr_compteur.sprite = sprite_compteur_on;
            Debug.Log((0.6f + duration_in_right_range) / (0.6f + duration_in_right_range_required));
            sr_compteur_light.color = new Vector4(sr_compteur_light.color.r, sr_compteur_light.color.g, sr_compteur_light.color.b, (70f + duration_in_right_range) / (70f + duration_in_right_range_required));

            if (duration_in_right_range >= duration_in_right_range_required)
            {
                decroissance = 0f;
                croissance = 0f;
                StartCoroutine(game_manager.Win());
            }
        }
        else
        {
            //sr_compteur_light.enabled = false;
            sr_compteur.sprite = sprite_compteur_off;
            duration_in_right_range = 0f;
        }
    }

    private void FixedUpdate()
    {
    }

    public void boost()
    {
        boost_list.Add(croissance);
    }

    public void EnterButton()
    {
        sr_button.sprite = sprite_button_on;
    }

    public void ExitButton()
    {
        sr_button.sprite = sprite_button_off;
    }

    private void Reset()
    {
        difficulty = game_manager.nvDifficulte;

        //score_aiguille = 0f;
        angle_aiguille = 0;

        rotation_aiguille.eulerAngles = new Vector3(0, 0, angle_aiguille);
        aiguille.transform.rotation = rotation_aiguille;
    }
}