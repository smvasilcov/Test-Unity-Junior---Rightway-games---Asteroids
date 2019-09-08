using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObjectParent : MonoBehaviour
// Parent object for PlayerController, EnemyController, AsteroidController
{
    public LevelData data;          // Data source (scriptable object) whith all parameters (life, power, spawnDelay etc)
                                    // Player, Enemy snd Asteroid have own data files
                                    
    protected GameObject Player;        // Used by Enemy to look on player
    protected GameObject[] Guns = new GameObject[2];       // Points, from that enemy and Player shoot
    protected int level;    
    protected float life;   
    protected float momentOfPreviousShot = 0.0f;    // Used by isReadyToShoot method to shoot whith shooting delay
    protected float movingDirection = 0;            // Used by Player to move whith arrows 

    public float Life { get { return life; } set { life = value; } }        // Public realisation of life var
    public int Level { get { return level; } set { level = value; } }       // Public realisation of level var

    protected void LookAtPlayer()   // Method for enemies
    {
        if (Player)
        {
            Vector3 dir = Player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public GameObject FindPlayer()
    {
        if (GameObject.FindWithTag("Player"))
        {
            return GameObject.FindWithTag("Player");
        }
        else
            return null;
    }

    protected void CheckIfDamageEnoughToExplode()   // Check if damage is critical to be destroyed
    {
        if (life < 0)
        {
            Explode();
            if (FindPlayer())
                Score.AddScore(data.defeatScore[level]);
        }
    }

    public void Explode()
    {
        if (this.gameObject.tag == "Player") GameOver();    // Run GameOver music and pause the game
        if (Resources.Load<GameObject>("Explosion"))     // If explosion prefab exist
        {
            GameObject Explosion = Instantiate(Resources.Load<GameObject>("Explosion"), transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    protected bool isReadyToShoot()
    {
        if(!data) 
        {
            Debug.LogWarning($"{this.name} file cannot access to data file!");
            return false;
        }
        if (Time.time - momentOfPreviousShot > data.shootingDelay[level] * Time.deltaTime)
            return true;
        else
            return false;
    }

    public GameObject[] SetNewGuns()    // Called when Player levels up and to init enemy Guns  
    {
        // Remove all old Guns
        foreach (Transform oldGuns in GetComponentsInChildren<Transform>())
        {
            if (oldGuns.gameObject.name.Contains("Gun"))
                Destroy(oldGuns.gameObject);
        }

        //Setup new Guns      
        for (int i = 0; i < data.gunsData[level].numOfGuns; i++)
        {
            GameObject Gun = GameObject.Instantiate(Resources.Load("Gun") as GameObject, (Vector2)transform.position + data.gunsData[level].gunsPositionShift[i], Quaternion.identity);
            Gun.transform.SetParent(transform);
            Guns[i] = Gun;
        }
        return Guns;
    }

    private void GameOver()
    {
        AudioClip gameOverClip = Resources.Load<AudioClip>("Sounds/GameOver");
        AudioSource audioSource = Camera.main.GetComponent<AudioSource>();
        // Audiosource is on Main Camera because all another objects dissapear when gameover 
        audioSource.clip = gameOverClip;
        audioSource.Play();
        Pause.ChangePauseState();
    }



    // FOR TESTS
    public void SetMomentOfPrevShoot(float newMoment)
    {
        momentOfPreviousShot = newMoment;
    }

    public void SetLife(float newLife)
    {
        life = newLife;
    }

    public float GetLife()
    {
        return life;
    }
}
