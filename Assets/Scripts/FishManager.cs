using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public GameObject fishingSpotPrefab;

    List<FishingSpot> activeFishingSpots;
    List<FishingSpot> inActivefishingSpots;

    public Animator fishCaughtAnimator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeFishingSpots = new List<FishingSpot>();
        inActivefishingSpots = new List<FishingSpot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
            SpawnFishingSpot();
    }

    public void SpawnFishingSpot()
    {
        FishingSpot newFishingSpot;
        if (inActivefishingSpots.Count > 0)
        {
            //take a fishing spot from the inactive fishing spots
            newFishingSpot = inActivefishingSpots[0];
            inActivefishingSpots.Remove(newFishingSpot);
            newFishingSpot.gameObject.SetActive(true);
        }
        else
        {
            newFishingSpot = Instantiate(fishingSpotPrefab).GetComponent<FishingSpot>();
            newFishingSpot.Initialize(this);
        }

        //set the location and other data, like the fish
        newFishingSpot.transform.position = new Vector3(Random.Range(-8f, 8f), 0f, Random.Range(-8f, 8f));

        activeFishingSpots.Add(newFishingSpot);
    }

    public void DeActivateFishingSpot(FishingSpot toRemove)
    {
        toRemove.gameObject.SetActive(false);

        activeFishingSpots.Remove(toRemove);
        inActivefishingSpots.Add(toRemove);
    }

    public void CatchFish(Fish _fish)
    {
        fishCaughtAnimator.SetTrigger("Test");
    }
}
