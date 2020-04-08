using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAttack : MonoBehaviour
{
    public abstract bool AttackCondition();
    public abstract bool RunAttack();
    public abstract bool HasAttackCooled();
    public abstract float CooldownLeft();
}
