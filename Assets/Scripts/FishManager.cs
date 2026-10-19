using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FishManager : MonoBehaviour
{
    public GameObject fishingSpotPrefab;

    List<FishingSpot> activeFishingSpots;
    List<FishingSpot> inActivefishingSpots;

    public Animator fishCaughtAnimator;

    public LayerMask ballLayer;
    public LayerMask fishingSpotLayer;
    public Ball highlightedBall;
    public FishingSpot highlightedFishingSpot;

    public GameObject fishSelection;
    public Image fishImage;
    public TextMeshProUGUI fishName;
    public Image catchFishImage;

    public Fish[] possibleFishes;

    private void Awake()
    {
        BlackBoard.fishManager = this;
        activeFishingSpots = new List<FishingSpot>();
        inActivefishingSpots = new List<FishingSpot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
            SpawnFishingSpot();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100, ballLayer);

        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<Ball>() != null)
                highlightedBall = hit.collider.GetComponent<Ball>();
            else
                highlightedBall = null;
        }
        else
            highlightedBall = null;

        Physics.Raycast(ray, out hit, 100, fishingSpotLayer);

        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<FishingSpot>() != null)
                highlightedFishingSpot = hit.collider.GetComponent<FishingSpot>();
            else
                highlightedFishingSpot = null;
        }
        else
            highlightedFishingSpot = null;

        if (highlightedBall != null)
        {
            fishSelection.SetActive(true);

            Debug.Log(highlightedBall.caughtFish);
            if(highlightedBall.caughtFish == null || highlightedBall.caughtFish.visual == null || highlightedBall.caughtFish.name == null)
            {
                fishImage.sprite = null;
                fishImage.color = new Color(255, 255, 255, 0);
                fishName.text = "EMPTY";
                Debug.Log("Show that it's empty");
            }
            else
            {
                fishImage.sprite = highlightedBall.caughtFish.visual;
                fishImage.color = new Color(255, 255, 255, 255);
                fishName.text = highlightedBall.caughtFish.name;
                Debug.Log("Show that it's full");

            }
        } 
        else if(highlightedFishingSpot != null)
        {
            fishSelection.SetActive(true);

            if (highlightedFishingSpot.myFish == null || highlightedFishingSpot.myFish.visual == null || highlightedFishingSpot.myFish.name == null)
            {
                fishImage.sprite = null;
                fishImage.color = new Color(255, 255, 255, 0);
                fishName.text = "EMPTY";
            }
            else
            {
                fishImage.sprite = highlightedFishingSpot.myFish.visual;
                fishImage.color = new Color(255, 255, 255, 255);
                fishName.text = highlightedFishingSpot.myFish.name;
            }

        }
        else
            fishSelection.SetActive(false);
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
