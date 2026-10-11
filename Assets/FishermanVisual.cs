using UnityEngine;

public class FishermanVisual : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform target;
    public Transform fishingRodBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, fishingRodBase.position);
        lineRenderer.SetPosition(1, target.position);
    }
}
