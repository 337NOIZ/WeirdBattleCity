
using UnityEngine;

using UnityEngine.InputSystem;

public sealed class InputSystemMaster : MonoBehaviour
{
    public void OnMove(InputValue inputValue)
    {
        Player.instance.Move(inputValue.Get<Vector2>());
    }

    public void OnLook(InputValue inputValue)
    {
        Player.instance.Look(inputValue.Get<Vector2>());
    }

    public void OnRun(InputValue inputValue)
    {
        Player.instance.Run();
    }

    public void OnJump(InputValue inputValue)
    {
        Player.instance.Jump();
    }
}