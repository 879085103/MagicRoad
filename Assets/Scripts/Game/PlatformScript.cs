using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    public SpriteRenderer[] m_SpriteRenderers ;
    public GameObject[] m_Obstales;

    void Update()
    {
        if (GameManager.Instance.isGameStarted )
        {
            Invoke("Fall", 3.0f);
        }
       
    }

    public void Init(Sprite m_Sprite, bool isRightSpawn = true)
    {
        for(int i = 0; i < m_SpriteRenderers.Length;i++)
        {
            m_SpriteRenderers[i].sprite = m_Sprite;
        }
        //如果是组合平台的话,将障碍物的方向与平台生成方向相反
        for(int j = 0; j <m_Obstales.Length;j++)
        {
            m_Obstales[j].transform.localPosition = isRightSpawn ? new Vector3(-1.09f, 0, 0) : new Vector3(1.09f, 0, 0);
        }
    }


    public void Fall()
    {
        this.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

}
