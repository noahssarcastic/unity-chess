using UnityEngine;

public static class WorldText
{
    public const int sortingOrderDefault = 5000;

    public static TextMesh CreateWorldText(string text, Vector3 pos, Color color) {
        TextAnchor textAnchor = TextAnchor.MiddleCenter;
        TextAlignment textAlignment = TextAlignment.Center;
        int sortingOrder = sortingOrderDefault;

        GameObject gameObject = new GameObject("Debug", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.position = pos;

        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = 1;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        // Sharpen textmesh
        gameObject.AddComponent<TextMeshSharpener>();
        gameObject.transform.localScale = new Vector3(0.05f, 0.05f);

        return textMesh;
    }

}