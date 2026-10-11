using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool aiming;
    Vector3 aimDirection;

    public float shootStrength;

    public FishManager fishManager;
    public LayerMask ballLayers;

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
        for (int i = 0; i < 4; i++)
            fishManager.SpawnFishingSpot();
    }
}
