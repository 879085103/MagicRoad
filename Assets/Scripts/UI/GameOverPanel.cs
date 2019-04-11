using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {

    private Button btn_Home;
    private Button btn_Rank;
    private Button btn_Restart;
    private Text txt_AddDiamondCount;
    private Text txt_Score;
    private Text txt_highestScore;
    private Image img_New;

	
	void Start () {
        EventCenter.AddListener(EventDefine.ShowGameOverPanel, ShowGameOverPanel);
        Init();
	}

    void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGameOverPanel, ShowGameOverPanel);
    }
	
    private void Init()
    {
        btn_Home = transform.Find("Btn_Home").GetComponent<Button>();
        btn_Home.onClick.AddListener(OnHomeButtonClick);

        btn_Rank = transform.Find("Btn_Rank").GetComponent<Button>();
        btn_Rank.onClick.AddListener(OnRankButtonClick);

        btn_Restart = transform.Find("Btn_Restart").GetComponent<Button>();
        btn_Restart.onClick.AddListener(OnRestartButtonClick);

        txt_AddDiamondCount = transform.Find("Diamond/txt_AddDiamondCount").GetComponent<Text>();
        txt_Score = transform.Find("txt_Score").GetComponent<Text>();
        txt_highestScore = transform.Find("txt_highestScore").GetComponent<Text>();

        img_New = transform.Find("Img_New").GetComponent<Image>();


        gameObject.SetActive(false);
    }

    /// <summary>
    /// 主界面按钮点击
    /// </summary>
    private void OnHomeButtonClick()
    {
        //重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.isRestartGame = false;
    }

    /// <summary>
    /// 排行榜按钮点击
    /// </summary>
    private void OnRankButtonClick()
    {
        EventCenter.Broadcast(EventDefine.ShowRankPanel);
    }

    /// <summary>
    /// 再试一次按钮点击
    /// </summary>
    private void OnRestartButtonClick()
    {

        //重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.isRestartGame = true;
    }

    private void ShowGameOverPanel()
    {
        
        txt_AddDiamondCount.text = "+" + GameManager.Instance.GetDiamondCount().ToString();
        txt_Score.text = GameManager.Instance.GetGameScore().ToString();

        //游戏结束后更新钻石数
        GameManager.Instance.UpdateAllDiamond(GameManager.Instance.GetDiamondCount());
        //游戏结束后更新成绩
        if(GameManager.Instance.GetGameScore() > GameManager.Instance.GetBestScore())
        {
            txt_highestScore.text = "最高分  " + GameManager.Instance.GetGameScore();
            img_New.gameObject.SetActive(true);
        }
        else
        {
            txt_highestScore.text = "最高分  " + GameManager.Instance.GetBestScore();
            img_New.gameObject.SetActive(false);
        }
        GameManager.Instance.SaveScore(GameManager.Instance.GetGameScore());

        gameObject.SetActive(true);
    }
}
