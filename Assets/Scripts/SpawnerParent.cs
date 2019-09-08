using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerParent : MonoBehaviour
{

    [HideInInspector]
    public LevelData dataInParentObject;

    protected int level = 0;
    protected float momentOfPreviousSpawn = 0.0f;
    protected float leftScreenBorder = 0;
    protected float rightScreenBorder = 0;
    protected float topScreenBorder = 0;

    public int Level { get { return level; } }

    protected GameObject Spawn(string name)
    {
        float spawnX = Random.Range(leftScreenBorder, rightScreenBorder);
        float spawnY = topScreenBorder;
        Vector2 spawnPoint = new Vector2(spawnX, spawnY);
        GameObject Object = Instantiate(Resources.Load(name) as GameObject, spawnPoint, Quaternion.identity);
        Object.SendMessage("SetData", dataInParentObject);
        momentOfPreviousSpawn = Time.time;
        return Object;
    }

    protected Vector2 GenerateRandomSpawnPoint()
    {
        float spawnX = Random.Range(leftScreenBorder, rightScreenBorder);
        float spawnY = topScreenBorder;
        Vector2 spawnPoint = new Vector2(spawnX, spawnY);
        return spawnPoint;
    }

    protected void FindScreenBorders()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        leftScreenBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        rightScreenBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        topScreenBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
    }

    protected void LevelUpIfEnoughScore()
    {
        // Do not level up if there is no next level in data
        if (Score.GetScore() > dataInParentObject.scoresToNextLevel[level] && level < dataInParentObject.life.Count - 1)
            level++;
    }

    protected bool isReadyToSpawn(LevelData levelData)
    {
        Debug.Log($"Level = {level}");
        Debug.Log($"Data file exist = {dataInParentObject}");
        Debug.Log($"There is Spawn delay field = {levelData}");
        Debug.Log($"Spawn delay = {levelData.spawnDelay[level]}");
        if (Time.time - momentOfPreviousSpawn > levelData.spawnDelay[level] * Time.deltaTime)
            return true;
        else
            return false;
    }

    protected bool isPlayerExist()
    {
        if (GameObject.FindWithTag("Player"))
            return true;
        else
            return false;
    }
}
