using UnityEngine;

public class Fisher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            //catch the ball
            ReelIn(other.gameObject.GetComponent<Ball>());
        }
    }

    void ReelIn(Ball toReel)
    {
        if(toReel.caughtFish != null && toReel.caughtFish.visual != null && toReel.caughtFish.name != null)
        {
            //catch the fish and calculate points?
            BlackBoard.fishManager.CatchFish(toReel.caughtFish);
            Destroy(toReel.gameObject);
        }
    }
}
