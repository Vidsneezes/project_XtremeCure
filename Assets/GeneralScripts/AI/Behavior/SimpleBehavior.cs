using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleBehavior : MonsterBehavior
{
    public enum SimpleStates
    {
        Idle,
        BecomingAware,
        AwareOfPlayer,
        Attack
    }

    public AudioSource monsterAware;
    public SimpleStates simpleState;
    public float distanceFromPlayer;
    public float tooCloseToPlayer;
    [Range(0f,1f)]
    public float fear;
    [Range(0f,1f)]
    public float aggressiveness;
    public NavMeshAgent navMeshAgent;

    public MonsterAttack monsterAttack;

    public float maxSpeed;
    public float playerSeparation;
    float awareDelayTimer = 0;
    float fuzzyDelay = 0;

    public override void BeginBehavior()
    {
        simpleState = SimpleStates.Idle;
        isAware = false;
    }

    public override void RunBehavior()
    {
        switch(simpleState)
        {
            case SimpleStates.Idle:
                break;
            case SimpleStates.BecomingAware:
                if(Time.time - awareDelayTimer > fuzzyDelay)
                {
                    simpleState = SimpleStates.AwareOfPlayer;
                }
                break;
            case SimpleStates.AwareOfPlayer:

                Vector3 fVelocity = navMeshAgent.velocity;

                float d_p = AIBlackboard.DistanceToPlayer2D(transform.position);

                if (AIBlackboard.DistanceToPlayer2D(transform.position) > distanceFromPlayer)
                {
                    fVelocity = Vector3.MoveTowards(fVelocity, AIBlackboard.DirectionToPlayer2D(transform.position), Time.deltaTime * 100 * aggressiveness);
                }

                if (d_p < tooCloseToPlayer)
                {
                    fVelocity += -AIBlackboard.DirectionToPlayer2D(transform.position).normalized * playerSeparation;
                }

                if (monsterAttack != null)
                {
                    if(monsterAttack.AttackCondition() && monsterAttack.HasAttackCooled())
                    {
                        simpleState = SimpleStates.Attack;
                        monsterAware.PlayOneShot(monsterAware.clip, 0.3f);

                    }
                    else if(monsterAttack.CooldownLeft() < fear)
                    {
                        fVelocity = Vector3.MoveTowards(fVelocity, -AIBlackboard.DirectionToPlayer2D(transform.position), Time.deltaTime * 40 * fear);
                    }
                }

                fVelocity.x = Mathf.Clamp(fVelocity.x, -maxSpeed, maxSpeed);
                fVelocity.y = Mathf.Clamp(fVelocity.y, -maxSpeed, maxSpeed);
                fVelocity.z = Mathf.Clamp(fVelocity.z, -maxSpeed, maxSpeed);

                navMeshAgent.velocity = fVelocity;

                break;
            case SimpleStates.Attack:
                if(monsterAttack != null)
                {
                    if(monsterAttack.RunAttack())
                    {
                        simpleState = SimpleStates.AwareOfPlayer;
                    }
                }
                break;
        }
    }

    public override void BecomeAware()
    {
        RecieveSensor(AIBlackboard.NEAR_PLAYER);
    }

    public override void RecieveSensor(int sensorTag)
    {
        if (simpleState == SimpleStates.Idle)
        {
            if (sensorTag == AIBlackboard.NEAR_PLAYER)
            {
                fuzzyDelay = Random.Range(0f, 1.03f);
                awareDelayTimer = Time.time;
                simpleState = SimpleStates.BecomingAware;
                isAware = true;
                monsterAware.PlayOneShot(monsterAware.clip);
            }
        }
    }
}
