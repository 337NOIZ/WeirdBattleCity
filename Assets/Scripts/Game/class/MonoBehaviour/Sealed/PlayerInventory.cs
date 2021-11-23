
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public sealed class PlayerInventory : MonoBehaviour
{
    [Space]

    [SerializeField] private GameObject _consumables = null;

    [SerializeField] private GameObject _weapons = null;

    public Player player { get; private set; }

    private Dictionary<ItemType, List<ItemInfo>> itemInfos;

    private Dictionary<ItemType, int> currentItemNumber;

    private Dictionary<ItemCode, Consumable> consumables = new Dictionary<ItemCode, Consumable>();

    private Dictionary<ItemCode, Weapon> weapons = new Dictionary<ItemCode, Weapon>();

    private Dictionary<ItemType, InventoryItem> currentItem = new Dictionary<ItemType, InventoryItem>();

    public void Initialize(Player player)
    {
        this.player = player;

        itemInfos = player.playerInfo.playerInventoryInfo.itemInfos;

        currentItemNumber = player.playerInfo.playerInventoryInfo.lastItemNumber;

        var consumables = _consumables.GetComponentsInChildren<Consumable>();

        int length = consumables.Length;

        for (int index = 0; index < length; ++index)
        {
            this.consumables.Add(consumables[index].itemCode, consumables[index]);

            consumables[index].Initialize();
        }

        var weapons = _weapons.GetComponentsInChildren<Weapon>();

        length = weapons.Length;

        for (int index = 0; index < length; ++index)
        {
            this.weapons.Add(weapons[index].itemCode, weapons[index]);

            weapons[index].Initialize();
        }

        int number = currentItemNumber[ItemType.consumable];

        currentItem.Add(ItemType.consumable, this.consumables[itemInfos[ItemType.consumable][number].itemCode]);

        number = currentItemNumber[ItemType.weapon];

        currentItem.Add(ItemType.weapon, this.weapons[itemInfos[ItemType.weapon][number].itemCode]);

        SelectWeapon(number);

        StartCoroutine(currentItem[ItemType.weapon].Draw());
    }

    public int Search(ItemType itemType, ItemCode itemCode)
    {
        int count = itemInfos[itemType].Count;

        for (int index = 0; index < count; ++index)
        {
            if (itemInfos[itemType][index].itemCode == itemCode)
            {
                return index;
            }
        }

        return -1;
    }

    public bool GetItem(ItemInfo itemInfo)
    {
        int index = Search(itemInfo.itemType, itemInfo.itemCode);

        if(index >= 0)
        {
            int stackCount_Max = Mathf.FloorToInt(GameMaster.instance.gameData.levelData.itemDatas[itemInfo.itemCode].stackCount_Max * itemInfo.stackCount_Max_Multiple);

            if (itemInfos[itemInfo.itemType][index].stackCount < stackCount_Max)
            {
                itemInfos[itemInfo.itemType][index].stackCount += itemInfo.stackCount;

                if (itemInfos[itemInfo.itemType][index].stackCount > stackCount_Max)
                {
                    itemInfos[itemInfo.itemType][index].stackCount = stackCount_Max;
                }

                return true;
            }

            return false;
        }

        itemInfos[itemInfo.itemType].Add(itemInfo);

        return true;
    }

    private void Consum(ItemCode itemCode, bool draw)
    {
        if (_consum == null && _switchWeapon == null && _attack == null && _reload == null)
        {
            int index = Search(ItemType.consumable, itemCode);

            if(index >= 0)
            {
                currentItem[ItemType.consumable] = consumables[itemCode];

                currentItem[ItemType.consumable].Initialize(itemInfos[ItemType.consumable][index]);

                _consum = _Consum(draw);

                StartCoroutine(_consum);
            }
        }
    }

    private IEnumerator _consum = null;

    private IEnumerator _Consum(bool draw)
    {
        if (draw == true)
        {
            currentItem[ItemType.weapon].Store();

            yield return currentItem[ItemType.consumable].Draw();
        }

        yield return currentItem[ItemType.consumable].Skill(0);

        if (draw == true)
        {
            currentItem[ItemType.consumable].Store();

            yield return currentItem[ItemType.weapon].Draw();
        }

        _consum = null;
    }

    private void StopConsum()
    {
        StartCoroutine(currentItem[ItemType.consumable].StopSkill());
    }

    public void ConsumGrenade()
    {
        Consum(ItemCode.grenade, true);
    }

    public void ConsumMedikit()
    {
        Consum(ItemCode.medikit, false);
    }

    private void SelectWeapon(int number)
    {
        currentItemNumber[ItemType.weapon] = number;

        currentItem[ItemType.weapon] = weapons[itemInfos[ItemType.weapon][number].itemCode];

        currentItem[ItemType.weapon].Initialize(itemInfos[ItemType.weapon][number]);
    }

    public void SwitchWeapon(int number)
    {
        if (_switchWeapon == null)
        {
            if (number > itemInfos[ItemType.weapon].Count - 1)
            {
                number = 0;
            }

            else if (number < 0)
            {
                number = itemInfos[ItemType.weapon].Count - 1;
            }

            if (number != currentItemNumber[ItemType.weapon])
            {
                _switchWeapon = _SwitchWeapon(number);

                StartCoroutine(_switchWeapon);
            }
        }
    }

    private IEnumerator _switchWeapon = null;

    private IEnumerator _SwitchWeapon(int number)
    {
        yield return currentItem[ItemType.weapon].Store();

        SelectWeapon(number);

        yield return currentItem[ItemType.weapon].Draw();

        _switchWeapon = null;
    }

    public void SwitchWeaponNext()
    {
        SwitchWeapon(currentItemNumber[ItemType.weapon] + 1);
    }

    public void SwitchWeaponPrevious()
    {
        SwitchWeapon(currentItemNumber[ItemType.weapon] - 1);
    }

    public void Attack(bool state)
    {
        if (state == true)
        {
            if (_consum == null && _switchWeapon == null && _attack == null && _reload == null)
            {
                _attack = _Attack();

                StartCoroutine(_attack);
            }
        }

        else
        {
            StartCoroutine(currentItem[ItemType.weapon].StopSkill(true));
        }
    }

    private IEnumerator _attack = null;

    private IEnumerator _Attack()
    {
        yield return currentItem[ItemType.weapon].Skill(0);

        _attack = null;
    }

    public void Reload()
    {
        if (_consum == null && _switchWeapon == null && _reload == null)
        {
            _reload = _Reload();

            StartCoroutine(_reload);
        }
    }

    private IEnumerator _reload = null;

    private IEnumerator _Reload()
    {
        if(_attack != null)
        {
            yield return currentItem[ItemType.weapon].StopSkill(false);
        }

        yield return currentItem[ItemType.weapon].Reload();

        _reload = null;
    }

    private void StopReload()
    {
        currentItem[ItemType.weapon].StopReload();
    }
}