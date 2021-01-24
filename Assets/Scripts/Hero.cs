using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : Creture
{
    public List<GameObject> skill_button;

    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    public void skillButtonInit()
    {
        for (int i = 0; i < skill_button.Count; i++)
        {
            skill_button[i] = Instantiate(skill_button[i], new Vector3(this.gameObject.transform.position.x+i*50, this.gameObject.transform.position.y - 50 ,0), Quaternion.identity);
            skill_button[i].SetActive(false);
            skill_button[i].transform.SetParent(GameManager.canvas.transform);
        }
    }

    public void setSkillButtonActive(bool a)
    {
        for (int i = 0; i < skill_button.Count; i++)
        {
            skill_button[i].SetActive(a);
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
