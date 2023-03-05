using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRocket : MonoBehaviour
{
    public float force = 1000f;
    Rigidbody rocket;
    AudioSource rocketSound;

    public ParticleSystem mainBoosterParticles;
    public ParticleSystem leftBoosterParticles;
    public ParticleSystem rightBoosterParticles;

    public bool flightEnabled = true;

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

    void RotateRocket(KeyCode[] keys, float direction, ParticleSystem particles)
    {
      foreach (KeyCode key in keys)
      {
        if (Input.GetKey(key) && flightEnabled) {
          transform.Rotate(direction, 0, 0);
          if (!particles.isPlaying) {
            particles.Play();
          }
          break;
        } else if (particles.isPlaying) {
          particles.Stop();
        }
      }
    }

    void BoostRocket() {
      if (!Input.GetKey(KeyCode.Space) || !flightEnabled) {
        rocketSound.Stop();
        mainBoosterParticles.Stop();
        return;
      }

      float myForce = force * Time.deltaTime;
      rocket.AddRelativeForce(Vector3.up * myForce, ForceMode.Force);
      if (!rocketSound.isPlaying) {
        rocketSound.Play();
      }
      if (!mainBoosterParticles.isPlaying) {
        mainBoosterParticles.Play();
      }
    }

    void FixedUpdate()
    {
        BoostRocket();
        RotateRocket(goLeftKeys, rotationVelocity, rightBoosterParticles);
        RotateRocket(goRightKeys, -rotationVelocity, leftBoosterParticles);
    }
}
