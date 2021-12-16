using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementWinZone : MonoBehaviour
{
    public Vector2 placementZone;
    Vector2 pos;
    public float randomVar;

    public void Start()
    {
        randomVar = Random.Range(-1.1f, 1.1f);
        pos = new Vector2(0, randomVar);
        transform.Translate(pos);
    }


}
