using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionFX;
    [SerializeField] GameObject hitFX;    
    [SerializeField] float timeToDestroy = 1f;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int hitHealth = 0;

    GameObject parentGameobject;
    Scoring scoring;

    void Awake()
    {
        scoring = FindObjectOfType<Scoring>();
        parentGameobject = GameObject.FindWithTag("SpawnAtRuntime");
    }
    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        ProcessScore();
    }

    void ProcessHit()
    {
        GameObject fx = Instantiate(hitFX, transform.position, Quaternion.identity);
        
        hitHealth--;
        if(hitHealth < 1)
        {
            EnemyDeath();
        }
    }

    void EnemyDeath()
    {
        GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameobject.transform;
        Destroy(gameObject);
        Destroy(fx, timeToDestroy);
    }

    void ProcessScore()
    {
        scoring.IncreaseScore(scorePerHit);
    }
}
