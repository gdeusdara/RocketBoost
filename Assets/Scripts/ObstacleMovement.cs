using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    Vector3 startingPosition;
    public float period = 5;

    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] Vector3 movementVector = new Vector3(8, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        const float tau = Mathf.PI * 2;
        if (period <= Mathf.Epsilon) return;
    
        float cycles = Time.time / period;

        float MovInSin = Mathf.Sin(cycles * tau);

        movementFactor = (MovInSin + 1) / 2;

        Vector3 offset = movementVector * movementFactor;

        transform.position = startingPosition + offset;
    }
}
