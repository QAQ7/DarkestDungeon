using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Creture : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite image;
    public int hp;
    public int mp;
    public int speed;
    public Animator animator;
    public Text hpText;
    public Vector3 hpTextPos;
    public List<State> states = new List<State>();

    public abstract void valueInit();

    public abstract void animatorInit();

    public void roundStateAction() {
        for (int i = 0; i < states.Count; i++) {
            if (states[i].action()==false) {
                GameObject.Destroy(states[i]);
                states.RemoveAt(i);
                i--;
            }
        }
    }

    public void addState(States state,int round) { 
        switch (state){
            case States.Bleeding: {
                    State newstate = this.gameObject.AddComponent<State>();
                    newstate.init(state,round);
                    states.Add(newstate);
                    break;
                }
        }
    }

    public void imageInit() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = image;
    }

    public void hpTextInit() {
        hpText = Instantiate(hpText, this.transform.position+new Vector3(0,400,0), Quaternion.identity);
        hpText.transform.SetParent(GameManager.canvas.transform);
        hpText.text = "HP: " + hp;
    }

    public void superInit() {
        //imageInit();
        valueInit();
        animatorInit();
        hpTextInit();
    }

    public void hpChange(int i) {
        hp += i;
        hpText.text = "HP: " + hp;
        if (hp == 0) {
            for (int o = 0; o < GameManager.gameObjects.Length; o++) { 
                if (GameManager.gameObjects[o].Equals(this.gameObject)) {
                    GameObject[] newGameObjects = new GameObject[GameManager.gameObjects.Length - 1];
                    for (int k = 0; k < GameManager.gameObjects.Length; k++) {
                        if (k == o) {
                            k++;
                            if (k == GameManager.gameObjects.Length) {
                                break;
                            }
                        }
                        newGameObjects[k] = GameManager.gameObjects[k];
                    }
                    GameManager.gameObjects = newGameObjects;
                    continue;
                }
            }
            GameManager.actionList.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
    }

    public void stateBleeding() {
        hpChange(-1);    
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
