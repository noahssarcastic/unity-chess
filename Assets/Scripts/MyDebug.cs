using UnityEngine;

public static class MyDebug {

    public static void DrawRect(Vector3 start, Vector3 end, Color color, float duration=0) {
        // Reset depth because square will only ever be coplanar with x-y plane.
        float depth = 0;

        // Define vertices
        Vector3 vert1 = new Vector3(start.x, start.y, depth);
        Vector3 vert2 = new Vector3(start.x, end.y, depth);
        Vector3 vert3 = new Vector3(end.x, start.y, depth);
        Vector3 vert4 = new Vector3(end.x, end.y, depth);

        Debug.DrawLine(vert1, vert2, color, duration);
        Debug.DrawLine(vert1, vert3, color, duration);
        Debug.DrawLine(vert2, vert4, color, duration);
        Debug.DrawLine(vert3, vert4, color, duration);
    }

    // C = segements * len
    // 2 * pi * r = (360 / ang) * len
    // r = (360 * len) / (ang * 2 * pi)
    // r = (180 * len) / (ang * pi)
    // 180 * len = r * ang * pi
    // len = r * ang * pi / 180
    public static void DrawCircle(Vector3 center, float radius, Color color, int segments=360, float duration=0) {
        float degreeIncrement = 360 / segments;
        float lineLength = radius * degreeIncrement * Mathf.PI / 180;

        for (int i = 0; i < segments; i++) {
            float azimuth = i * degreeIncrement;
            Vector3 arc = PolarToCartesian(center, radius, azimuth);
            float lineAngle = azimuth + 90;
            Vector3 start = PolarToCartesian(arc, lineLength/2, lineAngle);
            Vector3 end = PolarToCartesian(arc, lineLength/2, lineAngle + 180);
            Debug.DrawLine(start, end, color, duration);
        }
    }

    public static Vector3 PolarToCartesian(Vector3 center, float radius, float angle) {
        float radianAngle = Mathf.Deg2Rad * angle;
        float x = radius * Mathf.Cos(radianAngle);
        float y = radius * Mathf.Sin(radianAngle);
        Vector3 coords = new Vector3(x, y);
        return center + coords;
    }
}