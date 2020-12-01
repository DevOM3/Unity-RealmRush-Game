using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float enemyMovement = .5f;
    [SerializeField] ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();
        // When the method is the coroutine then it ust be called with the function
        StartCoroutine(FollowPath(path));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // coroutines is much like threads 
    // when the return type is the IEnumerator then the method works as Coroutine
    // then the method must return with the yield 
    IEnumerator FollowPath(List<WayPoint> path)
    {
        foreach(WayPoint wayPoint in path)
        {
            transform.position = wayPoint.transform.position;
            yield return new WaitForSeconds(enemyMovement); // yield does nothing but putting the transform in the list 
        }
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        var deadEffect = Instantiate(explosionParticle, new Vector3(transform.position.x, transform.position.y + 12.7f, transform.position.z), Quaternion.identity);
        deadEffect.Play();
        float destoryDelay = deadEffect.main.duration;
        Destroy(deadEffect.gameObject, destoryDelay);

        Destroy(gameObject);
    }
}
