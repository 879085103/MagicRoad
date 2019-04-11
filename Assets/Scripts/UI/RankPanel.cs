using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : MonoBehaviour {

    private Button btn_Back;
    private Text[] txt_Scores;

    void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowRankPanel, ShowRankPanel);
        Init();
        gameObject.SetActive(false);
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
    }

    private void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.ShowMainPanel);
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

    }


}
