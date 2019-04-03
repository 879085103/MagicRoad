using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bgTheme : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private ManagerVars vars;

    //private string bgResource = "Res/Textures2D/bg";

    void Awake()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
        vars = ManagerVars.GetManagerVars();

        int ranValue = Random.Range(0, vars.bgThemeSpriteList.Count );
        spriteRenderer.sprite = vars.bgThemeSpriteList[ranValue];
        ////动态加载资源
        //Sprite  bgSprite =  Resources.Load<Sprite>(bgResource + Random.Range(1, 4));
        //spriteRenderer.sprite = bgSprite;
    }
}
