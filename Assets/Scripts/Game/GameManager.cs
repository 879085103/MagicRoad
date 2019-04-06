using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

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

    public static GameManager Instance;

    private int gameScore = 0;
    private int gameDiamondCount = 0;

    private void Awake()
    {
        Instance = this;
        EventCenter.AddListener(EventDefine.AddScore, AddScore);
        EventCenter.AddListener(EventDefine.AddDiamondCount, AddDiamond);
        EventCenter.AddListener(EventDefine.PlayerMove, PlayerMove);
    }

    private void Destroy()
    {
        EventCenter.RemoveListener(EventDefine.AddScore, AddScore);
        EventCenter.RemoveListener(EventDefine.PlayerMove, PlayerMove);
        EventCenter.RemoveListener(EventDefine.AddDiamondCount, AddDiamond);
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
}
