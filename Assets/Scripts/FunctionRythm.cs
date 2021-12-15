using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionRythm : MonoBehaviour
{
    public int numberOfCircles;
    public List<Cible> listCircles;
    public Sprite sprite_ring;

    public class Cible
    {
        public Vector2 position;
        public GameObject object_target;
        public GameObject object_moving;

        public Cible(Vector2 _position, GameObject _object_target, GameObject _object_moving)
        {
            position = _position; object_target = _object_target; object_moving = _object_moving;
        }
    }

    private void Awake()
    {
        sprite_ring = Resources.Load<Sprite>("ring");
    }

    private void Start()
    {
        numberOfCircles = Random.Range(3, 5);
        for( int i = 0; i < numberOfCircles; i++)
        {
            float xPos = Random.Range(0f, Screen.width);
            float yPos = Random.Range(0f, Screen.height);
            GameObject o_t = new GameObject("ring");
            SpriteRenderer renderer_ot = o_t.AddComponent<SpriteRenderer>();
            renderer_ot.sprite = sprite_ring;
            GameObject o_m = new GameObject("ring");
            SpriteRenderer renderer_om = o_m.AddComponent<SpriteRenderer>();
            renderer_om.sprite = sprite_ring;
            Cible cible = new Cible(new Vector2(xPos, yPos), o_t, o_m);
            //cible.object_moving. = 
            listCircles.Add(cible);
        }
    }
    private void Update()
    {
        
    }
}
