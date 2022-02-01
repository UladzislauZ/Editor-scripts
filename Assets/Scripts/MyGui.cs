using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGui : MonoBehaviour
{
    private void OnGUI()
    {
        Rect rect = new Rect(200, 200, 50, 200);
        GUI.Label(rect,"h1");
    }
}
