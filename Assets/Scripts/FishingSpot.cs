using UnityEngine;

public class FishingSpot : MonoBehaviour
{
    FishManager fishManager;
    public Fish myFish;
    public void Initialize(FishManager manager, Fish _fish)
    {
        fishManager = manager;
        myFish = _fish;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Ball>() != null)
        {
            Ball touchedBall = other.GetComponent<Ball>();

            if (touchedBall.caughtFish != null && touchedBall.caughtFish.visual != null && touchedBall.caughtFish.name != null)
                return;

            touchedBall.caughtFish = myFish;
            fishManager.DeActivateFishingSpot(this);
        }
    }
}
