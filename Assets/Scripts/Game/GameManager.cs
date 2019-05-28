using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    /// <summary>
    /// 游戏是否开始
    /// </summary>
    public bool isGameStarted { get; set; }
    /// <summary>
    /// 游戏是否结束
    /// </summary>
    public bool isGameOver { get; set; } 
    /// <summary>
    /// 游戏是否暂停
    /// </summary>
    public bool isPaused { get; set; }
    /// <summary>
    /// 玩家是否开始移动
    /// </summary>
    public bool isPlayerMove { get; set; }
    /// <summary>
    /// 当前皮肤
    /// </summary>
    //public Sprite currentSkin;

    private GameData gameData;

    private bool isFirstGame;
    private bool isMusicOn;
    private int[] bestScoreArr;
    private int selectSkin;
    private bool[] skinUnlocked;
    private int diamondCount;
  
    private int gameScore = 0;
    private int gameDiamondCount = 0;
    private ManagerVars vars;


    private void Awake()
    {
        Instance = this;
        vars = ManagerVars.GetManagerVars();
        EventCenter.AddListener(EventDefine.AddScore, AddScore);
        EventCenter.AddListener(EventDefine.AddDiamondCount, AddDiamond);
        EventCenter.AddListener(EventDefine.PlayerMove, PlayerMove);

        if(GameData.isRestartGame == true)
        {
            isGameStarted = true;
        }
        InitGameData();
        
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.AddScore, AddScore);
        EventCenter.RemoveListener(EventDefine.PlayerMove, PlayerMove);
        EventCenter.RemoveListener(EventDefine.AddDiamondCount, AddDiamond);
    }

    /// <summary>
    /// 初始化游戏数据
    /// </summary>
    private void InitGameData()
    {
        
        //读取游戏数据
        LoadGameData();
        
        if (gameData != null)
        {
            isFirstGame = gameData.IsFirstGame;
        }
        else
        {
            isFirstGame = true;
        }
        //如果第一次开始游戏，初始化游戏数据
        if(isFirstGame)
        {
            isFirstGame = false;
            isMusicOn = true;
            bestScoreArr = new int[3];
            selectSkin = 0;
            skinUnlocked = new bool[vars.skinSpriteList.Count];
            skinUnlocked[0] = true;
            diamondCount = 10;
            SaveGameData();
        }
        //如果不是第一次开始游戏,就读取Json中保存的数据
        else
        {
            isMusicOn = gameData.IsMusicOn;
            bestScoreArr = gameData.BestScoreArr;
            selectSkin = gameData.SelectSkin;
            skinUnlocked = gameData.SkinUnlocked;
            diamondCount = gameData.DiamondCount;
        }

    }

    private void PlayerMove()
    {
        isPlayerMove = true;
    }

    private void AddScore()
    {
        if (isGameStarted == false || isGameOver == true || isPaused == true)
            return;
        gameScore++;
        EventCenter.Broadcast(EventDefine.UpdateScore, gameScore);
    }

    private void AddDiamond()
    {
        if (isGameStarted == false || isGameOver == true || isPaused == true)
            return;
        gameDiamondCount++;
        EventCenter.Broadcast(EventDefine.UpdateDiamondCount, gameDiamondCount);
    }

    public int GetGameScore()
    {
        return gameScore;
    }

    public int GetDiamondCount()
    {
        return gameDiamondCount;
    }

    //获取用户拥有的所有钻石数
    public int GetAllDiamond()
    {
        return diamondCount;
    }

    //更新总钻石
    public void UpdateAllDiamond(int value)
    {
        diamondCount += value;
        SaveGameData();
    }

    //保存成绩
    public void SaveScore(int score)
    {
        List<int> list = bestScoreArr.ToList();
        //成绩从大到小排序
        list.Sort((x,y) => (-x.CompareTo(y)));
        bestScoreArr = list.ToArray();

        
        int index = -1;
        for(int i = 0; i <bestScoreArr.Length; i++)
        {
            if(bestScoreArr[i] < score)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            return;
        else
        {
            for(int i = bestScoreArr.Length - 1; i > index; i--)
            {
                bestScoreArr[i] = bestScoreArr[i - 1];
            }
            bestScoreArr[index] = score;
        }
        SaveGameData();
    }

    //获取最高分
    public int GetBestScore()
    {
        return bestScoreArr.Max();
    }

    //获取分数排行数组
    public int[] GetBestScoreArr()
    {
        return bestScoreArr;
    }

    //设置音效是否开启
    public void SetIsMusicOn(bool value)
    {
        isMusicOn = value;
        SaveGameData();
    }

    public bool GetIsMusicOn()
    {
        return isMusicOn;
    }
    /// <summary>
    /// 当前皮肤是否解锁
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool GetSkinUnlocked(int index)
    {
        return skinUnlocked[index];
    }


    public void SetSkinUnlocked(int index)
    {
        skinUnlocked[index] = true;
        SaveGameData();
    }

    public int GetSelectSkin()
    {
        return selectSkin;
    }

    public void SetSelectSkin(int index)
    {
        selectSkin = index;
        SaveGameData();
    }

    //重置游戏数据
    public void ResetGameData()
    {
        isFirstGame = false;
        isMusicOn = true;
        bestScoreArr = new int[3];
        selectSkin = 0;
        skinUnlocked = new bool[vars.skinSpriteList.Count];
        skinUnlocked[0] = true;
        diamondCount = 10;
        SaveGameData();
    }

    //保存游戏数据
    //public void SaveGameData()
    //{
    //    gameData = new GameData();
    //    gameData.IsFirstGame = isFirstGame;
    //    gameData.IsMusicOn = isMusicOn;
    //    gameData.SelectSkin = selectSkin;
    //    gameData.SkinUnlocked = skinUnlocked;
    //    gameData.BestScoreArr = bestScoreArr;
    //    gameData.DiamondCount = diamondCount;
        
    //    string filePath = Application.dataPath + "/Resources/GameData.txt";
    //    //将GameData对象转换为Json格式的字符串
    //    string saveData = JsonMapper.ToJson(gameData);
    //    //将字符串写入到文件中
    //    //创建StreamWriter,并将字符串写入
    //    StreamWriter writer = new StreamWriter(filePath);
    //    writer.Write(saveData);
    //    //关闭StreamWriter
    //    writer.Close();

    //}

    public void SaveGameData()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream fs = File.Create(Application.persistentDataPath + "/GameData2.txt"))
            {
                gameData = new GameData();
                gameData.IsFirstGame = isFirstGame;
                gameData.IsMusicOn = isMusicOn;
                gameData.SelectSkin = selectSkin;
                gameData.SkinUnlocked = skinUnlocked;
                gameData.BestScoreArr = bestScoreArr;
                gameData.DiamondCount = diamondCount;

                bf.Serialize(fs, gameData);
            }   
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
        
    }

    //读取游戏数据
    //public void LoadGameData()
    //{
    //    string filePath = Application.dataPath + "/Resources/GameData.txt";
    //    if(File.Exists(filePath))
    //    {
    //        //创建一个StreamReader,用来读取流
    //        StreamReader reader = new StreamReader(filePath);
    //        //将读取到的流赋值给JsonStr
    //        string jsonStr = reader.ReadToEnd();
    //        //关闭
    //        reader.Close();
    //        //将jsonStr转换为GameData对象
    //        gameData = JsonMapper.ToObject<GameData>(jsonStr);
    //    }
    //    else
    //    {
    //        Debug.Log("存档失败");
    //    }
    //}

    public void LoadGameData()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(Application.persistentDataPath + "/GameData2.txt",FileMode.Open))
            {
                 gameData = (GameData) bf.Deserialize(fs);
            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
        
    }
}
