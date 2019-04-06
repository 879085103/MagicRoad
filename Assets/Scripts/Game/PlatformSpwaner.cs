using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformGroupType
{
    Grass,
    Winter
}


public class PlatformSpwaner : MonoBehaviour {

    //平台初始位置
    public Vector3 startSpawnPos;
    //里程碑
    public int mileStoneCount = 10;
    //平台掉落时间
    public float fallTime;
    //最小掉落时间
    public float minFallTime;
    //时间改变时乘的系数
    public float multiple;

    //每波生成的平台数
    private int spawnPlatformCount;
    private ManagerVars vars;
    //下一个要生成的平台的位置
    private Vector3 platformSpawnPos;
    //平台是否向左生成
    private bool isLeftSpawn = false;

    //选择的平台主题
    private Sprite selectPlatformSprite;
    //组合平台主题
    private PlatformGroupType platformGroupType;

    //钉子方向平台的位置
    private Vector3 spikeDirPlatformPos;
    //钉子方向平台的数目
    private int spikeDirPlatformCount;
    //是否生成钉子平台方向平台
    private bool isSpawnSpike = false;


    void Start()
    {
        //注册生成平台事件
        EventCenter.AddListener(EventDefine.PlatformSpawn, DecidePath);

        platformSpawnPos = startSpawnPos;

        vars = ManagerVars.GetManagerVars();

        //每次游戏开始随机一个平台主题
        RandomPlatformTheme();
        spawnPlatformCount = 5;
        //初始生成5个平台
        for (int i = 0; i < 5;i++)
        {   
            DecidePath();
        }

        //生成角色
        GameObject character = Instantiate(vars.character, transform);
        character.transform.position = new Vector3(0, -1.9f, 0);
    }

    private void Update()
    {
        if(GameManager.Instance.isGameStarted  || GameManager.Instance.isGameOver == false)
        {
            UpdateFallTime();
        }
    }

    /// <summary>
    /// 随机平台主题
    /// </summary>
    private void RandomPlatformTheme()
    { 
        //随机平台主题
        int ranValue = Random.Range(0, vars.platformThemeSpriteList.Count);
        selectPlatformSprite = vars.platformThemeSpriteList[ranValue];  

        //当平台是ice主题时选择,选择冰类型的组合平台
        if(ranValue == 2)
        {
            platformGroupType = PlatformGroupType.Winter;
        }
        //其他选择草类型的组合平台
        else
        {
            platformGroupType = PlatformGroupType.Grass;
        }
    }

    /// <summary>
    /// 确定平台生成路径
    /// </summary>
    private void DecidePath()
    {
        if(isSpawnSpike)
        {
            AfterSpawnSpike();
            return;
        }


        if(spawnPlatformCount > 0)
        {
            spawnPlatformCount--;
            SpawnPlatform();
        }
        else
        {
            //反向
            isLeftSpawn = !isLeftSpawn;
            spawnPlatformCount = Random.Range(1, 4);
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        //生成单个平台
        if(spawnPlatformCount >= 1)
        {
            SpawnNormalPlatform();
        }
        //生成组合平台
        else if (spawnPlatformCount == 0)
        {
            int ran = Random.Range(0, 3);
            //生成通用组合平台
            if (ran == 0)
            {
                SpawnCommonPlatformGroup();
            }
            //生成主题组合平台
            else if(ran == 1)
            {
                switch (platformGroupType)
                {
                    case PlatformGroupType.Grass:
                        SpawnGrassPlatformGroup();
                        break;
                    case PlatformGroupType.Winter:
                        SpawnWinterPlatformGroup();
                        break;
                    default:
                        break;
                }
                
            }
            //生成钉子组合平台
            else
            {
                SpawnSpikePlatformGroup();

                isSpawnSpike = true;
                spawnPlatformCount = 4;
                //确定钉子方向平台的位置
                float xPos = isLeftSpawn ? platformSpawnPos.x + 1.65f : platformSpawnPos.x - 1.65f;
                spikeDirPlatformPos = new Vector3(xPos, platformSpawnPos.y + vars.nextYPos, 0);
            }
        }

        int ranSpawnDiamond = Random.Range(0, 10);
        if(ranSpawnDiamond == 6 && GameManager.Instance.isPlayerMove)
        {
            GameObject go = ObjectPool.Instance.GetDiamond();
            go.transform.position = new Vector3(platformSpawnPos.x, platformSpawnPos.y + 0.5f, 0);
            go.SetActive(true);
        }

        //每次生成完之后更新位置
        platformSpawnPos.x = isLeftSpawn ? platformSpawnPos.x - vars.nextXPos : platformSpawnPos.x + vars.nextXPos;
        platformSpawnPos.y += vars.nextYPos;
    }

    /// <summary>
    /// 生成普通平台(单个)
    /// </summary>
    private void SpawnNormalPlatform()
    {
        GameObject go = ObjectPool.Instance.GetNormalPlatform();
        go.SetActive(true);
        //z坐标-1时普通平台不遮挡组合平台
        go.transform.position = platformSpawnPos - new Vector3(0,0,1);
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite,fallTime);
            
    }

