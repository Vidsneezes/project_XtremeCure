using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeAttack : MonsterAttack
{
    public enum CloseRangeStates
    {
        Idle,
        HitScanning,
        LocalCoolDown
    }

    public float minDistance;
    public float attackDistance;
    public float damage;
    public Transform launchPosition;
    public CloseRangeStates closeRangeState;

    public float coolDown;

    float scanTime = 0;
    float cooldownTime = 0;
    bool hitPlayer = false;

    float localCooldown = 0;


    public override bool AttackCondition()
    {
        float distanceToPlayer = AIBlackboard.DistanceToPlayer2D(transform.position);
        if (distanceToPlayer < minDistance)
        {
            if (AIBlackboard.PlayerLookingAtMe(transform.position))
            {
                scanTime = Time.time;
                closeRangeState = CloseRangeStates.HitScanning;
                return true;
            }
        }

        return false;
    }

    public override bool RunAttack()
    {
        switch(closeRangeState)
        {
            case CloseRangeStates.Idle:
                break;
            case CloseRangeStates.HitScanning:

                if (Time.time - scanTime < 0.2f)
                {
                    RaycastHit rayHit;

                    Vector3 directionTowardsPlayer = AIBlackboard.playerPosition3D - launchPosition.transform.position;


                    if (Physics.Raycast(launchPosition.position, directionTowardsPlayer.normalized, out rayHit, attackDistance, 1 << 9))
                    {
                        Debug.Log("hit player");
                        FpsControllerCore.instance.HitParticle(rayHit.point + rayHit.normal * 0.05f, rayHit.normal);
                        FpsControllerCore.instance.TakeDamage((int)damage);
                        localCooldown = Time.time;
                        closeRangeState = CloseRangeStates.LocalCoolDown;
                    }
                }
                else
                {
                    localCooldown = Time.time;
                    closeRangeState = CloseRangeStates.LocalCoolDown;
                }
                break;
            case CloseRangeStates.LocalCoolDown:
                if (Time.time - localCooldown > 0.8f)
                {
                    closeRangeState = CloseRangeStates.Idle;
                    cooldownTime = Time.deltaTime;
                    return true;
                }
                break;
        }

        return false;
    }

    public override bool HasAttackCooled()
    {
        bool hasCooled = Time.time - cooldownTime > coolDown;
        return hasCooled;
    }

    public override float CooldownLeft()
    {
        return (Time.time - cooldownTime) / coolDown;
    }
}