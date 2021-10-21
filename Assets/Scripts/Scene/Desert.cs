
using System.Collections;

using UnityEngine;

using FadeScreen;

public class Desert : MonoBehaviour
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

        yield return new WaitForSeconds(1f);

        dropSpawner.Spawn(new ItemInfo(ItemType.consumable, ItemCode.medikit, 999), new Vector3(2f, 0f, 10f));

        while (true)
        {
            enemySpawner.Spawn(new EnemyData(EnemyCode.dummy, new Vector3(0f, 0f, 10f), new Vector3(0f, 0f, 0f), new DamageableData(10, 10, 0f, 10), 1));

            while (enemySpawner.enemyCount > 0) yield return null;

            yield return new WaitForSeconds(2f);
        }
    }
}