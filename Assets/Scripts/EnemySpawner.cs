using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SpawnerParent
{
    public LevelData enemyData;

    private void Start()
    {   
        FindScreenBorders();
        InitDataFile();
    }

    void Update()
    {
        if (!Pause.OnPause && isReadyToSpawn(enemyData) && isPlayerExist())
        {
            GameObject newEnemy = Spawn("Enemy");
            newEnemy.GetComponent<EnemyController>().SetLevel(level);
        }
        LevelUpIfEnoughScore();
    }

    void InitDataFile()
    {
        dataInParentObject = enemyData;
    }

    // FOR TESTS
    public GameObject SpawnEnemy()
    {
        return Spawn("Enemy");
    }


}
