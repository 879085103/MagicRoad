using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPanel : MonoBehaviour {

    void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowResetPanel, ShowResetPanel);
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowResetPanel, ShowResetPanel);
    }

    private void ShowResetPanel()
    {
        gameObject.SetActive(true);
        StartCoroutine("DelayHide");
    }

    private IEnumerator DelayHide()
    {
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
    }

}
