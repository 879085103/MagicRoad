using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LackDiamond : MonoBehaviour {

    private Image img_Bg;
    private Text txt_LackDiamond;

    void Awake()
    {
        EventCenter.AddListener<string>(EventDefine.ShowLackDiamond, Show);

        img_Bg = GetComponent<Image>();
        txt_LackDiamond = GetComponentInChildren<Text>();

        img_Bg.color = new Color(img_Bg.color.r, img_Bg.color.g, img_Bg.color.b, 0);
        txt_LackDiamond.color = new Color(txt_LackDiamond.color.r, txt_LackDiamond.color.g, txt_LackDiamond.color.b, 0);
    }

    void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventDefine.ShowLackDiamond, Show);
    }

    private void Show(string text)
    {
        StopCoroutine("Delay");
        txt_LackDiamond.text = text;
        transform.localPosition = new Vector3(0, -70, 0);
        transform.DOLocalMoveY(0, 0.3f).OnComplete(() => 
        {
            StartCoroutine("Delay");
        });
        img_Bg.DOColor(new Color(img_Bg.color.r, img_Bg.color.g, img_Bg.color.b, 0.4f), 0.1f);
        txt_LackDiamond.DOColor(new Color(txt_LackDiamond.color.r,txt_LackDiamond.color.g, txt_LackDiamond.color.b, 1), 0.1f);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);

        transform.DOLocalMoveY(70, 0.3f);
        img_Bg.DOColor(new Color(img_Bg.color.r, img_Bg.color.g, img_Bg.color.b, 0), 0.1f);
        txt_LackDiamond.DOColor(new Color(txt_LackDiamond.color.r,txt_LackDiamond.color.g, txt_LackDiamond.color.b, 0), 0.1f);
    }
}
