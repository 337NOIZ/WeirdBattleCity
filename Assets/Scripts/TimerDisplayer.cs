
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public sealed class TimerDisplayer : Timer
{
    [Space]

    [SerializeField] private Text _text_Explain = null;

    [SerializeField] private Text _text_Timer = null;

    public Text text_Explain { get; private set; }

    public Text text_Timer { get; private set; }

    private void Awake()
    {
        text_Explain = _text_Explain;

        text_Timer = _text_Timer;
    }

    public void SetTimer(string explain, float time)
    {
        if(_setTimer != null)
        {
            StopCoroutine(_setTimer);
        }

        _setTimer = _SetTimer(explain, time);

        StartCoroutine(_setTimer);
    }

    private IEnumerator _setTimer = null;

    private IEnumerator _SetTimer(string explain, float time)
    {
        text_Explain.text = explain;

        IEnumerator setTimer = SetTimer(time);

        while (setTimer.MoveNext())
        {
            text_Timer.text = string.Format("{0:0}", timer);

            yield return null;
        }

        _setTimer = null;
    }
}