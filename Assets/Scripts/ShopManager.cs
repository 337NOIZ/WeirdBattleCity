
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

// ���� ��ũ��Ʈ�̴�.
// ��������.

public class ShopManager : MonoBehaviour
{
    // UI���� ������Ʈ

    public GameObject ChooseShop;

    public GameObject WeaponShop;

    public GameObject ConsumableShop;

    // ������ �ʿ��� �κ� (���� UI���� Money_Text�� ���� �ݾ��� ǥ�õǾ���Ѵ�.)
    // ���� ������Ʈ�� �ҷ��� ����ؾ��Ѵ�.

    public GameObject MoneyText;

    // ������ ���ϴ� �迭
    // �ν����� â���� ������ ������ �ȴ�.

    public int[] Price;

    // �⺻������ Price�迭�� ���̸� ���س��ƾ��Ѵ�.
    // money = ���� ȭ�� �����ϴ� ������ �����ϸ� �ȴ�.
    // number = ���° ������������ �����ִ� �����̴�.
    // money�� number�� �޾Ƽ� Price�迭�� �ݾװ� money�� �ݾ��� ���Ѵ�.
    // �ݾ��� ����ϴٸ� �����۰��� -�� ��ȯ�Ѵ�.
    // �ݾ��� ������� �ʴٸ� 0�� ��ȯ�Ѵ�.

    private int Buy_Item(int number)
    {
        // ������ �ʿ��� �κ� (������ ���ݺ��� ���� ���� ���� ���ƾ� ����ְ� �������)

        int money = 0;

        if (money >= Price[number])
        {
            return -Price[number];
        }

        // ���Ƿ� ������ ���ߴ�.
        // ������ �ٲپ �������.
        // 0 : ���� �Ѿ�
        // 1 : SMG �Ѿ�
        // 2 : ��Ŷ
        // 3 : ����ź
        // 4 : ����
        // 5 : SMG

        switch (number)
        {
            case 0:

                Player.instance.GetItem(new ItemInfo(GameMaster.instance.gameData.levelData.itemDatas[ItemCode.shotgunAmmo], 30));

                break;

            case 1:

                Player.instance.GetItem(new ItemInfo(GameMaster.instance.gameData.levelData.itemDatas[ItemCode.submachineGunAmmo], 100));

                break;

            case 2:

                Player.instance.GetItem(new ItemInfo(GameMaster.instance.gameData.levelData.itemDatas[ItemCode.medikit], 1));

                break;

            case 3:

                Player.instance.GetItem(new ItemInfo(GameMaster.instance.gameData.levelData.itemDatas[ItemCode.grenade], 1));

                break;

            case 4:

                Player.instance.Item_unlock[1] = true;

                break;

            case 5:

                Player.instance.Item_unlock[2] = true;

                break;
        }

        return 0;
    }

    // ���� ��ư �Լ�

    public void Choose_Shop1()
    {
        ChooseShop.SetActive(false);

        WeaponShop.SetActive(true);

        ConsumableShop.SetActive(false);

        MoneyText.SetActive(true);
    }

    public void Choose_Shop2()
    {
        ChooseShop.SetActive(false);

        WeaponShop.SetActive(false);

        ConsumableShop.SetActive(true);

        MoneyText.SetActive(true);
    }

    public void Exit_Shop()
    {
        ChooseShop.SetActive(false);

        WeaponShop.SetActive(false);

        ConsumableShop.SetActive(false);

        MoneyText.SetActive(false);
    }

    public void Inside_Shop_Exit()
    {
        ChooseShop.SetActive(true);

        WeaponShop.SetActive(false);

        ConsumableShop.SetActive(false);

        MoneyText.SetActive(false);
    }

    // �Һ���

    public void Item_1()
    {
        Buy_Item(0);
    }

    public void Item_2()
    {
        Buy_Item(1);
    }

    public void Item_3()
    {
        Buy_Item(2);
    }

    // ����
    public void Item_4()
    {
        Buy_Item(3);
    }

    public void Item_5()
    {
        Buy_Item(4);
    }
}
