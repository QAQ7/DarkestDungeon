using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStop : MonoBehaviour
{
    // Start is called before the first frame update
    void stopAnim(string name) {
        this.gameObject.GetComponent<Animator>().SetBool(name,false);
        Debug.Log(this.gameObject.name + " "+ name + " stop");
    }
}
