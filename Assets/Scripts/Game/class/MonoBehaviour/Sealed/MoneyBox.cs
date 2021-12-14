
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

    public void StartMoveTowardsMoneyAmount(float moneyAmount, float movingTime)
    {
        if (_moneyAmount != moneyAmount)

        if (_MoveTowardsMoneyAmount != null)
        {
            StopCoroutine(_MoveTowardsMoneyAmount);
        }

        _MoveTowardsMoneyAmount = MoveTowardsMoneyAmount_(moneyAmount, movingTime);

        StartCoroutine(_MoveTowardsMoneyAmount);
    }

    private IEnumerator _MoveTowardsMoneyAmount = null;

    private IEnumerator MoveTowardsMoneyAmount_(float moneyAmount, float movingTime)
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

        _MoveTowardsMoneyAmount = null;
    }
}