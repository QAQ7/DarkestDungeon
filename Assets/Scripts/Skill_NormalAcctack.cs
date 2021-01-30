using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_NormalAcctack : Skill
{
    public override void skillInit()
    {
        spriteName = "a";
        skillType = SkillType.acctack;
        value = -1;
        sourceAnimState = "isChopAcctacking";
        targetAnimState = "isHited";
        isTargetTeammate = false;
    }

    public override bool isAnimPlaying() {
        return isPlayingAnim;
    }
}
