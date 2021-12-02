
using UnityEngine;

public sealed class VirtualController : MonoBehaviour
{
    public void OnLook(Vector2 lookDirection)
    {
        Player.instance.Look(lookDirection);
    }

    public void OnMove(Vector2 moveDirection)
    {
        Player.instance.Move(moveDirection);
    }

    public void OnRun()
    {
        Player.instance.Run();
    }

    public void OnJump()
    {
        Player.instance.Jump();
    }

    public void OnSelectWeaponNext()
    {
        Player.instance.SelectWeaponNext();
    }

    public void OnSelectWeaponPrevious()
    {
        Player.instance.SelectWeaponPrevious();
    }

    public void OnSwitchConsumableNext()
    {
        Player.instance.SwitchConsumableNext();
    }

    public void OnSwitchConsumablePrevious()
    {
        Player.instance.SwitchConsumablePrevious();
    }

    public void OnSwitchWeapon()
    {
        Player.instance.SwitchWeapon();
    }

    public void OnConsumableSkill(int skillNumer)
    {
        Player.instance.ConsumableSkill(skillNumer);
    }

    public void OnWeaponSkill(int skillNumer)
    {
        Player.instance.WeaponSkill(skillNumer);
    }

    public void OnStopWeaponSkill()
    {
        Player.instance.StopWeaponSkill(true);
    }

    public void OnReload()
    {
        Player.instance.ReloadWeapon();
    }
}