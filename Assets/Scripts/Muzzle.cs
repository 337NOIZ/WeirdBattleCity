using UnityEngine;

using UnityEngine.Events;

public sealed class Muzzle : MonoBehaviour
{
    private Transform _aim;

    public void Awaken(Transform aim)
    {
        _aim = aim;
    }

    public void LaunchProjectile(Character attacker, UnityAction<HitBox> actionOnHit, SkillInfo.RangedInfo rangedInfo)
    {
        transform.LookAt(_aim);

        int index_Max = Mathf.FloorToInt(rangedInfo.division);

        for (int index = 0; index < index_Max; ++index)
        {
            var projectile = ObjectPool.instance.Pop(rangedInfo.projectileCode);

            projectile.transform.position = transform.position;

            projectile.transform.rotation = transform.rotation;

            if (rangedInfo.diffusion > 0f)
            {
                projectile.transform.rotation *= Quaternion.Euler(Random.insideUnitSphere * rangedInfo.diffusion);
            }

            projectile.gameObject.SetActive(true);

            projectile.Launch(attacker, actionOnHit, rangedInfo.projectileInfo);
        }
    }
}