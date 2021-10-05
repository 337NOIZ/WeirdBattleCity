
using System.Collections;

using UnityEngine;

using FadeScreen;

public class Desert : MonoBehaviour
{
    [Space]

    [SerializeField] private Player player = null;

    private EnemySpawner enemySpawner;

    private DropSpawner dropSpawner;

    private void Awake()
    {
        
    }

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

        dropSpawner.Spawn(new ItemData(ItemType.CONSUMABLE, ItemCode.MEDIKIT, true, 1, 999, 10f, 0f), new Vector3(2f, 0f, 10f));

        while (true)
        {
            enemySpawner.Spawn(new EnemyData(EnemyCode.DUMMY, new Vector3(0f, 0f, 10f), new Vector3(0f, 0f, 0f), new DamageableData(10, 10, 0f), 1));

            while (enemySpawner.enemyCount > 0) yield return null;

            yield return new WaitForSeconds(2f);
        }
    }
}