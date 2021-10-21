
using System.Collections;

using UnityEngine;

using FadeScreen;

public class City : MonoBehaviour
{
    [Space]

    [SerializeField] private Player player = null;

    private EnemySpawner enemySpawner;

    private DropSpawner dropSpawner;

    private void Start()
    {
        player.Initialize();

        enemySpawner = EnemySpawner.instance;

        enemySpawner.Initialize();

        dropSpawner = DropSpawner.instance;

        dropSpawner.Initialize();

        StartCoroutine(_Start());
    }

    private IEnumerator _Start()
    {
        yield return PrimaryFadeScreen.instance.fadeScreen.Fade(2f, 0f, 1f, 2f);
    }
}