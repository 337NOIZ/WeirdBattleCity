
using UnityEngine;

public sealed class VirtualController : MonoBehaviour
{
    public void OnMove(Vector2 moveDirection)
    {
        Player.instance.playerMovement.Move(moveDirection);
    }

    public void OnLook(Vector2 lookDirection)
    {
        Player.instance.playerMovement.Look(lookDirection);
    }

    public void OnRun()
    {
        Player.instance.playerMovement.Run();
    }

    public void OnJump()
    {
        Player.instance.playerMovement.Jump();
    }

    public void OnConsumGrenade()
    {
        Player.instance.playerInventory.ConsumGrenade();
    }

    public void OnConsumMedikit()
    {
        Player.instance.playerInventory.ConsumMedikit();
    }

    public void OnSelectWeapon()
    {

    }

    public void OnSwitchWeaponNext()
    {
        Player.instance.playerInventory.SwitchWeaponNext();
    }

    public void OnSwitchWeaponPrevious()
    {
        Player.instance.playerInventory.SwitchWeaponPrevious();
    }

    public void OnAttack(bool state)
    {
        Player.instance.playerInventory.Attack(state);
    }

    public void OnReload()
    {
        Player.instance.playerInventory.Reload();
    }
}