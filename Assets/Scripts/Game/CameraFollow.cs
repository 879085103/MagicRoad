using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform target;
    //偏移量
    private Vector3 offset;
    private Vector2 velocity;

	void Start () {
		
	}
	
	
	void Update () {
        //当target目标未赋值且场景里能找到标签为character的物体时
		if(target == null && GameObject.FindGameObjectWithTag("Character"))
        {
            target = GameObject.FindGameObjectWithTag("Character").transform;
            offset = target.position  - this.transform.position;
        }

        if(target != null)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, target.position.x - offset.x, ref velocity.x, 0.05f);
            float posY = Mathf.SmoothDamp(transform.position.y, target.position.y - offset.y, ref velocity.y, 0.05f);
            //如果要到达的位置大于相机当前位置
            if(posY > transform.position.y)
            {
                transform.position = new Vector3(posX, posY, transform.position.z);
            }
           
            //transform.position = Vector3.Lerp(transform.position, target.position - offset,0.05f);
        }
	}
}
