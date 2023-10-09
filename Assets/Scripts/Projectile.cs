using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    public ParticleSystem hitEffect;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //如果没有碰到东西，则飞行超过100时自动销毁
        if (transform.position.magnitude > 100)
            Destroy(gameObject);
    }

    //发射当前子弹
    public void Launch(Vector2 direction,float force)
    {
        //通过刚体对象调用物理系统的addforce方法，给游戏对象施加一个力，使其移动
        rigidbody2D.AddForce(direction*force);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //获取子弹碰撞到的机器人对象脚本
        EnermyController enermyController = collision.collider.GetComponent<EnermyController>();
        if(enermyController!=null)
        {
            enermyController.Fix();
        }

        Debug.Log($"子弹碰到了：{collision.gameObject}");

        //实例化特效，位置是碰撞的位置，不旋转
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        
        //碰撞后子弹销毁
        Destroy(gameObject);

    }
}
