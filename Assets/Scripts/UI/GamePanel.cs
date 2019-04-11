using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {

    private Button btn_Pause;
    private Button btn_Play;
    private Text txt_Score;
    private Text txt_DiamondCount;


    private void Awake()
    {
        //注册事件,添加监听
        EventCenter.AddListener(EventDefine.ShowGamePanel, Show);
        EventCenter.AddListener<int>(EventDefine.UpdateScore, UpdateScore);
        EventCenter.AddListener<int>(EventDefine.UpdateDiamondCount, UpdateDiamondCount);
        Init();
    }

    private void Init()
    {
        btn_Pause = transform.Find("Btn_Pause").GetComponent<Button>();
        btn_Pause.onClick.AddListener(OnPauseButtonClick);

        btn_Play = transform.Find("Btn_Play").GetComponent<Button>();
        btn_Play.onClick.AddListener(OnPlayButtonClick);

        txt_Score = transform.Find("txt_Score").GetComponent<Text>();
        txt_DiamondCount = transform.Find("Diamond/txt_Diamond").GetComponent<Text>();

        btn_Play.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Update()
    {
       
    }

    /// <summary>
    /// 继续按钮点击
    /// </summary>
    private void OnPlayButtonClick()
    {
        //隐藏开始按钮,显示暂停按钮
        btn_Pause.gameObject.SetActive(true);
        btn_Play.gameObject.SetActive(false);

        //游戏继续
        Time.timeScale = 1;
        GameManager.Instance.isPaused = false;
    }

    /// <summary>
    /// 暂停按钮点击
    /// </summary>
    private void OnPauseButtonClick()
    {
        //隐藏暂停按钮,显示开始按钮
        btn_Play.gameObject.SetActive(true);
        btn_Pause.gameObject.SetActive(false);

        //游戏暂停
        Time.timeScale = 0;
        GameManager.Instance.isPaused = true;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void UpdateScore(int score)
    {
        txt_Score.text = score.ToString();
    }

    private void UpdateDiamondCount(int diamondCount)
    {
        txt_DiamondCount.text = diamondCount.ToString();
    }

    private void OnDestroy()
    {
        //移除监听
        EventCenter.RemoveListener(EventDefine.ShowGamePanel,Show);
        EventCenter.RemoveListener<int>(EventDefine.UpdateScore, UpdateScore);
        EventCenter.RemoveListener<int>(EventDefine.UpdateDiamondCount, UpdateDiamondCount);
    }

  
    
}
