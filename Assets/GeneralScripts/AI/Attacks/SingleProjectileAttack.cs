using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleProjectileAttack : MonsterAttack
{
    public BadGuyProjectile prefab_projectile;
    public Transform launchPosition;
    public float coolDown;
    public bool heightBased;

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
            SingleProjectileAttack.LaunchProjectile2D(prefab_projectile, launchPosition.position,heightBased);
            return true;
        }

        return false;
    }

    public override bool HasAttackCooled()
    {
        bool hasCooled = Time.time - cooldownTime > coolDown;
        if(hasCooled)
        {
            justFired = false;
        }

        return hasCooled;
    }

    public override float CooldownLeft()
    {
        return (Time.time - cooldownTime) / coolDown;
    }

    public static void LaunchProjectile2D(BadGuyProjectile prefab, Vector3 launchPosition, bool heightBased = false)
    {
        BadGuyProjectile bgp = GameObject.Instantiate(prefab, launchPosition, Quaternion.identity);
        if (!heightBased)
        {
            bgp.Launch(BadGuy.DirectionToPlayer2D(launchPosition), 10);
        }
        else
        {
            bgp.Launch(BadGuy.DirectionToPlayer(launchPosition), 10);
        }
    }

}
