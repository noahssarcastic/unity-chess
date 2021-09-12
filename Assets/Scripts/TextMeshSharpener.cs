using System;
using UnityEngine;


/**
 * Makes TextMesh look sharp regardless of camera size/resolution.
 * Do NOT change character size or font size; use scale only.
 * Source: http://answers.unity.com/answers/799174/view.html
 */
public class TextMeshSharpener: MonoBehaviour { 

    private float lastPixelHeight = -1;
    private TextMesh textMesh;

    void Start() {
        textMesh = GetComponent<TextMesh>();
        Resize();
    }

    void Update() {
        bool resolutionDidChange = Camera.main.pixelHeight != lastPixelHeight;
        bool isEditorMode = (Application.isEditor && !Application.isPlaying);
        if (resolutionDidChange || isEditorMode) Resize();
    }

    private void Resize() {
        float pixelHeight = Camera.main.pixelHeight;
        float cameraHalfSize = Camera.main.orthographicSize;
        float pixelRatio = (cameraHalfSize * 2.0f) / pixelHeight;
        float targetResolution = 128f;
        float biggerDimension = Math.Max(transform.localScale.x, transform.localScale.y);
        textMesh.characterSize = pixelRatio * cameraHalfSize / biggerDimension;
        textMesh.fontSize = Mathf.RoundToInt(targetResolution / textMesh.characterSize);
        lastPixelHeight = pixelHeight;
    }
}
