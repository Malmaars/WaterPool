using UnityEngine;

public class FishingSpot : MonoBehaviour
{
    FishManager fishManager;
    Fish myFish;
    public void Initialize(FishManager manager, Fish _fish)
    {
        fishManager = manager;
        myFish = _fish;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Ball>() != null)
        {
            other.GetComponent<Ball>().caughtFish = myFish;
            fishManager.DeActivateFishingSpot(this);
        }
    }
}
