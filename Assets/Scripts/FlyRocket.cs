using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRocket : MonoBehaviour
{
    public float force = 1000f;
    Rigidbody rocket;

    public KeyCode[] goLeftKeys = { KeyCode.A, KeyCode.LeftArrow };
    public KeyCode[] goRightKeys = { KeyCode.D, KeyCode.RightArrow };

    public float rotationVelocity = 10;

    // Update is called once per frame
    void Start()
    {
      rocket = GetComponent<Rigidbody>();
    }

    void RotateRocket(KeyCode[] keys, float direction)
    {
      foreach (KeyCode key in keys)
      {
        if (Input.GetKey(key)) {
          transform.Rotate(direction, 0, 0);
        }
      }
    }

    void BoostRocket() {
      if (!Input.GetKey(KeyCode.Space)) return;

      float myForce = force * Time.deltaTime;

      float zForce = Mathf.Sin(transform.rotation.x) * myForce;
      float yForce = Mathf.Sin(90 - transform.rotation.x) * myForce;

      Vector3 forceApplied = new Vector3(0, yForce, zForce);
      rocket.AddForce(forceApplied, ForceMode.Force);
    }

    void FixedUpdate()
    {
        BoostRocket();
        RotateRocket(goLeftKeys, rotationVelocity);
        RotateRocket(goRightKeys, -rotationVelocity);
    }
}
