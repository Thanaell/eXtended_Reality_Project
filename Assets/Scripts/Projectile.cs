using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float translationSpeed;
    [SerializeField]
    private Vector3 rotationSpeeds;
    [SerializeField]
    private GameObject child;

    private Vector3 translationAxis;

    public void setTranslationAxis(Vector3 axis)
    {
        translationAxis = axis;
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO : ideally, pooling objects to avoid instantiate and destroy would be better
        Destroy(this.gameObject); 
    }

    void Update()
    {
        // Projectile rotates on itself
        child.transform.Rotate(rotationSpeeds * Time.deltaTime);
        // The whole GameObject(particle system included) translates
        transform.transform.position += translationAxis * translationSpeed * Time.deltaTime;
    }
}
