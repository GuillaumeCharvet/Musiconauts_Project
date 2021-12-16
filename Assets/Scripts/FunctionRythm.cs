using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionRythm : MonoBehaviour
{
    public int numberOfCircles;
    public List<Cible> listCibles = new List<Cible>();
    public Sprite sprite_ring;
    public float time = 0f;
    public float reflex_time = 10f;

    public class Cible
    {
        public Vector2 position;
        public GameObject object_target;
        public GameObject object_moving;
        public float starting_time;
        public float lerp_value;
        public bool isActive;

        public Cible(Vector2 _position, float _starting_time, float _lerp_value, bool _isActive, GameObject _object_target, GameObject _object_moving)
        {
            position = _position; starting_time = _starting_time; lerp_value = _lerp_value; isActive = _isActive; object_target = _object_target; object_moving = _object_moving;
        }

    }

    private void Awake()
    {
        sprite_ring = Resources.Load<Sprite>("Sprites/sprite_ring");
    }

    private void Start()
    {
        numberOfCircles = Random.Range(3, 5);
        for( int i = 0; i < numberOfCircles; i++)
        {
            //float xPos = .1f * (Random.Range(0f, Screen.width) * 0.8f + Screen.width * 0.1f);
            //float yPos = .1f * (Random.Range(0f, Screen.height) * 0.8f + Screen.height * 0.1f);
            float xPos = Random.Range(-1.7f, 1.7f);
            float yPos = Random.Range(-3f, 3f);
            float timing = Random.Range(2f, 5f);

            Debug.Log(xPos);
            Debug.Log(yPos);

            GameObject o_t = new GameObject("ring");
            SpriteRenderer renderer_o_t = o_t.AddComponent<SpriteRenderer>();
            BoxCollider2D box_collider_o_t = o_t.AddComponent<BoxCollider2D>();
            OnClick onclick = o_t.AddComponent<OnClick>();
            //._onClick.AddListener()
            renderer_o_t.sprite = sprite_ring;
            GameObject o_m = new GameObject("ring");
            SpriteRenderer renderer_om = o_m.AddComponent<SpriteRenderer>();
            renderer_om.sprite = sprite_ring;

            Cible cible = new Cible(new Vector2(xPos, yPos), timing, 0f, true, o_t, o_m);
            //cible.object_moving. =
            listCibles.Add(cible);
        }
        foreach (Cible cible in listCibles)
        {
            SpriteRenderer sp = cible.object_moving.GetComponent<SpriteRenderer>();
            sp.color = new Color(1f, 1f, 1f, 0f);
            Transform tr1 = cible.object_target.transform;
            tr1.position = new Vector3(cible.position.x,cible.position.y,0);
            tr1.localScale = new Vector3(0.08f, 0.08f, 1f);
            Transform tr2 = cible.object_moving.transform;
            tr2.position = new Vector3(cible.position.x, cible.position.y, 0);
        }
    }
    private void Update()
    {
        time += Time.deltaTime;

        foreach (Cible cible in listCibles)
        {
            if(cible.isActive && time >= cible.starting_time)
            {
                cible.lerp_value = (time - cible.starting_time)/reflex_time;
                //cible.object_moving.transform.localScale = new Vector3(1f - cible.lerp_value, 1f - cible.lerp_value, 1f);
                cible.object_moving.transform.localScale = new Vector3(Mathf.Lerp(1f, 0.08f, cible.lerp_value), Mathf.Lerp(1f, 0.08f, cible.lerp_value),1f);
                cible.object_moving.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, cible.lerp_value));
            }
            if (cible.lerp_value > 1f)
            {
                cible.isActive = false;
            }
        }
    }

    public void ChangeVictoryValue()
    {

    }
}
