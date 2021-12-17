using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class publicMovements : MonoBehaviour
{
    public GameManager gm;

    public float offset;

    private float baseY;
    private float baseX;

    private float frequency = 1.3f;
    private float amplitude = 0.1f;
    private float timer = 0;

    public SpriteRenderer sr;
    public Sprite spriteCalme;
    public Sprite spriteJoie;



    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gm = FindObjectOfType<GameManager>();
        baseY = transform.position.y;
        baseX = transform.position.x;
        timer += offset;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = baseY + Mathf.Sin(timer * frequency) * amplitude * gm.enjaillement;
        float newX = baseX + Mathf.Sin(timer * frequency*2) * amplitude/3 * gm.enjaillement;
        transform.position = new Vector3(newX, newY, transform.position.z);
        timer += Time.deltaTime;

        if (gm.enjaillement > 0.8f)
        {
            sr.sprite = spriteJoie;
        }
        else
        {
            sr.sprite = spriteCalme;
        }
    }
}
