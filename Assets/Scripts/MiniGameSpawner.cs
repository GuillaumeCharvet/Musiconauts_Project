using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameSpawner : MonoBehaviour
{
    public GameObject simonSaysMG, equalizerMG, sinusGameMG, duoMG, spamMG, knobMG;
    public GameObject spawnedMiniGame;

    public GameObject SpawnSimonSays()
    {
        spawnedMiniGame = Instantiate(simonSaysMG, transform);
        return spawnedMiniGame;
    }

    public GameObject SpawnEqualizer()
    {
        spawnedMiniGame = Instantiate(equalizerMG, transform);
        return spawnedMiniGame;
    }

    public GameObject SpawnSinusGame()
    {
        spawnedMiniGame = Instantiate(sinusGameMG, transform);
        return spawnedMiniGame;
    }

    public GameObject SpawnDuo()
    {
        spawnedMiniGame = Instantiate(duoMG, transform);
        return spawnedMiniGame;
    }

    public GameObject SpawnSpam()
    {
        spawnedMiniGame = Instantiate(spamMG, transform);
        return spawnedMiniGame;
    }

    public GameObject SpawnKnob()
    {
        spawnedMiniGame = Instantiate(knobMG, transform);
        return spawnedMiniGame;
    }

    public void DestroyMiniGame()
    {
        Destroy(spawnedMiniGame);
    }
}