using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Debugger : MonoBehaviour
{
    public static Debugger Instance
    {
        get
        {
            return instance;
        }
    }

    private static Debugger instance = null;

    private void Awake()
    {
        
    }

    //make a way to create new values and windows
}
