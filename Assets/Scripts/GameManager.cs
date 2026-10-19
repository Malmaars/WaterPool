using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool aiming;
    Vector3 aimDirection;

    public float shootStrength;

    public FishManager fishManager;
    public LayerMask ballLayers;

    int targetPoints;
    int points;
    public TextMeshProUGUI currentPointsText, targetPointsText;
    public Slider pointSlider;


    private void Awake()
    {
        BlackBoard.gameManager = this;
    }

    private void Start()
    {
        StartRound();
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 100, ballLayers))
        {
            hit.collider.GetComponent<Ball>().OnClick();
        }

        if (Input.GetKeyDown(KeyCode.R)) 
            SceneManager.LoadScene(0);


        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void StartRound()
    {
        //spawn a couple of fishing spots or smth
        for (int i = 0; i < 5; i++)
            fishManager.SpawnFishingSpot();

        targetPoints = 5;
        points = 0;
        UpdatePointsVisuals();
    }

    public void EditPoints(int _editing)
    {
        points += _editing;
        UpdatePointsVisuals();
    }

    void UpdatePointsVisuals()
    {
        pointSlider.maxValue = targetPoints;
        pointSlider.value = points;
        currentPointsText.text = points.ToString();
        targetPointsText.text = targetPoints.ToString();
    }
}
