using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRocket : MonoBehaviour
{
    public float force = 1000f;

    Rigidbody rocket;

    // Update is called once per frame
    void Start() {
      rocket = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        // Debug.Log("Entrou aquiii");
        float myForce = force * Time.deltaTime;

        float zForce = Mathf.Sin(transform.rotation.x) * myForce;
        float yForce = Mathf.Sin(90 - transform.rotation.x) * myForce;

        Vector3 forceApplied = new Vector3(0, yForce, zForce);
        rocket.AddForce(forceApplied, ForceMode.Force);
    }
}
