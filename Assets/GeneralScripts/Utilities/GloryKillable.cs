using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloryKillable : MonoBehaviour
{
    public Monster monster;

    public bool CanBeGloryKilled()
    {


        return monster.healthyState == Monster.HealthyState.Staggered;
    }

    public void GloryKillIt()
    {
        monster.GloryKill();
    }
}
