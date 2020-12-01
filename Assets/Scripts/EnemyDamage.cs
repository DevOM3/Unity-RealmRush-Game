using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem playerDiedParticle;
    [SerializeField] AudioClip gotDamageSFX;
    [SerializeField] AudioClip diedSFX;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        audioSource.PlayOneShot(gotDamageSFX);
        hitPoints -= 1;
        hitParticlePrefab.Play();
        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        var deadEffect = Instantiate(playerDiedParticle, new Vector3(transform.position.x, transform.position.y + 12.7f, transform.position.z), Quaternion.identity);
        deadEffect.Play();
        float destoryDelay = deadEffect.main.duration;
        Destroy(deadEffect.gameObject, destoryDelay);

        AudioSource.PlayClipAtPoint(diedSFX, Camera.main.transform.position);

        Destroy(gameObject);
    }
}
