using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Texture2D mouseTexture;

    private void Awake()
    {
        Cursor.SetCursor(mouseTexture, Vector2.zero, CursorMode.Auto);
    }
}
