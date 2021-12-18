using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType
{
    GrowSpeed, //速度
    Drought, //干旱
    Distance, //距离
    Duration //时长
}

public class SkillSettingPanel : MonoBehaviour
{
    List<SkillItem> _skillItem;

    public int TotallNum = 8;

    Text _totallNumTxt;

    private void Awake()
    {
        _skillItem =
            new List<SkillItem>(transform.GetComponentsInChildren<SkillItem>());
        foreach (var v in _skillItem) v.Init(this);
        _totallNumTxt = transform.Find("TotallNum").GetComponent<Text>();
    }

    public bool AlertNum(int num)
    {
        TotallNum += num;
        if (TotallNum > 8 || TotallNum < 0)
        {
            TotallNum -= num;
            return false;
        }
        _totallNumTxt.text = "Skill Points Left：" + TotallNum.ToString();
        return true;
    }

    //获取技能点
    public int GetNumByType(SkillType type)
    {
        return _skillItem.Find(v => v.Type == type).Num;
    }
}
