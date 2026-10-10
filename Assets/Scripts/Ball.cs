using UnityEngine;

public class Ball : MonoBehaviour
{
    float maxHeight = 1f;
    float extraGravity = 1;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > maxHeight)
            rb.linearVelocity = rb.linearVelocity + Vector3.down * extraGravity; 
    }
}
