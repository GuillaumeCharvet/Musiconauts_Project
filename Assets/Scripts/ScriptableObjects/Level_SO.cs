using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelTemplate", menuName = "Level", order = 0)]
public class Level_SO : ScriptableObject
{
    public int levelNumber;
    public AudioClip music;
    public string artistName, musicName, albumName, genre;
    public List<Color> mainColors;
    public int difficultyMin, difficultyMax;
    public float enjaillementDecrement;
    public Sprite albumImage, sceneSprite, artistsSprite;

    [TextArea(10, 25)]
    public string description;
}