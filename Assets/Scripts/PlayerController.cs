using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : SpaceObjectParent
{
    
    private void Start()
    {
        level = 0;
        life = data.life[level];
        SetGunsLifeAndSprite();
    }

    void Update()
    {
        if (!Pause.OnPause)
        {
            Move();
            Shoot();
            CheckIfDamageEnoughToExplode();
            CheckScore();
        }
    }

    private void Move()
    {
        movingDirection = Input.GetAxisRaw("Horizontal");
        transform.position = (Vector2)transform.position + new Vector2(1, 0) * data.speed[level] * movingDirection * Time.deltaTime;
    }

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && isReadyToShoot())
        {
            for (int i = 0; i < data.gunsData[level].numOfGuns; i++)
            {
                GameObject Ammo = GameObject.Instantiate(Resources.Load("Ammo") as GameObject, Guns[i].transform.position, Quaternion.identity);
                Ammo.tag = "MyAmmo";
                Ammo.GetComponent<AmmoController>().Speed = data.shootingSpeed[level];
                Ammo.GetComponent<AmmoController>().Power = data.power[level];
            }
            momentOfPreviousShot = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)     // Check collisions with ammos, asteroids and enemies
    {
        if (other.tag == "Ammo")
        {
            life -= other.GetComponent<AmmoController>().Power;
            other.GetComponent<AmmoController>().ExplodeAmmo();
        }
        if (other.tag == "Asteroid")
        {
            life -= other.GetComponent<AsteroidController>().Life;
            other.GetComponent<AsteroidController>().Explode();
        }
        if(other.tag == "Enemy")
        {
            life -= other.GetComponent<EnemyController>().Life;
            other.GetComponent<EnemyController>().Explode();
        }
    }

    private void CheckScore()   // Checking score to level up
    {
        // Do not level up if there is no next level in data
        if (Score.GetScore() >= data.scoresToNextLevel[level] && level < data.life.Count-1)
        {
            level++;
            SetGunsLifeAndSprite();
        }
    }

    public void SetGunsLifeAndSprite()       
    {
        Guns = SetNewGuns();
        life = data.life[level];
        if (Resources.Load<Sprite>("Sprites/Player/" + level))   // If sprite exists
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Player/" + level);
    }

}
