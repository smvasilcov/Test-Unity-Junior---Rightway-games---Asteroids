using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunsData
{
    public int numOfGuns;
    public GameObject[] Guns = new GameObject[2];
    public Vector2[] gunsPositionShift = new Vector2[2];
}
