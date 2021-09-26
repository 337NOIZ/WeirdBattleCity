
using System.Collections.Generic;

using UnityEngine;

public class FadeScreenManager : MonoBehaviour
{
    public static FadeScreenManager instance { get; private set; }

    [Space]

    [SerializeField] private List<Texture> _fadePatterns = null;

    public Dictionary<string, Texture> fadePatterns { get; private set; } = new Dictionary<string, Texture>();

    private void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);

            instance = this;

            int count = _fadePatterns.Count;

            for (int index = 0; index < count; ++index)
            {
                fadePatterns.Add(_fadePatterns[index].name, _fadePatterns[index]);
            }
        }
    }
}