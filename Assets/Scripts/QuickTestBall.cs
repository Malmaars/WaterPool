using UnityEngine;

public class QuickTestBall : MonoBehaviour
{
    Camera cameraMain;
    public LayerMask rayMask;

    public float shotPower;
    Rigidbody rb;

    private void Start()
    {
        cameraMain = Camera.main;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //raycast to the water
            RaycastHit hit;
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, rayMask, QueryTriggerInteraction.Collide))
            {
                Vector3 newDirection = hit.point - transform.position;
                newDirection.y = 0;
                newDirection.Normalize();

                rb.AddForce(newDirection * shotPower, ForceMode.Impulse);
            }
        }
    }
}
