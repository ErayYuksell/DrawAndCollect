using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] GameObject LinePrefab;
    GameObject Line;

    LineRenderer lineRenderer;
    EdgeCollider2D edgeCollider;
    [SerializeField] List<Vector2> FingerTouchList = new List<Vector2>();



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(fingerPos, FingerTouchList[^1]) > .1f)
            {
                UpdateLine(fingerPos);
            }
        }
    }

    void CreateLine()
    {
        Line = Instantiate(LinePrefab, Vector2.zero, Quaternion.identity);
        lineRenderer = Line.GetComponent<LineRenderer>();
        edgeCollider = Line.GetComponent<EdgeCollider2D>();
        FingerTouchList.Clear();
        FingerTouchList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        FingerTouchList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, FingerTouchList[0]);
        lineRenderer.SetPosition(1, FingerTouchList[1]);
        edgeCollider.points = FingerTouchList.ToArray();
    }

    void UpdateLine(Vector2 takeTheFingerPos)
    {
        FingerTouchList.Add(takeTheFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, takeTheFingerPos);
        edgeCollider.points = FingerTouchList.ToArray();
    }
}
