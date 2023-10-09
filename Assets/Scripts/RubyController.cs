using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    //声明一个公开的音频剪辑变量，用来在unity inspectior 中挂接要播放的音频
    public AudioClip throwCogClip;

    //播放音频声音大小
    public float throwCogsoundVol = 1.0f;

    //声明一个公开的音频剪辑变量，用来在unity inspectior 中挂接要播放的音频
    public AudioClip playerHitClip;

    //播放音频声音大小
    public float playerHitsoundVol = 1.0f;


    //声明音频袁对象，用来后期进行音频播放控制
    AudioSource audioSource;

    private float speed = 3.0f;
    //声明刚体对象
    Rigidbody2D rigidbody2D;
    
    //获取用户输入
    float horizontal;
    float vertical;

    //最大生命值
    public int maxHealth = 5;

    //声明一个动画控制器组件
    Animator animator;

    //C# 中的封装，保护数据成员
    //数据成员默认为私有，只能通过当前类的方法或者属性进行访问
    //属性是公有的，可以通过取值器get，赋值器set设定对应字段的访问规则，满足规则才能够访问
    //这是一个属性
    public int health { 
        get { return currentHealth; }
      //  set { currentHealth = value}
    }
     int currentHealth;

   //设置玩家无敌间隔时间
    public float timeInvincible = 2.0f;

    //设置是否无敌
    bool isInvincible;

    //定义变量，进行无敌时间的计时
    float invincibleTimer;

    //Ruby静止不动时播放的动画参数
    Vector2 lookDirection = new Vector2(1, 0);

    //移动二维向量
    Vector2 move;

    public GameObject projectilePrefab;

    
    void Start()
    {
        //锁定帧率为10帧
        //游戏是横向刷新，此代码设置垂直同步为0，垂直同步作用是显著减少游戏画面的撕裂感，跳帧。必须设为0，否则锁帧无效
        //QualitySettings.vSyncCount = 0;
        //设置帧率为10
        //  Application.targetFrameRate = 30;


        //获取当前游戏对象的刚体组件
        rigidbody2D = GetComponent<Rigidbody2D>();

        //初始化当前生命值
        currentHealth = maxHealth-1;

        //获取组件对象
        animator = GetComponent<Animator>();

        //获取声音源组件对象
        audioSource = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
       

        //判断角色是否处于无敌状态，如果无敌则开始进行倒计时
        if(isInvincible)
        {
            invincibleTimer = invincibleTimer - Time.deltaTime;
            if(invincibleTimer<0)
            {
                isInvincible = false;
            }
        }

        move = new Vector2(horizontal, vertical);
        
        //如果玩家正在移动
        if(!Mathf.Approximately(move.x,0.0f)||!Mathf.Approximately(move.y,0.0f))
        {
            //将ruby朝向移动方向
            lookDirection.Set(move.x, move.y);

            //该方法将向量变为单位向量，只表示方向，不表示它的位置
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);

        //传递Ruby速度给blend tree
        //矢量的magnitude属性，用来返回矢量的长度，是一个绝对值
        animator.SetFloat("Speed", move.magnitude);

        //添加发射子弹，获取输入
        if(Input.GetKeyDown(KeyCode.C)||Input.GetAxis("Fire1")!=0)
        {
            Launch();
        }

        //创建一个射线碰撞对象来接受射手投射碰撞信息

        if(Input.GetKeyDown(KeyCode.X))
        {
            //参数1：射线投射位置
            //参数2：投射方向
            //参数3：投射距离
            //参数4：投射生效的层
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

            if(hit.collider!=null)
            {
                Debug.Log($"射线碰撞的对象是：{hit.collider.gameObject}");

                //创建npc代码组件
                NPC npc = hit.collider.GetComponent<NPC>();
                if(npc!=null)
                {
                    //调用NPC组件显示对话框方法
                    npc.DisplayDialog();
                }
            }
        }
    }
    //固定时间间隔执行的更新方法,刷新频率比Update函数要慢,0.02s执行一次
    private void FixedUpdate()
    {
        
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2D.position = position;
        

    }

    public void ChangeHealth(int amount)
    {
        //假设玩家受伤害时间间隔为2s
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }

            //如果不处于无敌，受到了伤害，则改变为无敌状态使玩家不受伤害，计时器开始计时
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");

            //播放伤血音频
            PlaySound(playerHitClip, playerHitsoundVol);

        }
       

        //设定每次加减生命值后的最大限度，上线和下线 [0,maxHealth]
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.Instance.SetValue(currentHealth / (float)maxHealth);
       
    }

    //
    void Launch()
    {
        //创建子弹游戏对象
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        //获取子弹游戏对象的脚本组件
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection,300);
        animator.SetTrigger("Launch");
        //播放音频
        PlaySound(throwCogClip, throwCogsoundVol);
        

    }

    //新增一个公有方法，一经调用，会播放指定的音频
    public void PlaySound(AudioClip audioClip,float soundVol)
    {
        audioSource.PlayOneShot(audioClip);

    }

    
}
