
using UnityEngine;

public class VirtualController : MonoBehaviour
{
    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.instance;
    }

    public void Look(Vector2 direction)
    {
        inputManager.Look(direction);
    }

    public void Move(Vector2 direction)
    {
        inputManager.Move(direction);
    }

    public void Attack(bool state)
    {
        inputManager.Attack(state);
    }

    public void Jump(bool state)
    {
        inputManager.Jump(state);
    }

    public void Run(bool state)
    {
        inputManager.Run(state);
    }
}
