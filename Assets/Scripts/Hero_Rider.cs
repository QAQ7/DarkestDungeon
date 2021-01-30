using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Rider : Hero
{
    // Start is called before the first frame update
    public override void valueInit()
    {
        this.skillName = new string[1] { "Skill_NormalAcctack" };
        this.hp = 5;
        this.mp = 5;
        this.speed = 5;
        this.addState(States.Bleeding,3);
    }
}
