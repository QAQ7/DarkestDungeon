using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Button : MonoBehaviour
{
    public Sprite icon;
    public Skill skill;
    public int disX = 50;
    public int disY = 50;
    // Start is called before the first frame update

    private void Awake()
    {
        //this.gameObject.GetComponent<Image>().sprite = icon;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick() {
        if (skill!=null) { 
            GameManager.Skill_Cur = this.skill;
            Debug.Log(GameManager.Skill_Cur.name);
        }
    }
}
