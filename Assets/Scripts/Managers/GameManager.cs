using DebugUI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : Singleton<GameManager>
{
    bool aiming;
    Vector3 aimDirection;

    public float shootStrength;

    public FishManager fishManager;
    public LayerMask ballLayers;

    int targetPoints;
    int points;
    public TextMeshProUGUI currentPointsText, targetPointsText;
    public UnityEngine.UI.Slider pointSlider;

    public Animator cameraAnimator;
    public static UIDocument uiDocument;


    private void Awake()
    {
        uiDocument = FindFirstObjectByType<UIDocument>();
    }

    private void Start()
    {
        StartRound();

        var builder = new DebugUIBuilder();
        builder.AddLabel("Camera Settings");
        builder.AddButton("toggle OrthoGraphic", () => cameraAnimator.SetBool("OrthoGraphic", !cameraAnimator.GetBool("OrthoGraphic")));
        builder.AddButton("toggle Camera view", () => cameraAnimator.SetBool("CameraView", !cameraAnimator.GetBool("CameraView")));
        builder.ConfigureWindowOptions(options =>
        {
            options.Draggable = true;
        });
        builder.BuildWith(uiDocument);
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 100, ballLayers))
        {
            hit.collider.GetComponent<PhysicsBall>().OnClick();
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
