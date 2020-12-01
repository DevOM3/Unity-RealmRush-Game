using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    const int gridSize = 10;
    public bool isExplored = false;
    public WayPoint exploredFrom;
    [SerializeField] Color exploredColor;
    public bool isPlacable = true;
    [SerializeField] Tower tower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlacable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
        }
    }
}
