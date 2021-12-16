using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HindPanel : MonoBehaviour
{
    public Button hind;

    public GameObject Panel;

    public bool Hinden;

    public Sprite

            iconUp,
            iconDown;

    public float

            posYup = 191f,
            posYdown = -258.3f;

    // Start is called before the first frame update
    void Start()
    {
        hind.onClick.AddListener (OnClick);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnClick()
    {
        if (Hinden)
        {
            //raise and change icon to down
            Panel.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(0, posYdown);
            hind.image.sprite = iconUp;

            Hinden = false;
            Debug.Log("No hind");
        }
        else if (!Hinden)
        {
            //down and change to up
            Panel.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(0, posYup);
            hind.image.sprite = iconDown;

            Hinden = true;
            Debug.Log("hinding");
        }
    }
}
