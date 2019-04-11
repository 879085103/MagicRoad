using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {

    private Button btn_Start;
    private Button btn_Shop;
    private Button btn_Rank;
    private Button btn_Sound;
    private Button btn_Reset;

    private ManagerVars vars;


    void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        EventCenter.AddListener(EventDefine.ShowMainPanel, Show);
        EventCenter.AddListener<int>(EventDefine.ChangeSkin, ChangeSkin);
        Init();
    }

    //设置图标UI
    private void ChangeSkin(int skinIndex)
    {
        btn_Shop.transform.GetChild(0).GetComponent<Image>().sprite = vars.skinSpriteList[skinIndex];
    }

    void Start()
    {
        if (GameData.isRestartGame)
        {
            gameObject.SetActive(false);
            EventCenter.Broadcast(EventDefine.ShowGamePanel);
        }
        ChangeSkin(GameManager.Instance.GetSelectSkin());
    }

    void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowMainPanel, Show);
        EventCenter.RemoveListener<int>(EventDefine.ChangeSkin, ChangeSkin);
    }

    private void Init()
    {
        btn_Start = transform.Find("Btn_Start").GetComponent<Button>();
        btn_Start.onClick.AddListener(OnStartButtonClick);
        btn_Shop = transform.Find("Btns/Btn_Shop").GetComponent<Button>();
        btn_Shop.onClick.AddListener(OnShopButtonClick);
        btn_Rank = transform.Find("Btns/Btn_Rank").GetComponent<Button>();
        btn_Rank.onClick.AddListener(OnRankButtonClick);
        btn_Sound = transform.Find("Btns/Btn_Sound").GetComponent<Button>();
        btn_Sound.onClick.AddListener(OnSoundButtonClick);
        btn_Sound = transform.Find("Btns/Btn_Reset").GetComponent<Button>();
        btn_Sound.onClick.AddListener(OnResetButtonClick);

    }

    /// <summary>
    /// 开始按钮点击后调用此方法
    /// </summary>
    private void OnStartButtonClick()
    {
        GameManager.Instance.isGameStarted = true;
        EventCenter.Broadcast(EventDefine.ShowGamePanel);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 商店按钮点击
    /// </summary>
    private void OnShopButtonClick()
    {
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.ShowShopPanel);
    }

    /// <summary>
    /// 排行榜按钮点击
    /// </summary>
    private void OnRankButtonClick()
    {
        
        EventCenter.Broadcast(EventDefine.ShowRankPanel);
    }

    /// <summary>
    /// 声音按钮点击
    /// </summary>
    private void OnSoundButtonClick()
    {

    }

    /// <summary>
    /// 重置按钮点击
    /// </summary>
    private void OnResetButtonClick()
    {
        GameManager.Instance.ResetGameData();
        //重置皮肤
        EventCenter.Broadcast(EventDefine.ChangeSkin,GameManager.Instance.GetSelectSkin());
        EventCenter.Broadcast(EventDefine.ShowResetPanel);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

}
