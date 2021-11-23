
using UnityEngine;

using UnityEngine.InputSystem;

public sealed class InputSystemMaster : MonoBehaviour
{
    public static InputSystemMaster instance { get; private set; }

    public void OnMove(InputValue inputValue)
    {
        Player.instance.playerMovement.Move(inputValue.Get<Vector2>());
    }
    public void OnLook(InputValue inputValue)
    {
        Player.instance.playerMovement.Look(inputValue.Get<Vector2>());
    }
    public void OnRun(InputValue inputValue)
    {
        Player.instance.playerMovement.Run();
    }
    public void OnJump(InputValue inputValue)
    {
        Player.instance.playerMovement.Jump();
    }
    public void OnConsumGrenade(InputValue inputValue)
    {
        Player.instance.playerInventory.ConsumGrenade();
    }
    public void OnConsumMedikit(InputValue inputValue)
    {
        Player.instance.playerInventory.ConsumMedikit();
    }
    public void OnSelectWeapon(InputValue inputValue)
    {

    }
    public void OnSwitchWeaponNext(InputValue inputValue)
    {
        Player.instance.playerInventory.SwitchWeaponNext();
    }
    public void OnSwitchWeaponPrevious(InputValue inputValue)
    {
        Player.instance.playerInventory.SwitchWeaponPrevious();
    }
    public void OnAttack(InputValue inputValue)
    {
        Player.instance.playerInventory.Attack(inputValue.isPressed);
    }
    public void OnReload(InputValue inputValue)
    {
        Player.instance.playerInventory.Reload();
    }
}