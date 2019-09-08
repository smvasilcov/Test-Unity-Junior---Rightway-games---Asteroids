using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    private float speed;
    private float power;
    private Vector2 direction;

    public float Speed { get { return speed; } set { speed = value; } }
    public float Power { get { return power; } set { power = value; } }
    public Vector2 Direction { get { return direction; } set { direction = value; } }

    private void Start()
    {
        MakeSound();
    }

    void Update()
    {
        if (!Pause.OnPause)
            MoveAmmo();
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void MoveAmmo()
    {
        if (direction == Vector2.zero)      // if there is no direction
            transform.position = (Vector2)transform.position + new Vector2(0, 1) * speed * Time.deltaTime;
        else                                // if there is aim direction
            transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;
    }

    public void ExplodeAmmo()
    {
        if (Resources.Load<GameObject>("AmmoExplosion"))     // If explosion prefab exist
        {
            GameObject Explosion = Instantiate(Resources.Load<GameObject>("AmmoExplosion"), transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
            Destroy(this.gameObject);
        Destroy(this.gameObject);
    }

    private void MakeSound()
    {
        AudioClip[] soundVariants = Resources.LoadAll<AudioClip>("Sounds/Gun");
        int gunID = Random.Range(0,soundVariants.Length);
        this.GetComponent<AudioSource>().clip = soundVariants[gunID];
        this.GetComponent<AudioSource>().Play();
    }

    public void InitDirectionSpeedAndPower(Vector2 direction, float speed, float power)
     // Is used to init current ammo from shooter object (Player or Enemy)    
    {
        this.direction = direction;
        this.speed = speed;
        this.power = power;
    }

}

