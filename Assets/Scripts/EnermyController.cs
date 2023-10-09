using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    public float speed = 3.0f;

    //是否沿着Y方向走
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2d;
    float timer;
    int direction = 1;

    Animator animator;

    //获取烟雾特效
    public ParticleSystem smokeEffect; 

    //设定bool值，初始值表示机器人刚开始是坏的
    bool broked = true;
    void Start()
    {

        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!broked)
        {
            //好的状态，则直接退出，让机器人不再移动
            return;
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    private void FixedUpdate()
    {
        if (!broked)
        {
            //好的状态，则直接退出，让机器人不再移动
            return;
        }
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction; ;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }


    //刚体碰撞事件
    //添加对玩家伤害
    private void OnCollisionEnter2D(Collision other)
    {
        //
        RubyController rubyController = other.gameObject.GetComponent<RubyController>();
        if(rubyController!=null)
        {
            rubyController.ChangeHealth(-1);
        }

    }

    //修复机器人方法
    public void Fix()
    {
        //更改状态为已修复
        broked = false;
        //让她不能被碰撞，取消它的物理引擎效果
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        //Destroy(smokeEffect);

        //特效停止产生新的例子,原有的特效会执行完
        smokeEffect.Stop();

        //获取和机器人挂接的声音源组件，让其停止播放声音
        GetComponent<AudioSource>().Stop();
    }
}
