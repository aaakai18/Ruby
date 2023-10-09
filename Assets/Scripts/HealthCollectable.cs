using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    //记录碰撞次数
    int collideCount = 0;

    //草莓增加的血量
    public int amount = 1;

    //声明一个公开的音频剪辑变量，用来在unity inspectior 中挂接要播放的音频
    public AudioClip collectClip;

    //播放音频声音大小
    public float soundVol = 1.0f;

    public ParticleSystem cureEffect;
    //添加碰撞触发器事件，每次碰撞触发器时执行代码
    private void OnTriggerEnter2D(Collider2D other)
    {
        collideCount++;
       
        //获取Ruby游戏对象的脚本对象
        RubyController rubyController = other.GetComponent<RubyController>();

        if (rubyController != null)
        {
            //判断当前生命值是否是满的，如果是满的则不吃草莓
            if (rubyController.health < rubyController.maxHealth)

            { 
                //更改生命值
                rubyController.ChangeHealth(amount);

                //销毁当前游戏对象，草莓吃掉消失
                Destroy(gameObject);

                //播放吃血包时音频
                rubyController.PlaySound(collectClip,soundVol);


               // Instantiate(cureEffect, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("当前生命值已满");
            }
        }
        else
        {

        }

       

    }
}
