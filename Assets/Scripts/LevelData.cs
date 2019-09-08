using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "GameSettings/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public List<float> life = new List<float>();
    public List<float> speed = new List<float>();
    public List<float> power = new List<float>();
    public List<float> shootingDelay = new List<float>();
    public List<float> shootingSpeed = new List<float>();
    public List<float> spawnDelay = new List<float>();
    public List<float> defeatScore = new List<float>();
    public List<float> scoresToNextLevel = new List<float>();
    public List<GunsData> gunsData;

    public void ForceSerialization()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}


