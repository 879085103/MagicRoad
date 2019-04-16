using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu]
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

    public GameObject skinChooseItemPre;

    //商店皮肤
    public List<Sprite> skinSpriteList = new List<Sprite>();

    //角色皮肤
    public List<Sprite> characterSkinSpriteList = new List<Sprite>();

    //皮肤名
    public List<string> skinNameList = new List<string>();

    //皮肤花费
    public List<int> skinPriceList = new List<int>();

    //钻石
    public GameObject diamond;

    //死亡特效
    public GameObject deathEffect;

    //主角预制体
    public GameObject character;

    public static ManagerVars GetManagerVars()
    {
        return Resources.Load<ManagerVars>("ManagerVarsContainer");
    }

    public float nextXPos = 0.554f;
    public float nextYPos = 0.645f;

    public AudioClip jumpClip, fallClip, hitClip, diamondClip, buttonClip;

    public Sprite musicOn, musicOff;
}
