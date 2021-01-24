using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : Creture {

    public List<Skill> skill;

    public abstract GameObject getHitTarget();
    public abstract Skill getHitSkill();
    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    void Start()
    {
        superInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
