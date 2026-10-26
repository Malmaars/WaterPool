using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishManager : Singleton<FishManager>
{
    public GameObject fishingSpotPrefab;

    List<FishingSpot> activeFishingSpots;
    List<FishingSpot> inActivefishingSpots;

    public Animator fishCaughtAnimator;

    public LayerMask ballLayer;
    public LayerMask fishingSpotLayer;


    public Image catchFishImage;

    public Fish[] possibleFishes;

    private void Awake()
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
            newFishingSpot.Initialize(this, possibleFishes[Random.Range(0, possibleFishes.Length)]);
        }
        else
        {
            newFishingSpot = Instantiate(fishingSpotPrefab).GetComponent<FishingSpot>();
            newFishingSpot.Initialize(this, possibleFishes[Random.Range(0, possibleFishes.Length)]);
        }

        //set the location and other data, like the fish
        newFishingSpot.transform.position = new Vector3(Random.Range(-7f, 7f), 0f, Random.Range(-7f, 7f));

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
        catchFishImage.sprite = _fish.visual;
        fishCaughtAnimator.SetTrigger("Test");
    }
}
