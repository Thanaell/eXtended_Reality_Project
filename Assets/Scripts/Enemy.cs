using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

abstract public class Enemy : MonoBehaviour
{
    public GameObject particleSystem;

    // Set bool to true if the mmole is flying
    public UnityEvent<bool> moleDestroyed;

    private IEnumerator despawnCoroutine;
    
    protected virtual void Start()
    {
        moleDestroyed.AddListener(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemySpawner>().MoleDespawned);
    }

    void Update()
    {
        Move();
    }

    protected abstract void InvokeDestroyEvent();

    private IEnumerator DespawnCoroutine()
    {
        particleSystem.GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.7f);
        particleSystem.GetComponent<ParticleSystem>().Stop();

        InvokeDestroyEvent();
        Destroy(this.gameObject);
    }

    void Despawn()
    {
        despawnCoroutine = DespawnCoroutine();
        StartCoroutine(despawnCoroutine);
    }

    void OnTriggerEnter(Collider other)
    {
        Despawn();
    }

    protected abstract void Move();
}
