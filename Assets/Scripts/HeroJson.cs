using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

[Serializable]
public class HeroJson
{
	public int hp;
	public int mp;
	public int speed;
	public string type;
	//leper
	public List<SkillList> skillList;
}


[Serializable]
public class HerosJson {
	public List<HeroJson> heros;
}

[Serializable]
public class SkillList {
	public string name;

	public static string[] toArray(List<SkillList> e) {
		string[] strs = new string[e.Count];
		for (int i = 0; i < strs.Length; i++) {
			strs[i] = e[i].name;
		}
		return strs;
	}
}
