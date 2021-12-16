using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour
{
    Button _add;
    Button _sub;
    float _per;
    int _num;
    public int Num { get => _num; }
    public SkillType Type;
    SkillSettingPanel _panel;
    private void Awake()
    {
        _add = transform.Find("Add").GetComponent<Button>();
        _sub = transform.Find("Sub").GetComponent<Button>();
        var rt = transform.Find("Slider/Progress").GetComponent<RectTransform>();
        _num = 0;
        _per = transform.Find("Slider").GetComponent<RectTransform>().rect.width / 5;
        _add.onClick.AddListener(()=> {
            if (_num >= 5|| !_panel.AlertNum(-1)) return;
            _num++;
            rt.sizeDelta = new Vector2(_num * _per, rt.sizeDelta.y);
            transform.Find("Num").GetComponent<Text>().text = _num.ToString();
        });
        _sub.onClick.AddListener(() =>
        {
            if (_num <= 0|| !_panel.AlertNum(1)) return;
            _num--;
            rt.sizeDelta = new Vector2(_num * _per, rt.sizeDelta.y);
            transform.Find("Num").GetComponent<Text>().text = _num.ToString();
        });
    }
    public void Init(SkillSettingPanel panel)
    {
        _panel = panel;
    }

}
