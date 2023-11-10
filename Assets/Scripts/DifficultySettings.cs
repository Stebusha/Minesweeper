using System;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySettings : MonoBehaviour
{
    [SerializeField] private Dropdown levels;
    [SerializeField] private Dropdown heightDD;
    [SerializeField] private Dropdown widthDD;
    [SerializeField] private Dropdown minesDD;
    public static int gridHeight=10;
    public static int gridWidth=10;
    public static int countOfMines=10;
    
    private void FixedUpdate()
    {
        SelectBoardsOfLevel();
        SelectNumOfMines();
        gridHeight = Convert.ToInt32(heightDD.captionText.text);
        gridWidth = Convert.ToInt32(widthDD.captionText.text);
        countOfMines = Convert.ToInt32(minesDD.captionText.text);
    }
    public void SelectBoardsOfLevel()

    {
        if (levels.options.Count != 0)
        {
            if (levels.captionText.text == "Easy")
            {
                heightDD.options.Clear();
                widthDD.options.Clear();
                for(int i = 10; i < 13; i++)
                {
                    heightDD.options.Add(new Dropdown.OptionData(i.ToString()));
                    widthDD.options.Add(new Dropdown.OptionData(i.ToString()));
                }
                heightDD.RefreshShownValue();
                widthDD.RefreshShownValue();
            }
            else if(levels.captionText.text == "Normal")
            {
                heightDD.options.Clear();
                widthDD.options.Clear();
                for (int i = 13; i < 16; i++)
                {
                    heightDD.options.Add(new Dropdown.OptionData(i.ToString()));
                    widthDD.options.Add(new Dropdown.OptionData(i.ToString()));
                }
                heightDD.RefreshShownValue();
                widthDD.RefreshShownValue();
            }
            else
            {
                heightDD.options.Clear();
                widthDD.options.Clear();
                for (int i = 16; i < 19; i++)
                {
                    heightDD.options.Add(new Dropdown.OptionData(i.ToString()));
                    widthDD.options.Add(new Dropdown.OptionData(i.ToString()));
                }
                heightDD.RefreshShownValue();
                widthDD.RefreshShownValue();
            }
        }
    }

    public void SelectNumOfMines()
    {
        if (levels.captionText.text == "Easy")
        {
            minesDD.options.Clear();
            for(int i = 10; i < 26; i++)
            {
                minesDD.options.Add(new Dropdown.OptionData(i.ToString()));
            }
            minesDD.RefreshShownValue();
        }
        else if(levels.captionText.text == "Normal")
        {
            minesDD.options.Clear();
            for(int i = 26; i < 51; i++)
            {
                minesDD.options.Add(new Dropdown.OptionData(i.ToString()));
            }
            minesDD.RefreshShownValue();
        }
        else
        {
            minesDD.options.Clear();
            for (int i = 51; i < 76; i++)
            {
                minesDD.options.Add(new Dropdown.OptionData(i.ToString()));
            }
            minesDD.RefreshShownValue();
        }
    }
}
