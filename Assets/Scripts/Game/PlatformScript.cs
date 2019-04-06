using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    public SpriteRenderer[] m_SpriteRenderers ;
    public GameObject[] m_Obstales;
    private bool startTimer = false;
    private float fallTime;
    private Rigidbody2D my_Body;


    private void Awake()
    {
        my_Body = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
      if(GameManager.Instance.isGameStarted && GameManager.Instance.isGameOver == false && startTimer == true && GameManager.Instance.isPlayerMove)
        {
            fallTime -= Time.deltaTime;
            if(fallTime < 0)
            {
                startTimer = false;
                if(my_Body.bodyType != RigidbodyType2D.Dynamic)
                {
                    StartCoroutine("HidePlatform");
                    my_Body.bodyType = RigidbodyType2D.Dynamic;
                }
            }

        }
        //如果平台与摄像机距离过大，将平台隐藏
        if(transform.position.y - Camera.main.transform.position.y < -6)
        {
            StartCoroutine("HidePlatform");
        }
    }

    public void Init(Sprite m_Sprite,float fallTime, bool isRightSpawn = true)
    {
        startTimer = true;
        this.fallTime = fallTime;
        my_Body.bodyType = RigidbodyType2D.Static;
        for (int i = 0; i < m_SpriteRenderers.Length;i++)
        {
            m_SpriteRenderers[i].sprite = m_Sprite;
        }
        //如果是组合平台的话,将障碍物的方向与平台生成方向相反
        for(int j = 0; j <m_Obstales.Length;j++)
        {
            m_Obstales[j].transform.localPosition = isRightSpawn ? new Vector3(-1.09f, 0, 0) : new Vector3(1.09f, 0, 0);
        }
    }


    public IEnumerator HidePlatform()
    {
        //落下2s后隐藏
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
        //my_Body.bodyType = RigidbodyType2D.Static;
    }
}
