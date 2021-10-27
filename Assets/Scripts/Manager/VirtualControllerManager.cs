
using UnityEngine;

public class VirtualControllerManager : MonoBehaviour
{
    public void OnMove(Vector2 moveDirection)
    {
        Player.instance.movement.Move(moveDirection);
    }
    public void OnLook(Vector2 lookDirection)
    {
        Player.instance.movement.Look(lookDirection);
    }
    public void OnRun()
    {
        Player.instance.movement.Run();
    }
    public void OnJump()
    {
        Player.instance.movement.Jump();
    }
    public void OnConsumGrenade()
    {
        Player.instance.inventory.ConsumGrenade();
    }
    public void OnConsumMedikit()
    {
        Player.instance.inventory.ConsumMedikit();
    }
    public void OnSelectWeapon()
    {

    }
    public void OnSwitchWeaponNext()
    {
        Player.instance.inventory.SwitchWeaponNext();
    }
    public void OnSwitchWeaponPrevious()
    {
        Player.instance.inventory.SwitchWeaponPrevious();
    }
    public void OnAttack(bool state)
    {
        Player.instance.inventory.Attack(state);
    }
    public void OnReload()
    {
        Player.instance.inventory.Reload();
    }
}