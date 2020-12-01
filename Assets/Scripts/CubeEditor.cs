using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(WayPoint))]
public class CubeEditor : MonoBehaviour
{
    WayPoint wayPoint;

    private void Awake()
    {
        wayPoint = GetComponent<WayPoint>();
    }

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        UpdateText();
    }

    private void UpdateText()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = wayPoint.GetGridPosition().x  + "," + wayPoint.GetGridPosition().y;
        textMesh.text = labelText;
        gameObject.name = "Cube " + labelText;
    }

    private void SnapToGrid()
    {
        int gridSize = wayPoint.GetGridSize();
        transform.position = new Vector3(
            wayPoint.GetGridPosition().x * gridSize,
            0f, 
            wayPoint.GetGridPosition().y * gridSize
            );
    }
}