
using UnityEngine;

using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    

    public bool isNumericKeyPressed { get; set; } = false;

    public int numericKey { get; set; }

    private void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        instance = this;
    }

    
}