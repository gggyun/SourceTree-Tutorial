using UnityEngine;

public class Path : MonoBehaviour
{
    public Vector3[] points;
    public float radius = 2f; // 멈추는 거리 Stop Distance

    public Vector3 GetPoint(int index)
    {
        return points[index];
    }

    void OnDrawGizmos()
    {
        if (points == null) return;

        for (int i = 0; i < points.Length - 1; i++)
        {
            if (i + 1 < points.Length)
                Gizmos.DrawLine(points[i], points[i + 1]);
        }
    }
}
