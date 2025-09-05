using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public List<Transform> points = new List<Transform>();
    [SerializeField] Transform playerTransform;

    [Header("Line Settings")]
    [SerializeField] private float lineWidth = 0.1f; // biar gampang atur di inspector

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Set lebar garis sama di awal
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // Kalau mau lebih smooth, aktifkan ini
        lineRenderer.numCapVertices = 5;
        lineRenderer.numCornerVertices = 5;
    }

    public void AddLine(Transform point)
    {
        if (point == null) return;

        points.Add(point);
        RefreshLine();
    }

    public void AttachPlayer()
    {
        if (playerTransform != null && !points.Contains(playerTransform))
        {
            points.Add(playerTransform);
            RefreshLine();
        }
    }

    public void RemoveNewestLine()
    {
        if (points == null || points.Count == 0) return;

        points.RemoveAt(points.Count - 1);
        RefreshLine();
    }

    public void RefreshLine()
    {
        lineRenderer.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            if (points[i] != null)
                lineRenderer.SetPosition(i, points[i].position);
        }
    }

    private void Update()
    {
        if (points == null || points.Count == 0) return;

        RefreshLine();
    }
}
