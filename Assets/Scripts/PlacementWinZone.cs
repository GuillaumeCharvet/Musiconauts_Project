using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementWinZone : MonoBehaviour
{
    public Vector2 placementZone;
    Vector2 pos;
    public float randomVar;
    public float baseY;

    public void Start()
    {
        baseY = transform.position.y;
        randomVar = Random.Range(-1.1f, 1.1f);
        pos = new Vector2(transform.position.x, baseY + randomVar);
        transform.position = pos;
    }

    public void Reset()
    {
        randomVar = Random.Range(-1.1f, 1.1f);
        pos = new Vector2(transform.position.x, baseY + randomVar);
        transform.position = pos;
    }


}
