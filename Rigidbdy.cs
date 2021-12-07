using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigidbdy : MonoBehaviour
{
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider){
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
