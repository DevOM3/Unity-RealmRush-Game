using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawns = 1f;
    [SerializeField] EnemyMovement enemyPrefab;  
    [SerializeField] Transform enemyParent;
    [SerializeField] Text spawnedEnemies;
    [SerializeField] AudioClip spawnEnemySFX;

    int score;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        spawnedEnemies.text = score.ToString();
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GetComponent<AudioSource>().PlayOneShot(spawnEnemySFX);
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParent;
            IncreaseScore();
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private void IncreaseScore()
    {
        score++;
        spawnedEnemies.text = score.ToString();
    }
}
