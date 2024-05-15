using System.Collections;

using UnityEngine;

using UnityEngine.UI;

public sealed class FrameRateDisplayer : MonoBehaviour
{
    [SerializeField] private bool _display = false;

    [SerializeField] private Text frameRate_Text = null;

    public bool display
    {
        get
        {
            return _display;
        }

        set
        {
            _display = value;

            if(_display == true)
            {
                StartCoroutine(_updateFrameRate);
            }

            else
            {
                StopCoroutine(_updateFrameRate);
            }

            frameRate_Text.gameObject.SetActive(_display);
        }
    }

    private void Awake()
    {
        _updateFrameRate = _UpdateFrameRate();

        display = _display;
    }

    private IEnumerator _updateFrameRate = null;

    private IEnumerator _UpdateFrameRate()
    {
        var deltaTime = 0f;

        while (true)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

            frameRate_Text.text = string.Format("{0:0.0} ms ({1:0.0} fps)", deltaTime * 1000f, 1f / deltaTime);

            yield return null;
        }
    }
}