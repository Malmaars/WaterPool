using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HighlightInfo : Singleton<HighlightInfo>
{

    bool dataSelectionActive;
    Vector3 dataSelectionTargetLocation;

    DataHolder highlightedData;
    Transform highlightLocation;


    [Header("Customizable data")]
    public float dataSelectionMoveSpeed, dataSelectionFadeSpeed;

    [Header("Prefabs and sprites")]
    public Sprite ballTypeSprite; 
    public Sprite fishTypeSprite; 
    public Sprite fisherTypeSprite;

    [Header("Essential assignments")]
    public LayerMask highlightLayer;
    public RectTransform dataSelection;
    public CanvasGroup dataSelectionGroup;
    public Image dataImage, dataTypeIcon;
    public TextMeshProUGUI dataName, dataDescription, dataType;

    private void Start()
    {
        dataSelectionGroup.alpha = 0;
    }
    private void Update()
    {
        HandleFishSelection();
    }

    void HandleFishSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100, highlightLayer);

        if (hit.collider != null)
        {
            highlightLocation = hit.collider.transform;
            if (hit.collider.GetComponent<PhysicsBall>() != null)
                highlightedData = hit.collider.GetComponent<PhysicsBall>().myBallData;
            else if (hit.collider.GetComponent<FishingSpot>() != null)
                highlightedData = hit.collider.GetComponent<FishingSpot>().myFish;
            else if (hit.collider.GetComponent<FisherHighlighter>() != null)
                highlightedData = hit.collider.GetComponent<FisherHighlighter>().myData;
            else
                highlightedData = null;
        }
        else
            highlightedData = null;

        if(highlightedData != null)
            SetInfoData(highlightedData, highlightLocation);

        else
            HideInfoData();


        //now work on the visual
        if (dataSelectionActive)
        {
            if (dataSelectionGroup.alpha < 1f)
                dataSelectionGroup.alpha = (dataSelectionGroup.alpha >= 1f) ? 1f : dataSelectionGroup.alpha + Time.deltaTime * dataSelectionFadeSpeed;
        }

        else
        {
            if (dataSelectionGroup.alpha > 0f)
                dataSelectionGroup.alpha = (dataSelectionGroup.alpha <= 0f) ? 0f : dataSelectionGroup.alpha - Time.deltaTime * dataSelectionFadeSpeed;
        }
        dataSelection.anchoredPosition = Vector3.Lerp(dataSelection.anchoredPosition, dataSelectionTargetLocation, Time.deltaTime * dataSelectionMoveSpeed);
    }
    void SetInfoData(DataHolder _data, Transform _highlighted)
    {
        dataSelectionActive = true;

        dataSelectionTargetLocation = Camera.main.WorldToScreenPoint(_highlighted.position) + new Vector3(-48, 80, 0);

        if (dataSelectionGroup.alpha <= 0)
            dataSelection.anchoredPosition = dataSelectionTargetLocation;

        if (_data == null)
        {
            dataImage.sprite = null;
            dataImage.color = new Color(255, 255, 255, 0);
            dataName.text = "EMPTY";
            return;
        }

        dataImage.color = new Color(255, 255, 255, 255);

        switch (_data.GetType().ToString())
        {
            case "Fish":
                dataTypeIcon.sprite = fishTypeSprite;
                dataType.text = "FISH";
                break;
            case "Ball":
                dataTypeIcon.sprite = ballTypeSprite;
                dataType.text = "BALL";
                break;
            case "Fisher":
                dataTypeIcon.sprite = fisherTypeSprite;
                dataType.text = "FISHER";
                break;
        }

        dataDescription.text = _data.description;
        dataImage.sprite = _data.visual;
        dataName.text = _data.name;
    }

    void HideInfoData()
    {
        dataSelectionActive = false;
    }
}
