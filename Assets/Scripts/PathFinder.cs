using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // The dictonary 
    Dictionary<Vector2Int, WayPoint> grid = new Dictionary<Vector2Int, WayPoint>();
    Queue<WayPoint> queue = new Queue<WayPoint>();
    [SerializeField] bool isRunning = true;
    WayPoint searchCenter;
    [SerializeField] private List<WayPoint> path;

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    [SerializeField] WayPoint startWaypoint, endWaypoint;

    public List<WayPoint> GetPath()
    {
        if (path.Count == 0)
        {
            // get Everything of the WayPoint
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath()
    {
        LoadBlock();

        BreadthFirstSearch();
        CreatePath();
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);
        WayPoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }

        SetAsPath(startWaypoint);
        path.Reverse();
    }

    private void SetAsPath(WayPoint wayPoint)
    {
        path.Add(wayPoint);
        wayPoint.isPlacable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            HaltIfEndFound();
            ExploreNeighbours();
            searchCenter.isExplored = true;
        }
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void LoadBlock()
    {
        WayPoint[] wayPoints = FindObjectsOfType<WayPoint>();
        foreach(WayPoint wayPoint in wayPoints)
        {
            if (grid.ContainsKey(wayPoint.GetGridPosition()))
            {
                Debug.LogWarning("Block " + wayPoint + " is Overlapping");
            }
            else
            {
                grid.Add(wayPoint.GetGridPosition(), wayPoint);
            }
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning ) { return; }
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = (searchCenter.GetGridPosition() + direction);
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbour(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbour(Vector2Int neighbourCoordinates)
    {
        WayPoint neighbours = grid[neighbourCoordinates];
        if (!neighbours.isExplored && !queue.Contains(neighbours)) {
            queue.Enqueue(neighbours);
            neighbours.exploredFrom = searchCenter;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
