using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManagerVars : ScriptableObject {

    //背景主题
    public List<Sprite> bgThemeSpriteList = new List<Sprite>();

    //平台主题
    public List<Sprite> platformThemeSpriteList = new List<Sprite>();

    //通用组合平台
    public List<GameObject> commonPlatform = new List<GameObject>();

    //Winter系列平台预制体
    public List<GameObject> winterPlatform = new List<GameObject>();

    //Grass系列平台预制体
    public List<GameObject> grassPlatform = new List<GameObject>();

    //Spike平台预制体
    public List<GameObject> spikePlatform = new List<GameObject>();

    //普通平台
    public GameObject normalPlatform;

    //主角预制体
    public GameObject character;

    public static ManagerVars GetManagerVars()
    {
        return Resources.Load<ManagerVars>("ManagerVarsContainer");
    }

    public float nextXPos = 0.554f;
    public float nextYPos = 0.645f;

}
