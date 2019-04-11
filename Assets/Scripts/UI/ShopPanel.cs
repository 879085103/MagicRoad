using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopPanel : MonoBehaviour {

    private Text txt_Name;
    private Text txt_DiamondCount;
    private Text txt_Price;
    private Button btn_Back;
    private Button btn_Buy;
    private Button btn_Select;
    //当前选中皮肤的下标
    private int currentIndex;

    private ManagerVars vars;
    private Transform parent;

    void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowShopPanel, Show);
    }

    void Start()
    {
        init();
    }

    void Update()
    {
        //根据parent的localPosition的范围确定当前的皮肤
        currentIndex = (int)Mathf.Round(parent.transform.localPosition.x / -160.0f);
        //当松开鼠标时,自动跳到当前最近的皮肤
        if(Input.GetMouseButtonUp(0))
        {
            parent.transform.DOLocalMoveX(currentIndex * -160, 0.2f);
        }
        SetSkinItemSize(currentIndex);
    
        RefreshUI(currentIndex);

    }

    void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowShopPanel, Show);
    }

    private void init()
    {
        parent = transform.Find("ScrollRect/Parent");
        
        vars = ManagerVars.GetManagerVars();

        txt_Name = transform.Find("txt_Name").GetComponent<Text>();
        txt_DiamondCount = transform.Find("Diamond/txt_DiamondCount").GetComponent<Text>();
        txt_Price = transform.Find("btn_Buy/txt_Price").GetComponent<Text>();
        btn_Back = transform.Find("btn_Back").GetComponent<Button>();
        btn_Back.onClick.AddListener(OnBackButtonClick);
        btn_Buy = transform.Find("btn_Buy").GetComponent<Button>();
        btn_Buy.onClick.AddListener(OnBuyButtonClick);
        btn_Select = transform.Find("btn_Select").GetComponent<Button>();
        btn_Select.onClick.AddListener(OnSelectButtonClick);

        //根据皮肤数设置parent的Width
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2((vars.skinSpriteList.Count + 2) * 160, 310);
        for(int i = 0; i < vars.skinSpriteList.Count; i++)
        {
            GameObject go = Instantiate(vars.skinChooseItemPre, parent);
            //未解锁
            if (GameManager.Instance.GetSkinUnlocked(i) == false)
            {
                go.transform.Find("Image").GetComponent<Image>().color = Color.gray;
            }
            else
            {
                go.transform.Find("Image").GetComponent<Image>().color = Color.white;
            }
            //设置皮肤图片
            go.transform.Find("Image").GetComponent<Image>().sprite = vars.skinSpriteList[i];
            go.transform.localPosition = new Vector3((i + 1) * 160, 0, 0);
        }
        
        gameObject.SetActive(false);
    }

    //当前选中皮肤的放大
    private void SetSkinItemSize(int index)
    {
        for(int i = 0; i < parent.childCount;i++)
        {
            //将parent的第index个子物体放大
            if(index == i)
            {
                parent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
            }
            else
            {
                parent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
            }
        }
        
    }

    //更新当前选中皮肤的数据
    private void RefreshUI(int index)
    {
        txt_Name.text = vars.skinNameList[index];
        //显示钻石数量
        txt_DiamondCount.text = GameManager.Instance.GetAllDiamond().ToString();
        if (GameManager.Instance.GetSkinUnlocked(index) == false)
        {
            btn_Buy.gameObject.SetActive(true);
            btn_Select.gameObject.SetActive(false);
            txt_Price.text = vars.skinPriceList[index].ToString();
        }
        else
        {
            btn_Buy.gameObject.SetActive(false);
            btn_Select.gameObject.SetActive(true);
            
        }
    }

    //返回按钮的点击事件
    private void OnBackButtonClick()
    {
        gameObject.SetActive (false);
        EventCenter.Broadcast(EventDefine.ShowMainPanel);
    }
    //选择皮肤按钮点击事件
    private void OnSelectButtonClick()
    {
        gameObject.SetActive(false);
        GameManager.Instance.isGameStarted = true;
        EventCenter.Broadcast(EventDefine.ShowGamePanel);
        GameManager.Instance.SetSelectSkin(currentIndex);
        EventCenter.Broadcast(EventDefine.ChangeSkin, currentIndex);
    }
    //购买皮肤按钮点击事件
    private  void OnBuyButtonClick()
    {
        int price = int.Parse(txt_Price.text);
        if(price > GameManager.Instance.GetAllDiamond())
        {
            Debug.Log("钻石不足，无法购买皮肤");
            return;
        }
        //消耗钻石
        GameManager.Instance.UpdateAllDiamond(-price);
        //解锁皮肤
        GameManager.Instance.SetSkinUnlocked(currentIndex);
        parent.GetChild(currentIndex).GetChild(0).GetComponent<Image>().color = Color.white;
    }

    private void Show()
    {
        gameObject.SetActive(true);
        //打开页面直接定位到选中的皮肤
        parent.transform.localPosition = new Vector3(GameManager.Instance.GetSelectSkin() * -160, 0, 0);
    }
}
