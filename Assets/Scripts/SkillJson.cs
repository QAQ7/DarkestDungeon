using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

[Serializable]
public class SkillJson{
    public List<SkillListGroup> SkillList;
}

[Serializable]
public class SkillListGroup
{
    public string name;
    public string path;
}
