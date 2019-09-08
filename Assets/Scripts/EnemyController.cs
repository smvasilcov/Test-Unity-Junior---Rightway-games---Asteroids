using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : SpaceObjectParent
{
    private void OnEnable()
    {
        EnemySpawner enemySpawner = GameObject.FindWithTag("Spawner").GetComponent<EnemySpawner>();
        data = enemySpawner.dataInParentObject;
        level = enemySpawner.Level;
        Player = FindPlayer();
        SetLifeGunsAndSprite();
    }
    void Update()
    {
        if (!Pause.OnPause)
        {
            Move();
            Shoot();
            CheckIfDamageEnoughToExplode();
            LookAtPlayer();
        }
    }

    public void Move()   
    {
        if (FindPlayer())
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, data.speed[level] * Time.deltaTime);
    }

    public void Shoot()       
    {
        if (isReadyToShoot() && FindPlayer())
        {
            var heading = (Vector2)Player.transform.position - (Vector2)transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;

            for (int i = 0; i < data.gunsData[level].numOfGuns; i++)
            {
                GameObject Ammo = GameObject.Instantiate(Resources.Load("Ammo") as GameObject, Guns[i].transform.position, Quaternion.identity);
                Ammo.GetComponent<AmmoController>().InitDirectionSpeedAndPower(direction, data.shootingSpeed[level], data.power[level]);
            }
            momentOfPreviousShot = Time.time;
        }
    }

    public void SetLevel(int level)
    {
        this.level = level;
        SetLifeGunsAndSprite();
    }

    public void SetData(LevelData data)
    {
        this.data = data;
    }

    private void SetLifeGunsAndSprite()
    {
        life = data.life[level];

        Guns = SetNewGuns();

        if (Resources.Load<Sprite>("Sprites/Enemy/" + level))   // If sprite exists
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Enemy/" + level);
    }

    private void OnTriggerEnter2D(Collider2D other)     // Check if Player shot enemy
    {
        if (other.tag == "MyAmmo" && FindPlayer())
        {
            int playerLevel = Player.GetComponent<PlayerController>().Level;
            life -= Player.GetComponent<PlayerController>().data.power[playerLevel];
            other.GetComponent<AmmoController>().ExplodeAmmo();
        }
    }
}