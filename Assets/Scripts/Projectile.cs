using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float translationSpeed;
    public Vector3 rotationSpeeds;
    private Vector3 translationAxis;
    public GameObject child;

    public void setTranslationAxis(Vector3 axis)
    {
        translationAxis = axis;
    }

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject); //TODO : ideally, pooling objects to avoid instantiate and destroy would be better
    }

    void Update()
    {
        child.transform.Rotate(rotationSpeeds * Time.deltaTime);
        transform.transform.position += translationAxis * translationSpeed * Time.deltaTime;
    }
}
