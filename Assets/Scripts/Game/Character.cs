using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Character : MonoBehaviour {

    public Transform rayDown;
    public Transform rayLeft;
    public Transform rayRight;

    public LayerMask platfFormLayer;
    public LayerMask obstacleLayer;

    private Rigidbody2D rig;
    private SpriteRenderer spriteRender;
    private BoxCollider2D boxCollider;
    //是否向左移动
    private bool isMoveLeft = false;
    //是否在跳跃
    private bool isJumping = false;
    private Vector3 nextPlatformLeft;
    private Vector3 nextPlatformRight;

    private bool isMove = false;


    private ManagerVars vars;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        vars = ManagerVars.GetManagerVars();
    }

    void Update()
    {
        Debug.DrawRay(rayDown.position, Vector2.down , Color.red);
        Debug.DrawRay(rayLeft.position, Vector2.left * 0.15f, Color.red);
        Debug.DrawRay(rayRight.position, Vector2.right * 0.15f, Color.red);

        //如果游戏处于暂停状态,不进行跳跃
        if (GameManager.Instance.isPaused)
            return;
        //如果点击到UI,就不进行跳跃
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (GameManager.Instance.isGameStarted == false || GameManager.Instance.isGameOver)
            return;

        if ( Input.GetMouseButtonDown(0) && isJumping == false )
        {
            if(isMove ==false)
            {
                isMove = true;
                EventCenter.Broadcast(EventDefine.PlayerMove);
            }
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

        //第一种判断游戏结束的方法(落下时未检测到平台)
        if(rig.velocity.y < 0 && IsRayOnPlatform() == false && GameManager.Instance.isGameOver == false)
        {
            spriteRender.sortingLayerName = "Default";
            //将Collier设置为false主角会下落
            boxCollider.enabled = false;
            GameManager.Instance.isGameOver = true;
            GameManager.Instance.isGameStarted = false;
            //结束游戏界面
        }

        //第二种判断游戏结束的方法(碰到障碍物)
        if(isJumping && IsRayOnObstacle() == true&& GameManager.Instance.isGameOver == false)
        {
            PlayDeathEffect();
            Destroy(this.gameObject);
            GameManager.Instance.isGameOver = true;
            GameManager.Instance.isGameStarted = false;
        }

        //第三种判断游戏结束的方法(随平台一起落下)
        if(transform.position.y - Camera.main.transform.position.y < -6 && GameManager.Instance.isGameOver == false)
        {
            GameManager.Instance.isGameOver = true;
            GameManager.Instance.isGameStarted = false;
            gameObject.SetActive(false);
        }

    }

    private GameObject lastHitGo = null;
    /// <summary>
    /// 判断射线(向下)是否检测到平台
    /// </summary>
    private bool IsRayOnPlatform()
    {
        RaycastHit2D hit =  Physics2D.Raycast(rayDown.position, Vector2.down, 1.0f, platfFormLayer);
        if (hit.collider != null)
        {
            //当检测到平台时,分数增加
            if (hit.collider.tag == "Platform")
            {
                //如果上次检测到的平台和这次检测到的不一样,加分
                //防止重复加分
                if(lastHitGo != hit.collider.gameObject)
                {
                    //防止第一次跳跃分数多加1
                    if(lastHitGo == null)
                    {
                        lastHitGo = hit.collider.gameObject;
                        return true;
                    }
                    EventCenter.Broadcast(EventDefine.AddScore);
                    lastHitGo = hit.collider.gameObject;
                }
               
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 判断射线(向左或向右)是否检测到障碍物
    /// </summary>
    private bool IsRayOnObstacle()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(rayLeft.position, Vector2.left,0.15f, obstacleLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rayRight.position, Vector2.right, 0.15f, obstacleLayer);

        if(leftHit.collider != null)
        {
            if(leftHit.collider.tag =="Obstacle")
            {
                return true;
            }
        }

        if (rightHit.collider != null)
        {
            if (rightHit.collider.tag == "Obstacle")
            {
                return true;
            }
        }
        //如果左边和右边都没有检测到障碍物
        return false;
    }

    /// <summary>
    /// 播放死亡特效
    /// </summary>
    private void PlayDeathEffect()
    {
        GameObject go = ObjectPool.Instance.GetDeathEffect();
        go.SetActive(true);
        go.transform.position = transform.position;
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
        else if(collision.tag == "Diamond")
        {
            collision.gameObject.SetActive(false);
            EventCenter.Broadcast(EventDefine.AddDiamondCount);
        }
    }
}
