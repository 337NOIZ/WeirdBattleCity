using System.Collections.Generic;

using UnityEngine;

public sealed class ShopManager : MonoBehaviour
{
    [SerializeField] private MoneyBox _mobeyBox = null;

    private Dictionary<ItemCode, ItemData> _itemDatas;

    public void Awaken()
    {
        _itemDatas = GameManager.instance.gameData.levelData.itemDatas;
    }

    public void BuyItem(int number)
    {
        switch (number)
        {
            case 0:

                if (Player.instance.characterInfo.moneyAmount > 10f)
                {
                    Player.instance.GetMoney(-10f);

                    Player.instance.GetItem(new ItemInfo(_itemDatas[ItemCode.Grenade], 1));

                    _mobeyBox.StartMoveTowardsMoneyAmount(Player.instance.characterInfo.moneyAmount, 1f);
                }

                break;

            case 1:

                if (Player.instance.characterInfo.moneyAmount > 10f)
                {
                    Player.instance.GetMoney(-10f);

                    Player.instance.GetItem(new ItemInfo(_itemDatas[ItemCode.Medikit], 1));

                    _mobeyBox.StartMoveTowardsMoneyAmount(Player.instance.characterInfo.moneyAmount, 1f);
                }
                
                break;

            case 2:

                if (Player.instance.characterInfo.moneyAmount > 10f)
                {
                    Player.instance.GetMoney(-10f);

                    Player.instance.GetItem(new ItemInfo(_itemDatas[ItemCode.Shotgun], 1));

                    _mobeyBox.StartMoveTowardsMoneyAmount(Player.instance.characterInfo.moneyAmount, 1f);
                }

                break;

            case 3:

                if (Player.instance.characterInfo.moneyAmount > 10f)
                {
                    Player.instance.GetMoney(-10f);

                    Player.instance.GetItem(new ItemInfo(_itemDatas[ItemCode.SubmachineGun], 1));

                    _mobeyBox.StartMoveTowardsMoneyAmount(Player.instance.characterInfo.moneyAmount, 1f);
                }

                break;

            case 4:

                if (Player.instance.characterInfo.moneyAmount > 10f)
                {
                    Player.instance.GetMoney(-10f);

                    Player.instance.GetItem(new ItemInfo(_itemDatas[ItemCode.ShotgunAmmo], 3));

                    _mobeyBox.StartMoveTowardsMoneyAmount(Player.instance.characterInfo.moneyAmount, 1f);
                }

                break;

            case 5:

                if (Player.instance.characterInfo.moneyAmount > 10f)
                {
                    Player.instance.GetMoney(-10f);

                    Player.instance.GetItem(new ItemInfo(_itemDatas[ItemCode.SubmachineGunAmmo], 30));

                    _mobeyBox.StartMoveTowardsMoneyAmount(Player.instance.characterInfo.moneyAmount, 1f);
                }

                break;

            default :

                break;
        }
    }
}