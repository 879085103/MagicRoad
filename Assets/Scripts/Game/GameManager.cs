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

    public static GameManager Instance;


    void Awake()
    {
        Instance = this;
    }
}
