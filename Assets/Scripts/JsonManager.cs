using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonManager{
    public static T loadFromResource<T>(string path){
        TextAsset text =  Resources.Load<TextAsset>(@path);
        string json = text.text;
        Debug.Log(text);
        T t = JsonUtility.FromJson<T>(@json);
        return t;
    }

    public static T loadFromFile<T>(string path)
    {
        path = Environment.CurrentDirectory + @"/" + path;
        string json = File.ReadAllText(@path);
        //Debug.Log(@json);
        T t = JsonUtility.FromJson<T>(@json);
        return t;
    }
}
