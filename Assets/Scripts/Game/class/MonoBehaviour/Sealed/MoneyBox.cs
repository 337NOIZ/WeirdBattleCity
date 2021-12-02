
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public class MoneyBox : MonoBehaviour
{
    [SerializeField] private Text _text_MoneyAmount = null;

    private float _moneyAmount;
    public float moneyAmount
    {
        set
        {
            _moneyAmount = value;

            _text_MoneyAmount.text = string.Format("{0:0}", _moneyAmount);
        }
    }

    public void SetMoneyAmountWithDirect(float moneyAmount, float movingTime)
    {
        if (_setMoneyAmountWithDirect != null)
        {
            StopCoroutine(_setMoneyAmountWithDirect);
        }

        _setMoneyAmountWithDirect = _SetMoneyAmountWithDirect(moneyAmount, movingTime);

        StartCoroutine(_setMoneyAmountWithDirect);
    }

    private IEnumerator _setMoneyAmountWithDirect = null;

    private IEnumerator _SetMoneyAmountWithDirect(float moneyAmount, float movingTime)
    {
        if (movingTime > 0f)
        {
            float maxDelta = (_moneyAmount >= moneyAmount ? _moneyAmount - moneyAmount : moneyAmount - _moneyAmount) / movingTime;

            while (_moneyAmount != moneyAmount)
            {
                this.moneyAmount = Mathf.MoveTowards(_moneyAmount, moneyAmount, maxDelta * Time.deltaTime);

                yield return null;
            }
        }

        _setMoneyAmountWithDirect = null;
    }
}