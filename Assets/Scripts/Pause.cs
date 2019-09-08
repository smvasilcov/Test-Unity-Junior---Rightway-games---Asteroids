using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private static bool onPause = false;
    public static bool OnPause { get { return onPause; } }

    private void OnEnable()
    {
        onPause = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePauseState();
    }

    public static void ChangePauseState()
    {
        onPause = !onPause;
    }

    

    
}




