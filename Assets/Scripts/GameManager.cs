using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool aiming;
    Vector3 aimDirection;

    public float shootStrength;

    public FishManager fishManager;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (aiming)
            {
                //stop aiming
                aiming = false;
            }
        }
    }
    public void StartAim()
    {
        aiming = true;

        //show an aim line

    }

    public void StartRound()
    {
        //spawn a couple of fishing spots or smth
        for (int i = 0; i < 4; i++)
            fishManager.SpawnFishingSpot();
    }
}
