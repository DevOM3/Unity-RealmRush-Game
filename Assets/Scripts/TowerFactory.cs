using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower tower;
    [SerializeField] Transform towerParent;
    int towers = 0;

    Queue<Tower> towerQueue = new Queue<Tower>();

    // Start is called before the first frame update
    public void AddTower(WayPoint baseWayPoint)
    {
        towers = towerQueue.Count;
        if (towers < towerLimit)
        {
            InstantiateNewTower(baseWayPoint);
        }
        else
        {
            MoveTower(baseWayPoint);
        }
    }

    private void InstantiateNewTower(WayPoint baseWayPoint)
    {
        var towerInstance = Instantiate(tower, baseWayPoint.transform.position, Quaternion.identity);
        towerInstance.transform.parent = towerParent;
        baseWayPoint.isPlacable = false;

        towerInstance.baseWayPoint = baseWayPoint;
        towerQueue.Enqueue(towerInstance);
        
        towers++;
    }

    private void MoveTower(WayPoint baseWayPoint)
    {
        var oldTower = towerQueue.Dequeue();

        oldTower.baseWayPoint.isPlacable = true;
        baseWayPoint.isPlacable = false;

        oldTower.baseWayPoint = baseWayPoint;
        oldTower.transform.position = baseWayPoint.transform.position;

        towerQueue.Enqueue(oldTower);
        
    }
}
