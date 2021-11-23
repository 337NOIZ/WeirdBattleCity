
using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public sealed class Notification : MonoBehaviour
{
    public static Notification instance { get; private set; } = null;

    private CanvasGroupController canvasGroupController;

    private Text text;

    private void Awake()
    {
        instance = this;

        canvasGroupController = GetComponent<CanvasGroupController>();

        text = GetComponent<Text>();
    }

    private IEnumerator _notify = null;

    private IEnumerator _Notify(string notificationString)
    {
        text.text = notificationString;

        canvasGroupController.canvasGroup.alpha = 0f;

        yield return canvasGroupController.FadeAlpha(0f, 1f, 1f);

        yield return new WaitForSeconds(3f);

        yield return canvasGroupController.FadeAlpha(1f, 0f, 1f);

        text.text = "";
    }

    public void Notify(string notificationString)
    {
        if(_notify != null)
        {
            StopCoroutine(_notify);
        }

        _notify = _Notify(notificationString);

        StartCoroutine(_notify);
    }
}