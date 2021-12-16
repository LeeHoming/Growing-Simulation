using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumDisplay : MonoBehaviour
{
    public int num = 100;

    public int canLive = 200;

    public int round = 0;

    public double percent = 0;

    public GameOfPlants game;

    void start()
    {
        // game = GameObject.Find("Game");
    }

    public Text AliveNum;

    // Update is called once per frame
    void Update()
    {
        num = game.numberAlive;
        canLive = game.CanLiveBlock;
        round = game.round;
        percent = (float) num / (float) canLive * 100;

        // AliveNum.text =
        //     "Alive Percent : " + percent + "\n" + "Round : " + round;
        AliveNum.text =
            "Alive Percent :\n" +
            percent.ToString("#0.00") +
            "% \n" +
            "Round : " +
            round;
    }
}
