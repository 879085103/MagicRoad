using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour {

    //是否向左移动
    private bool isMoveLeft = false;
    //是否在跳跃
    private bool isJumping = false;
    private Vector3 nextPlatformLeft;
    private Vector3 nextPlatformRight;


    private ManagerVars vars;

    void Awake()
    {
       
        vars = ManagerVars.GetManagerVars();
    }

    void Update()
    {
        if (GameManager.Instance.isGameStarted == false || GameManager.Instance.isGameOver)
            return;

        if ( Input.GetMouseButtonDown(0) && isJumping == false )
        {
            isJumping = true;
            Vector3 mousePos = Input.mousePosition;
            //点击的是左边屏幕 
            if(mousePos.x <= Screen.width /2)
            {
                isMoveLeft = true;
            }
            //点击右边屏幕
            else if(mousePos.x > Screen.width/2)
            {
                isMoveLeft = false;
            }
            Jump();

            //调用生成平台的方法
            EventCenter.Broadcast(EventDefine.PlatformSpawn);
        }
        
    }

    private void Jump()
    {
        if(isMoveLeft)
        {
            //转向
            transform.localScale = new Vector3(-1, 1, 1);
            transform.DOMoveX(nextPlatformLeft.x, 0.2f);
            transform.DOMoveY(nextPlatformLeft.y + 0.8f, 0.15f);
      
        }
        else
        {
            transform.localScale = Vector3.one;
            transform.DOMoveX(nextPlatformRight.x, 0.2f);
            transform.DOMoveY(nextPlatformRight.y + 0.8f, 0.15f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰撞到平台说明跳跃结束
        isJumping = false;
        if(collision.tag == "Platform")
        {
            //获取当前平台位置
            Vector3 currentPlatformPos = collision.gameObject.transform.position;
            nextPlatformLeft = new Vector3(currentPlatformPos.x - vars.nextXPos, currentPlatformPos.y + vars.nextYPos, 0);
            nextPlatformRight = new Vector3(currentPlatformPos.x + vars.nextXPos, currentPlatformPos.y + vars.nextYPos, 0);
        }
    }
}
