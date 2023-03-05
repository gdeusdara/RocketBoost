using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugging : MonoBehaviour
{
    public playerCollider pc;
    
    void goToNextLevel() {
      pc.NextScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
          pc.collisionsEnabled = !pc.collisionsEnabled;
        }
        if (Input.GetKeyDown(KeyCode.L)) {
          goToNextLevel();
        }
    }
}
