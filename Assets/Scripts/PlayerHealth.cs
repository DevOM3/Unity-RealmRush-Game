using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] int healthDecrease = -1;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip enemyReached;

    void Start()
    {
        healthText.text = health.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(enemyReached);
        health -= healthDecrease;
        healthText.text = health.ToString();
    }
}
