using UnityEngine;

public class CylinderWalls : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Rigidbody>() != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();


            //get the normal
            Vector3 normal = -(other.transform.position.normalized);
            Vector3 reflection = Vector3.Reflect(rb.linearVelocity, normal);
            rb.linearVelocity = reflection;
        }
    }
}
