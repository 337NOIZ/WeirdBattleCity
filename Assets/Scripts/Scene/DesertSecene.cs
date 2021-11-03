
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using FadeScreen;

public class DesertSecene : MonoBehaviour
{
    private void Start()
    {
        Player.instance.Initialize(GameManager.instance.gameData.playerInfo);

        StartCoroutine(_Start());
    }
    private IEnumerator _Start()
    {
        yield return new WaitForSeconds(1f);

        yield return PrimaryFadeScreen.instance.fadeScreen.Fade(2f, 0f, 1f, 2f);

        yield return new WaitForSeconds(1f);

        EnemySpawner.instance.Spawn
        (
            new CharacterInfo
            (
                new TransformInfo(new Vector3(0f, 0f, 10f), new Vector3(0f, 180f, 0f)),

                new DamageableInfo(CharacterType.enemy, CharacterCode.crazySpider, 1f, 1f),

                new MovementInfo(),

                new List<SkillInfo>() { new SkillInfo() }
            )
        );
    }
}