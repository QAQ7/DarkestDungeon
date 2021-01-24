using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HpAddBuff : Skill
{
    public override bool isAnimPlaying()
    {
        return isPlayingAnim;
    }

    public override void skillInit()
    {
        value = 2;
        //sourceAnimState = "isHealing";
        targetAnimState = "isHealing";
        skillType = SkillType.buff;
        isTargetTeammate = true;
    }
}
