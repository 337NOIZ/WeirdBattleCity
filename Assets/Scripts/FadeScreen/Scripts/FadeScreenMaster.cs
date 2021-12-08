
using System.Collections.Generic;

using UnityEngine;

public class FadeScreenMaster : MonoBehaviour
{
    public static FadeScreenMaster instance { get; private set; } = null;

    [Space]

    [SerializeField] private List<Texture> _fadePatterns = null;

    public Dictionary<string, Texture> fadePatterns { get; private set; } = new Dictionary<string, Texture>();

    private void Awake()
    {
        if (instance != null)
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