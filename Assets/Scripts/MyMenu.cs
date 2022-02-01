using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyMenu : MonoBehaviour
{
    [MenuItem("My tools/Hello menu %g")]
    public static void HelloMenu()
    {
        Debug.Log("1menu");
    }
    
    [MenuItem("My tools/Hello menu2")]
    public static bool HelloMenuValidate()
    {
        return false;
    }
}
