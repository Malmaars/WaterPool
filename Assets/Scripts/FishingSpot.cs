using UnityEngine;

public class FishingSpot : MonoBehaviour
{
    FishManager fishManager;
    public void Initialize(FishManager manager)
    {
        fishManager = manager;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Ball>() != null)
        {
            fishManager.DeActivateFishingSpot(this);
        }
    }
}
