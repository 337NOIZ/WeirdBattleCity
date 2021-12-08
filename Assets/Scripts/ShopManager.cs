
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

// 상점 스크립트이다.
// 수정가능.

public class ShopManager : MonoBehaviour
{
    // UI관련 오브젝트

    public GameObject ChooseShop;

    public GameObject WeaponShop;

    public GameObject ConsumableShop;

    // 수정이 필요한 부분 (상점 UI안의 Money_Text에 현재 금액이 표시되어야한다.)
    // 하위 오브젝트를 불러서 출력해야한다.

    public GameObject MoneyText;

    // 가격을 정하는 배열
    // 인스팩터 창에서 가격을 넣으면 된다.

    public int[] Price;

    // 기본적으로 Price배열의 길이를 정해놓아야한다.
    // money = 기존 화폐를 저장하는 변수를 삽입하면 된다.
    // number = 몇번째 아이템인지를 정해주는 변수이다.
    // money와 number를 받아서 Price배열의 금액과 money의 금액을 비교한다.
    // 금액이 충분하다면 아이템값을 -로 반환한다.
    // 금액이 충분하지 않다면 0을 반환한다.

    private int Buy_Item(int number)
    {
        // 수정이 필요한 부분 (물건의 가격보다 현재 가진 돈이 많아야 살수있게 만드는중)

        int money = 0;

        if (money >= Price[number])
        {
            return -Price[number];
        }

        // 임의로 순서를 정했다.
        // 순서는 바꾸어도 상관없다.
        // 0 : 샷건 총알
        // 1 : SMG 총알
        // 2 : 힐킷
        // 3 : 수류탄
        // 4 : 샷건
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

    // 상점 버튼 함수

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

    // 소비탬

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

    // 무기
    public void Item_4()
    {
        Buy_Item(3);
    }

    public void Item_5()
    {
        Buy_Item(4);
    }
}