    //生成普通组合平台
    private void SpawnCommonPlatformGroup()
    {
        GameObject go = ObjectPool.Instance.GetCommonPlatformGroup();
        go.SetActive(true);
        go.transform.position = platformSpawnPos;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite,fallTime,!isLeftSpawn);
        
    }

    //生成草地主题组合平台
    private void SpawnGrassPlatformGroup()
    {
        GameObject go = ObjectPool.Instance.GetGrassPlatformGroup();
        go.SetActive(true);
        go.transform.position = platformSpawnPos;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite,fallTime,!isLeftSpawn);
    }

    //生成冬季主题平台
    private void SpawnWinterPlatformGroup()
    {
        GameObject go = ObjectPool.Instance.GetWinterPlatformGroup();
        go.SetActive(true);
        go.transform.position = platformSpawnPos;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite,fallTime,!isLeftSpawn);
    }

    //生成钉子平台
    private void SpawnSpikePlatformGroup()
    {
        GameObject go = ObjectPool.Instance.GetSpikePlatformGroup();
        go.SetActive(true);
        go.transform.position = platformSpawnPos;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite,fallTime,!isLeftSpawn);
    }

    //实现不等概率随机取值
    //rate数组储存了各个数值的概率
    //total为概率总和
    private int RandRate(int []rate,int total)
    {
        int rand = Random.Range(0, total + 1);
        for(int i = 0; i < rate.Length; i++)
        {
            rand -= rate[i];
            if(rand <= 0)
            {
                return i;
            }
        }
        return 0;
    }

    /// <summary>
    /// 生成钉子平台之后需要生成的平台
    /// 包括原来方向的平台
    /// </summary>
    private void AfterSpawnSpike()
    {
        if(spawnPlatformCount > 0)
        {
            spawnPlatformCount--;
            for(int i = 0; i < 2;i++)
            {
                if(i == 0)//生成原来方向的平台
                {
                    SpawnNormalPlatform();
                    //每次生成完之后更新位置
                    platformSpawnPos.x = isLeftSpawn ? platformSpawnPos.x - vars.nextXPos : platformSpawnPos.x + vars.nextXPos;
                    platformSpawnPos.y += vars.nextYPos;
                }
                else//生成钉子方向的平台
                {
                    GameObject go = ObjectPool.Instance.GetNormalPlatform();
                    go.SetActive(true);
                    //z坐标-1时普通平台不遮挡组合平台
                    go.transform.position = spikeDirPlatformPos - new Vector3(0, 0, 1);
                    go.GetComponent<PlatformScript>().Init(selectPlatformSprite,fallTime);

                    //每次生成完之后更新下一个位置
                    spikeDirPlatformPos.x = isLeftSpawn ? spikeDirPlatformPos.x +  vars.nextXPos : spikeDirPlatformPos.x - vars.nextYPos;
                    spikeDirPlatformPos.y += vars.nextYPos;
                }
            }
        }
        else
        {
            isSpawnSpike = false;
            //如果没有DecidePath(),每次当生成最后一个钉子平台方向的平台时,不会生成新的平台
            DecidePath();
        }
    }

    //更新平台掉落时间
    private void UpdateFallTime()
    {
        if(GameManager.Instance.GetGameScore() > mileStoneCount)
        {
            mileStoneCount *= 2;
            fallTime *= multiple;
            if(fallTime < minFallTime)
            {
                fallTime = minFallTime;
            }
        }
    }
    
    private void OnDestroy()
    {
        //脚本销毁时移除监听
        EventCenter.RemoveListener(EventDefine.PlatformSpawn, DecidePath);
    }
}
