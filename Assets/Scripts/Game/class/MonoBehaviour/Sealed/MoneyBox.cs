
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

        if (_moveTowardsMoneyAmount != null)
        {
            StopCoroutine(_moveTowardsMoneyAmount);
        }

        _moveTowardsMoneyAmount = _MoveTowardsMoneyAmount(moneyAmount, movingTime);

        StartCoroutine(_moveTowardsMoneyAmount);
    }

    private IEnumerator _moveTowardsMoneyAmount = null;

    private IEnumerator _MoveTowardsMoneyAmount(float moneyAmount, float movingTime)
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

        _moveTowardsMoneyAmount = null;
    }
}