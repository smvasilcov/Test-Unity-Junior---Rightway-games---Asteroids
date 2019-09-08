using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float explosionLength;
    private Sprite[] spriteVariants;
    private AudioClip[] soundVariants;

    void Start()
    {
        DrawExplosion();
        MakeSound();
    }

    IEnumerator Clean()
    {
        yield return new WaitForSeconds(explosionLength);
            Destroy(this.gameObject);
    }

    private void DrawExplosion()
    {
        spriteVariants = Resources.LoadAll<Sprite>("Sprites/Explosion");
        int explosionID = Random.Range(0,spriteVariants.Length);
        this.GetComponent<SpriteRenderer>().sprite = spriteVariants[explosionID];
        StartCoroutine("Clean");
    }

    private void MakeSound()
    {
        soundVariants = Resources.LoadAll<AudioClip>("Sounds/Explosion");
        int explosionID = Random.Range(0,soundVariants.Length);
        this.GetComponent<AudioSource>().clip = soundVariants[explosionID];
        this.GetComponent<AudioSource>().Play();
    }
}
