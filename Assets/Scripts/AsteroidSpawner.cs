using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : SpawnerParent
{
    public LevelData asteroidData;

    private void OnEnable()
    {
        InitDataFile();
        FindScreenBorders();
    }

    void Update()
    {
        if (!Pause.OnPause && isReadyToSpawn(asteroidData))
        {
            GameObject newbyAsteroid = Spawn("Asteroid");
            // Init asteroid parameters from spawner
            AsteroidController asteroidController = newbyAsteroid.GetComponent<AsteroidController>();
            asteroidController.SetLevel(level);
            asteroidController.asteroidSpawner = this;
            asteroidController.data = asteroidData;
        }
        LevelUpIfEnoughScore();
    }

    void InitDataFile() // is needed to send asteroidLevelData (scriptable object) to the SpawnerParent 
    {
        dataInParentObject = asteroidData;
    }

    // FOR TESTS
    public GameObject SpawnAsteroid()
    {
        return Spawn("Asteroid");
    }

}
