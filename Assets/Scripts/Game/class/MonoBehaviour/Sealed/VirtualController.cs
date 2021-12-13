
using UnityEngine;

public sealed class VirtualController : MonoBehaviour
{
    public static VirtualController instance { get; private set; } = null;

    public bool interactable = true;

    private void Awake()
    {
        instance = this;
    }

    public void Look(Vector2 lookDirection)
    {
        if (interactable == true)
        {
            Player.instance.Look(lookDirection);
        }
    }

    public void Move(Vector2 moveDirection)
    {
        if (interactable == true)
        {
            Player.instance.Move(moveDirection);
        }
    }

    public void Run()
    {
        if (interactable == true)
        {
            Player.instance.Run();
        }
    }

    public void Jump()
    {
        if (interactable == true)
        {
            Player.instance.Jump();
        }
    }

    public void StartSwitchingWeaponNext()
    {
        if (interactable == true)
        {
            Player.instance.StartSwitchingItemNext(ItemType.Weapon);
        }
    }

    public void StartSwitchingWeaponPrevious()
    {
        if (interactable == true)
        {
            Player.instance.StartSwitchingItemPrevious(ItemType.Weapon);
        }
    }

    public void StartGrenadeSkill()
    {
        if (interactable == true)
        {
            Player.instance.StartGrenadeSkill();
        }
    }

    public void StartMedikitSkill()
    {
        if (interactable == true)
        {
            Player.instance.StartMedikitSkill();
        }
    }

    public void StartWeaponSkill(int skillNumer)
    {
        if (interactable == true)
        {
            Player.instance.StartWeaponSkill(skillNumer);
        }
    }

    public void StopWeaponSkill()
    {
        if (interactable == true)
        {
            Player.instance.StopWeaponSkill();
        }
    }

    public void StartReloadWeapon()
    {
        if (interactable == true)
        {
            Player.instance.StartReloadWeapon();
        }
    }
}