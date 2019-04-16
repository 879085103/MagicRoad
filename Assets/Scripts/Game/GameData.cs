using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

    /// <summary>
    /// 是否再来一次游戏
    /// </summary>
    public static bool isRestartGame = false;

    private bool isFirstGame;
    private bool isMusicOn;
    private int[] bestScoreArr;
    private int selectSkin;
    private bool[] skinUnlocked;
    private int diamondCount;

    public bool IsFirstGame
    {
        get { return isFirstGame; }
        set { isFirstGame = value; }
    }

    public bool IsMusicOn
    {
        get { return isMusicOn; }
        set { isMusicOn = value; }
    }

    public int[] BestScoreArr
    {
        get { return bestScoreArr; }
        set { bestScoreArr = value; }
    }

    public int SelectSkin
    {
        get { return selectSkin; }
        set { selectSkin = value; }
    }

    public bool[] SkinUnlocked
    {
        get { return skinUnlocked; }
        set { skinUnlocked = value; }
    }

    public int DiamondCount
    {
        get { return diamondCount; }
        set { diamondCount = value; }
    }



    //private bool GetIsFirstGame()
    //{
    //    return isFirstGame;
    //}

    //public void SetIsFirstGame(bool isFirstGame)
    //{
    //    this.isFirstGame = isFirstGame;
    //}
    
    //public bool GetIsMusicOn()
    //{
    //    return isMusicOn;
    //}

    //public void SetIsMusicOn(bool isMusicOn)
    //{
    //    this.isMusicOn = isMusicOn;
    //}

    //public int [] GetBestScoreArr()
    //{
    //    return bestScoreArr;
    //}

    //public void SetBestScoreArr(int [] bestScoreArr)
    //{
    //    this.bestScoreArr = bestScoreArr;
    //}

    //public int GetSelectSkin()
    //{
    //    return selectSkin;
    //}

    //public void SetSelectSkin(int selectSkin)
    //{
    //    this.selectSkin = selectSkin;
    //}

    //public bool [] GetSkinUnlocked()
    //{
    //    return skinUnlocked;
    //}

    //public void SetSkinUnlocked(bool[] skinUnlocked)
    //{
    //    this.skinUnlocked = skinUnlocked;
    //}

    //public int GetDiamondCount()
    //{
    //    return diamondCount;
    //}

    //public void SetDiamondCount(int diamondCount)
    //{
    //    this.diamondCount = diamondCount;
    //}

}
