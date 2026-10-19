using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float maxHeight = 1f;
    float extraGravity = 1;
    Rigidbody rb;

    public float minShotPower = 50f;
    public float maxShotPower = 800f;
    bool dragging;
    float shotPower;
    float shotPowerMultiplier = 100;
    Vector3 shotDirection;

    public LineRenderer lineRenderer;

    public Fish caughtFish;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (dragging)
        {
            Plane plane = new Plane(Vector3.up, 0);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float enter = 0.0f;

            if (plane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                hitPoint.y = 0;
                Vector3 myLocationOnZero = new Vector3(transform.position.x, 0, transform.position.z);

                shotDirection = (myLocationOnZero - hitPoint).normalized;
                shotPower = Vector3.Distance(myLocationOnZero, hitPoint) * shotPowerMultiplier;

                if (shotPower > maxShotPower)
                    shotPower = maxShotPower;

                if(shotPower < minShotPower)
                    shotPower = minShotPower;

                lineRenderer.SetPositions(new Vector3[] { transform.position , transform.position + shotDirection * (shotPower / shotPowerMultiplier)});
            }

            if (Input.GetMouseButtonDown(1))
            {
                dragging = false;
                lineRenderer.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
            }

            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                lineRenderer.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });


                if (shotPower >= minShotPower)
                {
                    //shoot
                    rb.AddForce(shotDirection * shotPower, ForceMode.Impulse);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > maxHeight)
            rb.linearVelocity = rb.linearVelocity + Vector3.down * extraGravity; 
    }

    public void OnClick()
    {
        dragging = true;
    }

    public void ThrowBack()
    {
        caughtFish = null;
        transform.position = new Vector3(Random.Range(-6f, 6f),0, Random.Range(-6f, 6f));
        rb.linearVelocity = Vector3.zero;
    }
}
