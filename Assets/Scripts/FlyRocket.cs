using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRocket : MonoBehaviour
{
    public float force = 1000f;
    Rigidbody rocket;
    AudioSource rocketSound;

    public KeyCode[] goLeftKeys = { KeyCode.A, KeyCode.LeftArrow };
    public KeyCode[] goRightKeys = { KeyCode.D, KeyCode.RightArrow };

    public float rotationVelocity = 10;

    // Update is called once per frame
    void Start()
    {
      rocket = GetComponent<Rigidbody>();
      rocketSound = GetComponent<AudioSource>();
      rocketSound.loop = true;
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
      if (!Input.GetKey(KeyCode.Space)) {
        rocketSound.Stop();
        rocket.freezeRotation = false;
        return;
      }

      rocket.freezeRotation = true;

      float myForce = force * Time.deltaTime;
      rocket.AddRelativeForce(Vector3.up * myForce, ForceMode.Force);
      if (!rocketSound.isPlaying) {
        rocketSound.Play();
      }
    }

    void FixedUpdate()
    {
        BoostRocket();
        RotateRocket(goLeftKeys, rotationVelocity);
        RotateRocket(goRightKeys, -rotationVelocity);
    }
}
