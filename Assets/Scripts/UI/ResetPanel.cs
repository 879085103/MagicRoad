using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ResetPanel : MonoBehaviour {

    private Image background;
    private GameObject succeedReset;
    private GameObject dialog;
    private Button btn_Yes;
    private Button btn_No;

    void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowResetPanel, ShowResetPanel);
        succeedReset = transform.Find("Succeed").gameObject;
        dialog = transform.Find("Dialog").gameObject;
        background = transform.Find("background").GetComponent<Image>();
        btn_Yes = transform.Find("Dialog/btn_Yes").GetComponent<Button>();
        btn_Yes.onClick.AddListener(OnYesButtonClick);
        btn_No = transform.Find("Dialog/btn_No").GetComponent<Button>();
        btn_No.onClick.AddListener(OnNoButtonClick);

        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        dialog.transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowResetPanel, ShowResetPanel);
    }

    private void ShowResetPanel()
    {
        gameObject.SetActive(true);
        dialog.SetActive(true);
        dialog.transform.DOScale(Vector3.one, 0.3f);
        background.DOColor(new Color(background.color.r, background.color.g, background.color.b, 0.3f), 0.3f);
    }

    private IEnumerator DelayHide()
    {
        yield return new WaitForSeconds(0.8f);
        succeedReset.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnYesButtonClick()
    {
        EventCenter.Broadcast(EventDefine.PlayAudio);
        GameManager.Instance.ResetGameData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //重置皮肤
        //EventCenter.Broadcast(EventDefine.ChangeSkin, GameManager.Instance.GetSelectSkin());
        dialog.SetActive(false);
        //显示成功重置界面
        succeedReset.SetActive(true);
        //0.8s后隐藏
        StartCoroutine("DelayHide");

        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        dialog.transform.localScale = Vector3.zero;
    }

    private void OnNoButtonClick()
    {
        EventCenter.Broadcast(EventDefine.PlayAudio);
        dialog.transform.DOScale(Vector3.zero, 0.3f);
        background.DOColor(new Color(background.color.r, background.color.g, background.color.b, 0f), 0.3f).OnComplete(()=>
        {
            gameObject.SetActive(false);
        });
    }

}
