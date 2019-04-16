using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RankPanel : MonoBehaviour {

    private Button btn_Back;
    private Text[] txt_Scores;
    public GameObject scoreList;

    void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowRankPanel, ShowRankPanel);
        Init();
        
    }

    void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowRankPanel, ShowRankPanel);
    }

    private void Init()
    {
        btn_Back = transform.Find("btn_Back").GetComponent<Button>();
        btn_Back.onClick.AddListener(OnBackButtonClick);

        txt_Scores = transform.Find("ScoreList").GetComponentsInChildren<Text>();

        scoreList = transform.Find("ScoreList").gameObject;

        btn_Back.GetComponent<Image>().color = new Color(btn_Back.GetComponent<Image>().color.r, btn_Back.GetComponent<Image>().color.g, btn_Back.GetComponent<Image>().color.b, 0);
        scoreList.transform.localScale = Vector3.zero;

        gameObject.SetActive(false);
    }

    private void OnBackButtonClick()
    {
        EventCenter.Broadcast(EventDefine.PlayAudio);
        btn_Back.GetComponent<Image>().DOColor(new Color(btn_Back.GetComponent<Image>().color.r, btn_Back.GetComponent<Image>().color.g, btn_Back.GetComponent<Image>().color.b, 0), 0.3f);
        scoreList.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });    
    }

    private void ShowRankPanel()
    {
        //显示排行榜
        int[] bestScoreArr = GameManager.Instance.GetBestScoreArr();
        for(int i = 0; i < txt_Scores.Length; i++)
        {
            txt_Scores[i].text = bestScoreArr[i].ToString();
        }

        gameObject.SetActive(true);
        scoreList.transform.DOScale(Vector3.one, 0.3f);
        btn_Back.GetComponent<Image>().DOColor(new Color(btn_Back.GetComponent<Image>().color.r, btn_Back.GetComponent<Image>().color.g, btn_Back.GetComponent<Image>().color.b, 0.3f),0.3f);

    }


}
