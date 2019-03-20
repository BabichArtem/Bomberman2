using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    List<Text> UIText;




    private void Start()
    {
        InitText();
    }

    private void InitText()
    {
        UIText = new List<Text>();
        
        UIText.Add(GameObject.Find("SpeedText").GetComponent<Text>());
        UIText.Add(GameObject.Find("BombCountText").GetComponent<Text>());
        UIText.Add(GameObject.Find("BombDistanceText").GetComponent<Text>());
        UIText.Add(GameObject.Find("WallWalkingText").GetComponent<Text>());

        for (int i = 0; i < UIText.Count; i++)
        {
            UIText[i].text = "";
        }
    }
    public void ChangeText(int UITextNumber, string val)
    {
        string[] Prefix = { "Speed: ", "Bomb Count: ", "Bomb Distance: ", "Wall Walking ON!" };
        UIText[UITextNumber].text = Prefix[UITextNumber] + val;

    }

}
