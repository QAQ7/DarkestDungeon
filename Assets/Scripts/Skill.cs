using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public string spriteName;
    public GameObject source;
    public bool isPlayingSourceAnim = false;
    public GameObject target;
    public bool isPlayingTargetAnim = false;
    public bool isPlayingAnim = false;
    public bool isSkillInited = false;
    public String sourceAnimState;
    public String targetAnimState;
    public int value;
    public bool isActived = false;
    public SkillType skillType;
    public bool isTargetTeammate;
    public enum SkillType { 
        acctack,
        buff
    }
    public void useSkill(GameObject source, GameObject target, int acctack) { 
    
    }

    public void useSkill(GameObject source, GameObject target) {
        if (source != null && target != null)
        {
            bool sign = false;
            if (source.tag == target.tag && isTargetTeammate == true)
            {
                sign = true;
            }
            if (source.tag != target.tag && isTargetTeammate == false)
            {
                sign = true;
            }
            if (sign)
            {
                if (isPlayingAnim == false && this.source == null && this.target == null)
                {
                    isPlayingAnim = true;
                }
                this.source = source;
                this.target = target;
                isActived = true;
            }
        }
    }
    public abstract void skillInit();
    public void skillClear() {
        source = null;
        target = null;
    }

    public abstract bool isAnimPlaying();

    private void Awake()
    {
        skillInit();
    }

    public void OnDestory() {
        if (this.gameObject.GetComponent<Skill_Button>() != null) {
            this.gameObject.GetComponent<Image>().sprite = null;
            this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            this.gameObject.GetComponent<Skill_Button>().skill = null;
            this.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (skillType == SkillType.acctack) {
            if (isPlayingAnim == false || source == null || target == null)
            {
                return;
            }
            else
            {
                /*if (source.GetComponent<Animator>().GetBool(sourceAnimState) == false && isPlayingSourceAnim == false)
                {
                    source.GetComponent<Animator>().SetBool(sourceAnimState, true);
                    isPlayingSourceAnim = true;
                }
                if (source.GetComponent<Animator>().GetBool(sourceAnimState) == false && isPlayingSourceAnim == true)
                {
                    if (target.GetComponent<Animator>().GetBool(targetAnimState) == false && isPlayingTargetAnim == false)
                    {
                        target.GetComponent<Animator>().SetBool(targetAnimState, true);
                        isPlayingTargetAnim = true;
                    }
                    if (target.GetComponent<Animator>().GetBool(targetAnimState) == false && isPlayingTargetAnim == true)
                    {
                        target.GetComponent<Creture>().hpChange(value);
                        isPlayingAnim = false;
                        isPlayingSourceAnim = false;
                        isPlayingTargetAnim = false;
                        source = null;
                        target = null;
                        isActived = false;
                    }
                }*/
                if (source.GetComponent<Animator>().GetBool(sourceAnimState) == false && target.GetComponent<Animator>().GetBool(targetAnimState) == false && isPlayingSourceAnim == false && isPlayingTargetAnim == false)
                {
                    source.GetComponent<Animator>().SetBool(sourceAnimState, true);
                    target.GetComponent<Animator>().SetBool(targetAnimState, true);
                    isPlayingSourceAnim = true;
                    isPlayingTargetAnim = true;
                }
                if (source.GetComponent<Animator>().GetBool(sourceAnimState) == false && target.GetComponent<Animator>().GetBool(targetAnimState) == false && isPlayingSourceAnim == true && isPlayingTargetAnim == true)
                {
                    target.GetComponent<Creture>().hpChange(value);
                    isPlayingAnim = false;
                    isPlayingSourceAnim = false;
                    isPlayingTargetAnim = false;
                    source = null;
                    target = null;
                    isActived = false;
                }
            }
        }
        if (skillType == SkillType.buff) {
            if (isPlayingAnim == false || source == null || target == null)
            {
                return;
            }
            else
            {
                if (target.GetComponent<Animator>().GetBool(targetAnimState) == false && isPlayingTargetAnim == false)
                {
                    target.GetComponent<Animator>().SetBool(targetAnimState, true);
                    isPlayingTargetAnim = true;
                }
                if (target.GetComponent<Animator>().GetBool(targetAnimState) == false && isPlayingTargetAnim == true)
                {
                    target.GetComponent<Creture>().hpChange(value);
                    isPlayingAnim = false;
                    isPlayingSourceAnim = false;
                    isPlayingTargetAnim = false;
                    source = null;
                    target = null;
                    isActived = false;
                }
            }
        }
    }
}
