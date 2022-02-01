using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MyEditorWindow : EditorWindow
{
    public Texture2D _texture2D;
    private Color _paintColor;
    private Color _eraseColor;
    private GameObject _gameObject;
    private Color[,] _colors = new Color[8,8];
    
    private void OnEnable()
    {
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                _colors[i,j] = Color.white;
            }
        }
    }

    [MenuItem("Window/My Editor Window")]
    private static void OpenMyEditorWindow()
    {
        GetWindow<MyEditorWindow>();
    }

    private Texture2D SetTextureColor(Color color)
    {
        _texture2D = new Texture2D(32,32);
        for (int i = 0; i < _texture2D.width; i++)
            for (int j = 0; j < _texture2D.height; j++)
                _texture2D.SetPixel(i,j, color);
        _texture2D.Apply();
        return _texture2D;
    }
    
    private void OnGUI()
    {
        Event evnt = Event.current;
        GUI.Label(new Rect(10,10,150,20),"Toolbar");
        _paintColor = EditorGUI.ColorField(new Rect(10,30,150,20),_paintColor);
        _eraseColor = EditorGUI.ColorField(new Rect(10,55,150,20),(_eraseColor));

        for (int i = 0; i < 8; i++)
        {
            var oldColor = GUI.color;
            for (int j = 0; j < 8; j++)
            {
                Rect r = new Rect(250 + (j * 42), 10 + (i * 42), 32, 32);
                GUI.Box(r,SetTextureColor(_colors[i,j]));
                if (evnt.type == EventType.MouseDown)
                {
                    if (r.Contains(evnt.mousePosition))
                    {
                        if (evnt.button == 0)
                        {
                            _colors[i, j] = _paintColor;
                            Event.current.Use();
                        }

                        if (evnt.button == 1)
                        {
                            _colors[i, j] = _eraseColor;
                            Event.current.Use();
                        }
                    }
                }
            }

            GUI.color = oldColor;
        }

        if (GUI.Button(new Rect(10, 80, 150, 20), "Fill all"))
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    _colors[i, j] = _paintColor;
        }
        
        _gameObject = (GameObject) EditorGUI.ObjectField(new Rect(10, 300, 150, 30), _gameObject, typeof(GameObject));
        if (GUI.Button(new Rect(10,340,150,20),"Save to object"))
        {
            Texture2D texture2D = new Texture2D(8,  8);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    texture2D.SetPixel(i, j, _colors[i,j]);
            
            texture2D.Apply();

            _gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture = texture2D;
        }
        
    }
}

public class Pixel
{
    public Color _color;
    public Rect _rect;

    public Pixel(Color color, Rect rect)
    {
        _color = color;
        _rect = rect;
    }
}