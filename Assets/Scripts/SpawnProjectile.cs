using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float forwardOffset;
    public float heightOffset;
    public AudioSource fireSound;

    // TODO instantiate without parent
    public void spawnProjectile()
    {
        Vector3 spawnPosition = this.transform.position;
        spawnPosition += forwardOffset * transform.forward + heightOffset * new Vector3(0, 1, 0);

        // Instantiate projectile with correct position and axis
        GameObject projectileObject = Instantiate(projectilePrefab);
        projectileObject.transform.position = spawnPosition;
        projectileObject.GetComponent<Projectile>().setTranslationAxis(transform.forward);
        fireSound.Play();
    }

    private void Start()
    {

    }

    private void Update()
    {
    }
}
