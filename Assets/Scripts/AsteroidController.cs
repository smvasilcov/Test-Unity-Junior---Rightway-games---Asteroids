using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : SpaceObjectParent
{

    public AsteroidSpawner asteroidSpawner;
    private void OnEnable()
    {
        // Init new "empty" Asteroid object
        AsteroidSpawner asteroidSpawner = GameObject.FindWithTag("Spawner").GetComponent<AsteroidSpawner>();
        data = asteroidSpawner.asteroidData;
        level = 0;
        Player = FindPlayer();
        SetLifeAndSprite();
    }

    void Update()
    {
        if (!Pause.OnPause)
        {
            Move();
            CheckIfDamageEnoughToExplode();
        }
    }

    private void Move()
    {
        transform.position = (Vector2)transform.position + new Vector2(0, -1) * data.speed[level] * Time.deltaTime;
    }

    public void SetLevel(int level)
    {
        this.level = level;
        SetLifeAndSprite();
    }

    private void SetLifeAndSprite()
    {
        life = data.life[level];

        if (Resources.Load<Sprite>("Sprites/Asteroid/" + level))   // If sprite exists
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Asteroid/" + level);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MyAmmo" && FindPlayer())
        {
            int playerLevel = Player.GetComponent<PlayerController>().Level;
            life -= Player.GetComponent<PlayerController>().data.power[playerLevel];
            other.GetComponent<AmmoController>().ExplodeAmmo();
            
        }

    }
}
