
using UnityEngine;

using UnityEngine.InputSystem;

public class InputSystemManager : MonoBehaviour
{
    public static InputSystemManager instance { get; private set; }

    public void OnMove(InputValue inputValue)
    {
        Player.instance.movement.Move(inputValue.Get<Vector2>());
    }
    public void OnLook(InputValue inputValue)
    {
        Player.instance.movement.Look(inputValue.Get<Vector2>());
    }
    public void OnRun(InputValue inputValue)
    {
        Player.instance.movement.Run();
    }
    public void OnJump(InputValue inputValue)
    {
        Player.instance.movement.Jump();
    }
    public void OnConsumGrenade(InputValue inputValue)
    {
        Player.instance.inventory.ConsumGrenade();
    }
    public void OnConsumMedikit(InputValue inputValue)
    {
        Player.instance.inventory.ConsumMedikit();
    }
    public void OnSelectWeapon(InputValue inputValue)
    {

    }
    public void OnSwitchWeaponNext(InputValue inputValue)
    {
        Player.instance.inventory.SwitchWeaponNext();
    }
    public void OnSwitchWeaponPrevious(InputValue inputValue)
    {
        Player.instance.inventory.SwitchWeaponPrevious();
    }
    public void OnAttack(InputValue inputValue)
    {
        Player.instance.inventory.Attack(inputValue.isPressed);
    }
    public void OnReload(InputValue inputValue)
    {
        Player.instance.inventory.Reload();
    }
}