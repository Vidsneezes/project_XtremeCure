using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriShotAttack : MonsterAttack
{
    public BadGuyProjectile prefab_projectile;
    public Transform launchPosition;
    public float coolDown;

    float cooldownTime = 0;
    bool justFired = false;


    public override bool AttackCondition()
    {
        return true;
    }

    public override bool RunAttack()
    {
        if (!justFired)
        {
            justFired = true;
            cooldownTime = Time.time;
            SingleProjectileAttack.LaunchProjectile2D(prefab_projectile, launchPosition.position + Vector3.left * 1.2f);
            SingleProjectileAttack.LaunchProjectile2D(prefab_projectile, launchPosition.position + Vector3.right * 1.2f);
            SingleProjectileAttack.LaunchProjectile2D(prefab_projectile, launchPosition.position);

            return true;
        }

        return false;
    }

    public override bool HasAttackCooled()
    {
        bool hasCooled = Time.time - cooldownTime > coolDown;
        if (hasCooled)
        {
            justFired = false;
        }

        return hasCooled;
    }

    public override float CooldownLeft()
    {
        return (Time.time - cooldownTime) / coolDown;
    }

    public static void LaunchProjectile2D(BadGuyProjectile prefab, Vector3 launchPosition)
    {
        BadGuyProjectile bgp = GameObject.Instantiate(prefab, launchPosition, Quaternion.identity);
        bgp.Launch(BadGuy.DirectionToPlayer2D(launchPosition), 10);
    }

}
