using System;
using UnityEngine;

/*
    Makes TextMesh look sharp regardless of camera size/resolution
    Do NOT change character size or font size; use scale only
*/
public class TextMeshSharpener:MonoBehaviour 
{ 
    // Properties

    private float lastPixelHeight = -1;
    private TextMesh textMesh;

    // MonoBehavior Methods

    void Start() {
        textMesh = GetComponent<TextMesh>();
        resize();
    }

    void Update() {
        // Always resize in the editor, or when playing the game, only when the resolution changes
        if (Camera.main.pixelHeight != lastPixelHeight || (Application.isEditor && !Application.isPlaying)) resize();
    }

    // Helper functions

    private void resize() {
        float ph = Camera.main.pixelHeight;
        float ch = Camera.main.orthographicSize;
        float pixelRatio = (ch * 2.0f) / ph;
        float targetRes = 128f;
        textMesh.characterSize = pixelRatio * Camera.main.orthographicSize / Math.Max(transform.localScale.x, transform.localScale.y);
        textMesh.fontSize = (int)Math.Round(targetRes / textMesh.characterSize);
        lastPixelHeight = ph;
    }
}
