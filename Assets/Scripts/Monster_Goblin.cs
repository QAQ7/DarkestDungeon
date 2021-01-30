using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Goblin : Monster
{

    public override Skill getHitSkill()
    {
        return skill[0];
    }

    public override GameObject getHitTarget()
    {
        GameObject[] heros = GameManager.heros;
        int minHp = heros[0].GetComponent<Hero>().hp;
        GameObject target = heros[0];
        for (int i = 0; i < heros.Length; i++) {
            if (minHp > heros[i].GetComponent<Hero>().hp) {
                minHp = heros[i].GetComponent<Hero>().hp;
                target = heros[i];
            }
        }
        return target;
    }

    // Start is called before the first frame update
    public override void valueInit()
    {
        this.hp = 5;
        this.mp = 5;
        this.speed = 4;
    }
}
