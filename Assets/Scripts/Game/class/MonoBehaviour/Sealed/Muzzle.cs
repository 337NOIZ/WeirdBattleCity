
using UnityEngine;

public sealed class Muzzle : MonoBehaviour
{
    private Transform _aim;

    public void Awaken(Transform aim)
    {
        _aim = aim;
    }

    public void LaunchProjectile(Character attacker, SkillInfo.RangedInfo rangedInfo)
    {
        transform.LookAt(_aim);

        Projectile projectile;

        int index_Max = Mathf.FloorToInt(rangedInfo.division);

        for (int index = 0; index < index_Max; ++index)
        {
            projectile = ObjectPool.instance.Pop(rangedInfo.projectileCode);

            projectile.transform.position = transform.position;

            projectile.transform.rotation = transform.rotation;

            if (rangedInfo.diffusion > 0f)
            {
                projectile.transform.rotation *= Quaternion.Euler(Random.insideUnitSphere * rangedInfo.diffusion);
            }

            projectile.Launch(attacker, rangedInfo.projectileInfo);
        }
    }
}