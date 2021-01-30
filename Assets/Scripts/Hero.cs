using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Hero : Creture
{
    public string[] skillName;

    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    public void skillButtonInit()
    {
        //for (int i = 0; i < skillName.Length; i++)
        //{
        //    Type type = Type.GetType(skillName[i]);
        //    GameManager.skillButtons[i].AddComponent(type);
            //GameManager.skillButtons[i] = Instantiate(GameManager.skillButtons[i], new Vector3(this.gameObject.transform.position.x+i*50, this.gameObject.transform.position.y - 50 ,0), Quaternion.identity);
            //GameManager.skillButtons[i].SetActive(false);
            //GameManager.skillButtons[i].transform.SetParent(GameManager.canvas.transform);
        //}
    }

    public void setSkillButtonActive(bool a)
    {
        for (int i = 0; i < skillName.Length; i++)
        {
            GameManager.skillButtons[i].SetActive(a);
        }
        if (a) {
            for (int i = 0; i < skillName.Length; i++)
            {
                Type type = Type.GetType(skillName[i]);
                Skill skill = (Skill)GameManager.skillButtons[i].AddComponent(type);
                GameManager.skillButtons[i].GetComponent<Image>().sprite = GameManager.sprites[skillName[i]];
                GameManager.skillButtons[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                GameManager.skillButtons[i].GetComponent<Skill_Button>().skill = skill;
            }
        }
    }

    void Start()
    {
        superInit();
        skillButtonInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
