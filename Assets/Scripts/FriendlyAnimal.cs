using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyAnimal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("trigger enter");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
