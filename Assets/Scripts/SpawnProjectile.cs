using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float forwardOffset;
    [SerializeField]
    private float heightOffset;
    [SerializeField]
    private AudioSource fireSound;

    public void SpawnNewProjectile()
    {
        Vector3 spawnPosition = this.transform.position;
        spawnPosition += forwardOffset * transform.forward + heightOffset * new Vector3(0, 1, 0);

        // Instantiate projectile with correct position and axis
        //TODO : ideally, pooling objects to avoid instantiate and destroy would be better
        GameObject projectileObject = Instantiate(projectilePrefab);
        projectileObject.transform.position = spawnPosition;
        projectileObject.GetComponent<Projectile>().setTranslationAxis(transform.forward);
        fireSound.Play();
    }
}
