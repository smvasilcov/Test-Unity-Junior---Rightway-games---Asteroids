using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelData))]
[CanEditMultipleObjects]
public class LevelDataEditor : Editor
{

    SerializedProperty lifeArray;
    SerializedProperty powerArray;
    SerializedProperty gunsDataArray;

    int level = 0;
    float life;
    float speed;
    float power;
    float shootingDelay;
    float shootingSpeed;
    float spawnDelay;
    float defeatScore;
    float scoresToNextLevel;
    GunsData gunsData = new GunsData();


    LevelData data;

    void Start()
    {

        lifeArray = serializedObject.FindProperty("life");
        powerArray = serializedObject.FindProperty("power");
        gunsDataArray = serializedObject.FindProperty("gunsData");
    }

    public override void OnInspectorGUI()
    {
        data = (LevelData)target;
        serializedObject.Update();

        // EditorGUILayout.PropertyField(lifeArray, true);
        // EditorGUILayout.PropertyField(powerArray, true);
        // EditorGUILayout.PropertyField(gunsDataArray, true);
        GUILayout.Label("Press Prev or Next butttons to check parameters");
        GUILayout.Label("Here you can change parameters and add new levels");
        GUILayout.Label("You can search through levels just typing level in the Level field");
        GUILayout.Space(10f);
        EditorGUILayout.BeginHorizontal();

        EditorGUI.BeginChangeCheck();
        level = EditorGUILayout.IntField("Level", level);
        if (EditorGUI.EndChangeCheck())
        {
            ShowData(level);
            serializedObject.Update();
        }
        if (GUILayout.Button("Prev"))
            Prev();

        if (GUILayout.Button("Next"))
            Next();

        EditorGUILayout.EndHorizontal();
        EditorGUI.BeginChangeCheck();
        life = EditorGUILayout.FloatField("Life", life);
        speed = EditorGUILayout.FloatField("Speed", speed);
        power = EditorGUILayout.FloatField("Power", power);
        shootingDelay = EditorGUILayout.FloatField("Shooting delay", shootingDelay);
        shootingSpeed = EditorGUILayout.FloatField("Shooting speed", shootingSpeed);
        spawnDelay = EditorGUILayout.FloatField("Spawn delay", spawnDelay);
        defeatScore = EditorGUILayout.FloatField("Defeat score", defeatScore);
        scoresToNextLevel = EditorGUILayout.FloatField("Scores to next level", scoresToNextLevel);
        if (EditorGUI.EndChangeCheck())
            SaveChanges();

        EditorGUI.BeginChangeCheck();
        gunsData.numOfGuns = EditorGUILayout.IntField("Num of guns", gunsData.numOfGuns);
        if (EditorGUI.EndChangeCheck())
        {
            gunsData.Guns = new GameObject[gunsData.numOfGuns];
            gunsData.gunsPositionShift = new Vector2[gunsData.numOfGuns];
        }

        EditorGUI.BeginChangeCheck();
        for (int i = 0; i < gunsData.numOfGuns; i++)
        {
            gunsData.gunsPositionShift[i] = EditorGUILayout.Vector2Field("Position shift", gunsData.gunsPositionShift[i]);
        }
        if (EditorGUI.EndChangeCheck())
            SaveChanges();

        GUILayout.Space(10f);

        if (GUILayout.Button("Add new Level"))
            AddLevel();

        if (GUILayout.Button("Delete Level"))
            DeleteLevel();

        serializedObject.ApplyModifiedProperties();
    }

    private void SaveChanges()
    {
        data.life[level] = life;
        data.speed[level] = speed;
        data.power[level] = power;
        data.shootingDelay[level] = shootingDelay;
        data.shootingSpeed[level] = shootingSpeed;
        data.spawnDelay[level] = spawnDelay;
        data.defeatScore[level] = defeatScore;
        data.scoresToNextLevel[level] = scoresToNextLevel;
        data.gunsData[level] = gunsData;
        serializedObject.Update();
        data.ForceSerialization();
    }

    public void ShowData(int newLevel)
    {
        if (newLevel > data.life.Count - 1) level = 0;
        if (newLevel < 0) level = data.life.Count - 1;
        life = data.life[level];
        speed = data.speed[level];
        power = data.power[level];
        shootingDelay = data.shootingDelay[level];
        shootingSpeed = data.shootingSpeed[level];
        spawnDelay = data.spawnDelay[level];
        defeatScore = data.defeatScore[level];
        scoresToNextLevel = data.scoresToNextLevel[level];
        gunsData = data.gunsData[level];
        serializedObject.Update();
    }

    public void Prev()
    {
        ShowData(--level);
    }

    public void Next()
    {
        ShowData(++level);
    }

    public void AddLevel()
    {
        level = data.life.Count;
        data.life.Add(0);
        data.speed.Add(0);
        data.power.Add(0);
        data.shootingDelay.Add(0);
        data.shootingSpeed.Add(0);
        data.spawnDelay.Add(0);
        data.defeatScore.Add(0);
        data.scoresToNextLevel.Add(0);
        data.gunsData.Add(new GunsData());
        life = speed = power = shootingDelay = shootingSpeed = spawnDelay = defeatScore = scoresToNextLevel = 0;
        gunsData = new GunsData();
    }

    public void DeleteLevel()
    {
        if (data.life.Count < 1)
            return;
        data.life.RemoveAt(level);
        data.speed.RemoveAt(level);
        data.power.RemoveAt(level);
        data.shootingDelay.RemoveAt(level);
        data.shootingSpeed.RemoveAt(level);
        data.spawnDelay.RemoveAt(level);
        data.defeatScore.RemoveAt(level);
        data.scoresToNextLevel.RemoveAt(level);
        data.gunsData.RemoveAt(level);
        ShowData(level);
    }


}
