
using UnityEngine;

using UnityEngine.InputSystem;

public sealed class InputSystemMaster : MonoBehaviour
{
    public static InputSystemMaster instance { get; private set; }

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

    public void OnSelectWeaponNext(InputValue inputValue)
    {
        Player.instance.SelectWeaponNext();
    }

    public void OnSelectWeaponPrevious(InputValue inputValue)
    {
        Player.instance.SelectWeaponPrevious();
    }

    public void OnSwitchConsumableNext(InputValue inputValue)
    {
        Player.instance.SwitchConsumableNext();
    }

    public void OnSwitchConsumablePrevious(InputValue inputValue)
    {
        Player.instance.SwitchConsumablePrevious();
    }

    public void OnSwitchWeapon(InputValue inputValue)
    {
        Player.instance.SwitchWeapon();
    }

    public void OnReload()
    {
        Player.instance.ReloadWeapon();
    }
}