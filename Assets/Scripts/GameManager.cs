using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Skill Skill_Cur;
    public static GameObject canvas;
    public GameObject source;
    public GameObject target;
    public static GameObject[] gameObjects;
    public static GameObject[] heros;
    public static GameObject[] monsters;
    public static GameObject[] skillButtons;
    public static List<GameObject> actionList;
    public int counter = 0;
    public GameObject controlRole;
    public GameState gameState;
    public bool isOnlyDoOneFuncUndone = true;
    bool anyHeroLive = true;
    bool anyMonsterLive = true;
    public static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

    public enum GameState {
        StageBegin,
        RoundBegin,
        Battle,
        RoundEnd,
        Grap,
        StageOver,
    }

    // Start is called before the first frame update
    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        //heros = GameObject.FindGameObjectsWithTag("Hero");
        heros = GetHeros();
        //HerosJson herosjson = JsonManager.loadFromResource<HerosJson>("hero");
        //Debug.Log(herosjson.heros[0].hp);

        monsters = GameObject.FindGameObjectsWithTag("Monster");
        object[] objSkillButtons = GameObject.FindObjectsOfType<Skill_Button>();
        skillButtons = new GameObject[objSkillButtons.Length];
        for (int i = 0; i < skillButtons.Length; i++)
        {
            skillButtons[i] = (objSkillButtons[i] as Skill_Button).gameObject;
        }
        Array.Sort(skillButtons, (a, b) => a.name.CompareTo(b.name));
        object[] objContainCreture = GameObject.FindObjectsOfType<Creture>();
        gameObjects = new GameObject[objContainCreture.Length];
        for (int i = 0; i < gameObjects.Length; i++) {
            gameObjects[i] = (objContainCreture[i] as Creture).gameObject;
        }
        actionList = new List<GameObject>(gameObjects);
        actionListSort();
        TextAsset t = Resources.Load<TextAsset>("skill.sprite.path");
        string json = t.text;
        SkillJson skillJson = JsonUtility.FromJson<SkillJson>(json);
        for (int i = 0; i < skillJson.SkillList.Count; i++) {
            sprites.Add(skillJson.SkillList[i].name, Resources.Load<Sprite>(skillJson.SkillList[i].path));
        }
    }
    void Start()
    {
        gameState = GameState.RoundBegin;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState) {
            case GameState.RoundBegin:
                {
                    roundBegin();
                    break;
            }
            case GameState.Battle: 
                {
                    battle();
                    break;   
             }
            case GameState.RoundEnd:
                {
                    roundEnd();
                    break;
             }
            case GameState.StageOver:
                {
                    stageOver();
                    break;
            }
        }
    }

    public GameObject[] GetHeros() {
        HerosJson herosjson = JsonManager.loadFromFile<HerosJson>("save/save.json");
        GameObject[] newheros = new GameObject[herosjson.heros.Count];
        GameObject cur;
        //Debug.Log(herosjson.ToString());
        for (int i = 0; i < herosjson.heros.Count; i++) {
            cur = Resources.Load<GameObject>(@"Prefabs/" + herosjson.heros[i].type);
            //Debug.Log(herosjson.heros[i].type);
            cur.GetComponent<Hero>().setData(herosjson.heros[i]);
            newheros[i] = Instantiate(cur);
        }
        return newheros;
    }

    public void nextState() {
        switch (gameState) {
            case GameState.RoundBegin: {
                    gameState = GameState.Battle;
                    break;
                }
            case GameState.Battle: {
                    gameState = GameState.RoundEnd;
                    break;
                }
            case GameState.RoundEnd: {
                    gameState = GameState.RoundBegin;
                    break;
                }
        }
        isOnlyDoOneFuncUndone = true;
    }

    public void nextState(bool change)
    {
        switch (gameState)
        {
            case GameState.RoundBegin:
                {
                    gameState = GameState.Battle;
                    break;
                }
            case GameState.Battle:
                {
                    gameState = GameState.RoundEnd;
                    break;
                }
            case GameState.RoundEnd:
                {
                    if (change == true)
                    {
                        gameState = GameState.StageOver;
                        break;
                    }
                    else {
                        gameState = GameState.RoundBegin;
                        break;
                    }
                }
        }
        isOnlyDoOneFuncUndone = true;
    }


    private bool roundBegin() {
        if (isOnlyDoOneFuncUndone)
        {
            controlRole = getControlRole();
            Debug.Log(controlRole.name);
            controlRole.GetComponent<Creture>().roundStateAction();
            isOnlyDoOneFuncUndone = false;
        }
        nextState();
        return true;
    }

    private bool battle()
    {
        if (controlRole.tag == "Hero")
        {
            source = controlRole;
            source.GetComponent<Hero>().setSkillButtonActive(true);
            if (Input.GetMouseButtonDown(0) && target == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log("click");
                    if (source != null)
                    {
                        if (Skill_Cur != null && Skill_Cur.isActived == false)
                        {
                            Debug.Log("skill_cur: " + Skill_Cur.name + " ;actived: " + Skill_Cur.isActived);
                            if ((Skill_Cur.skillType == Skill.SkillType.acctack && hit.collider.gameObject.tag == "Monster")
                                ||(Skill_Cur.skillType == Skill.SkillType.buff && hit.collider.gameObject.tag == "Hero")) {
                                Debug.Log("continue");
                                target = hit.collider.gameObject;
                                Skill_Cur.useSkill(source, target);
                            }
                            //if (Skill_Cur.isActived == false)//不知道这里干什么的
                            //{
                            //    target = null;
                            //}
                        }
                    }
                }
            }
            if (Skill_Cur != null && Skill_Cur.isAnimPlaying() == false && target != null)
            {
                Debug.Log(source.name + ":" + source.GetComponent<Creture>().hp + "    " + target.name + ":" + target.GetComponent<Creture>().hp);
                nextState();
                return true;
            }
        }
        if (controlRole.tag == "Monster")
        {
            source = controlRole;
            if (target == null)
            {
                Skill_Cur = source.GetComponent<Monster>().getHitSkill();
                target = source.GetComponent<Monster>().getHitTarget();
                Skill_Cur.useSkill(source, target);
            }
            if (Skill_Cur != null && Skill_Cur.isAnimPlaying() == false && target != null)
            {
                Debug.Log(source.name + ":" + source.GetComponent<Creture>().hp + "    " + target.name + ":" + target.GetComponent<Creture>().hp);
                nextState();
                return true;
            }
        }
        return true;
    }

    private bool roundEnd()
    {
        Skill_Cur = null;
        target = null;
        if (source.tag == "Hero") {
            source.GetComponent<Hero>().setSkillButtonActive(false);
        } 
        source = null;
        controlRole = null;
        for (int i = 0; i < skillButtons.Length; i++) {
            if (skillButtons[i].GetComponent<Skill>() != null) {
                Destroy(skillButtons[i].GetComponent<Skill>());
            }
        }
        counter++;
        for (int i = 0; i < gameObjects.Length; i++) {
            if (gameObjects[i].tag == "Hero")
            {
                anyHeroLive = true;
            }
            if (gameObjects[i].tag == "Monster")
            {
                anyMonsterLive = true;
            }
        }
        if ((anyHeroLive==true&&anyMonsterLive==false)
            ||(anyHeroLive==false&&anyMonsterLive==true)){
            nextState(true);
        }
        else {
            nextState();
        }
        return true;
        //Debug.Log(counter);
    }

    public void stageOver() {
        if (anyHeroLive)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("uWin");
        }
        else {
            UnityEngine.SceneManagement.SceneManager.LoadScene("uLose");
        }
    }
    public GameObject getControlRole() {
        counter = counter % actionList.Count;
        Debug.Log("counter = "+ counter);
        return actionList[counter];
        /*
         * 已弃用
        if (counter % 2 == 0)
        {
            //Debug.Log(counter+"：hero-red");
            //return GameObject.Find("hero-red");
            Debug.Log(GameObject.Find("leper.sprite.idle.skel (default)").name);
            return GameObject.Find("leper.sprite.idle.skel (default)");
        }
        else{
            //Debug.Log(counter+": monster-blue");
            return GameObject.Find("monster-blue");
        }
        */
    }

    public void actionListSort() {
        actionList.Sort((a,b) =>{
            if (a.GetComponent<Creture>().speed > b.GetComponent<Creture>().speed)
            {
                return -1;
            }
            else {
                if (a.GetComponent<Creture>().speed == b.GetComponent<Creture>().speed) {
                    if (a.tag == "Hero")
                    {
                        return -1;
                    }
                    else {
                        return 1;
                    }
                }
                else {
                    return 1;
                }
            }
        });
        string str = "";
        for (int i = 0; i < actionList.Count; i++) {
            str += actionList[i].name + " ;";
        }
        Debug.Log(str);
    }

    /*
    暂时用不上
    public int[] SpeedOrder(List<GameObject> e) {
        int[] speeds = new int[e.Count];
        for (int i = 0; i < speeds.Length; i++) {
            speeds[i] = e[i].GetComponent<Creture>().speed;
        }
        for (int i = 1; i < speeds.Length; i++) {
            if (speeds[i] == speeds[i - 1]) {
                for (int j = i; j < speeds.Length; j++) {
                    speeds[j]++;
                }
            }
        }
        int speedLcm = lcm(speeds);
        int[] action = new int[speedLcm];
        actionListSort(actionList);
        for (int i = 0; i < actionList.Count; i++) {
            int j = speedLcm;
            int c = 0;
            while (j > 0) { 
                c += 
            }
        }
        return action;
    }

    public static int lcm(int[] e) {
        int a = e[0];
        int b = e[1];
        int tmp = 0;
        while (b > 0) {
            tmp = a;
            a = b;
            b = tmp % b;
        }
        for (int i = 2; i < e.Length; i++) {
            b = e[i];
            while (b > 0)
            {
                tmp = a;
                a = b;
                b = tmp % b;
            }
        }
        return 0;
    }*/
}
