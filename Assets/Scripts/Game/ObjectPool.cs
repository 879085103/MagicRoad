using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool Instance;

    public int initSpawnCount = 5;

    private List<GameObject> normalPlatformList = new List<GameObject>();
    private List<GameObject> commonPlatformList = new List<GameObject>(); 
    private List<GameObject> grassPlatformList = new List<GameObject>();
    private List<GameObject> winterPlatformList = new List<GameObject>();
    private List<GameObject> spikePlatformList = new List<GameObject>();
    private List<GameObject> deathEffectList = new List<GameObject>();
    private List<GameObject> diamondList = new List<GameObject>();

    private ManagerVars vars;

    private void Awake()
    {
        Instance = this;
        vars = ManagerVars.GetManagerVars();
        Init();
    }

    /// <summary>
    /// 初始化所有List
    /// </summary>
    private void Init()
    {
        for(int i = 0; i < initSpawnCount; i++)
        {
             InstantiateObject(vars.normalPlatform, ref normalPlatformList);
        }

        for(int i = 0; i < initSpawnCount; i++)
        {
            for(int j = 0; j < vars.commonPlatform.Count;j++)
            {
                InstantiateObject(vars.commonPlatform[j], ref commonPlatformList);
            }
        }

        for (int i = 0; i < initSpawnCount; i++)
        {
            for (int j = 0; j < vars.grassPlatform.Count; j++)
            {
                InstantiateObject(vars.grassPlatform[j], ref grassPlatformList);
            }
        }

        for (int i = 0; i < initSpawnCount; i++)
        {
            for (int j = 0; j < vars.winterPlatform.Count; j++)
            {
                InstantiateObject(vars.winterPlatform[j], ref winterPlatformList);
            }
        }

        for (int i = 0; i < initSpawnCount; i++)
        {
            for (int j = 0; j < vars.spikePlatform.Count; j++)
            {
                InstantiateObject(vars.spikePlatform[j], ref spikePlatformList);
            }
        }

        for(int i = 0; i < initSpawnCount; i++)
        {
            InstantiateObject(vars.deathEffect, ref deathEffectList);
        }

        for(int i = 0; i < initSpawnCount; i++)
        {
            InstantiateObject(vars.diamond, ref diamondList);
        }
    }

    /// <summary>
    /// 生成制定平台并加入list
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="addList"></param>
    /// <returns></returns>
    private GameObject InstantiateObject(GameObject prefab,ref List<GameObject> addList)
    {
        GameObject go = Instantiate(prefab, transform);
        addList.Add(go);
        go.SetActive(false);
        return go;
    }

    /// <summary>
    /// 获取普通平台
    /// </summary>
    /// <returns></returns>
    public GameObject GetNormalPlatform()
    {
        for(int i = 0; i < normalPlatformList.Count; i++)
        {
            //如果list中有隐藏的物体,返回该物体
            if(normalPlatformList[i].activeInHierarchy == false)
            {
                return normalPlatformList[i];
            }
        }
        //如果list中的物体都被使用,重新创建一个物体加入list并取得该物体
        return InstantiateObject(vars.normalPlatform, ref normalPlatformList);
    }

    /// <summary>
    /// 获取普通组合平台
    /// </summary>
    /// <returns></returns>
    public GameObject GetCommonPlatformGroup()
    {
        for(int i = 0; i < commonPlatformList.Count; i++)
        {
            if(commonPlatformList[i].activeInHierarchy == false)
            {
                return commonPlatformList[i];
            }
        }
        int random = Random.Range(0, vars.commonPlatform.Count);
        return InstantiateObject(vars.commonPlatform[random], ref commonPlatformList);
    }

    /// <summary>
    /// 获取草地组合平台
    /// </summary>
    /// <returns></returns>
    public GameObject GetGrassPlatformGroup()
    {
        for (int i = 0; i < grassPlatformList.Count; i++)
        {
            if (grassPlatformList[i].activeInHierarchy == false)
            {
                return grassPlatformList[i];
            }
        }
        int random = Random.Range(0, vars.grassPlatform.Count);
        return InstantiateObject(vars.grassPlatform[random], ref grassPlatformList);
    }

    /// <summary>
    /// 获取冬季组合平台
    /// </summary>
    /// <returns></returns>
    public GameObject GetWinterPlatformGroup()
    {
        for (int i = 0; i < winterPlatformList.Count; i++)
        {
            if (winterPlatformList[i].activeInHierarchy == false)
            {
                return winterPlatformList[i];
            }
        }
        int random = Random.Range(0, vars.winterPlatform.Count);
        return InstantiateObject(vars.winterPlatform[random], ref winterPlatformList);
    }

    /// <summary>
    /// 获取钉子组合平台
    /// </summary>
    /// <returns></returns>
    public GameObject GetSpikePlatformGroup()
    {
        for (int i = 0; i < spikePlatformList.Count; i++)
        {
            if (spikePlatformList[i].activeInHierarchy == false)
            {
                return spikePlatformList[i];
            }
        }
        int random = Random.Range(0, vars.spikePlatform.Count);
        return InstantiateObject(vars.spikePlatform[random], ref spikePlatformList);
    }

    /// <summary>
    /// 获取死亡特效
    /// </summary>
    /// <returns></returns>
    public GameObject GetDeathEffect()
    {
        for(int i = 0; i < deathEffectList.Count; i++)
        {
            if(deathEffectList[i].activeInHierarchy == false)
            {
                return deathEffectList[i];
            }
        }
        return InstantiateObject(vars.deathEffect, ref deathEffectList);
    }

    /// <summary>
    /// 获取钻石
    /// </summary>
    /// <returns></returns>
    public GameObject GetDiamond()
    {
        for (int i = 0; i < diamondList.Count; i++)
        {
            if (diamondList[i].activeInHierarchy == false)
            {
                return diamondList[i];
            }
        }
        return InstantiateObject(vars.diamond, ref diamondList);
    }


}
