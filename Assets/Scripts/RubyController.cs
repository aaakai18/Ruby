using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    //����һ����������Ƶ����������������unity inspectior �йҽ�Ҫ���ŵ���Ƶ
    public AudioClip throwCogClip;

    //������Ƶ������С
    public float throwCogsoundVol = 1.0f;

    //����һ����������Ƶ����������������unity inspectior �йҽ�Ҫ���ŵ���Ƶ
    public AudioClip playerHitClip;

    //������Ƶ������С
    public float playerHitsoundVol = 1.0f;


    //������ƵԬ�����������ڽ�����Ƶ���ſ���
    AudioSource audioSource;

    private float speed = 3.0f;
    //�����������
    Rigidbody2D rigidbody2D;
    
    //��ȡ�û�����
    float horizontal;
    float vertical;

    //�������ֵ
    public int maxHealth = 5;

    //����һ���������������
    Animator animator;

    //C# �еķ�װ���������ݳ�Ա
    //���ݳ�ԱĬ��Ϊ˽�У�ֻ��ͨ����ǰ��ķ����������Խ��з���
    //�����ǹ��еģ�����ͨ��ȡֵ��get����ֵ��set�趨��Ӧ�ֶεķ��ʹ������������ܹ�����
    //����һ������
    public int health { 
        get { return currentHealth; }
      //  set { currentHealth = value}
    }
     int currentHealth;

   //��������޵м��ʱ��
    public float timeInvincible = 2.0f;

    //�����Ƿ��޵�
    bool isInvincible;

    //��������������޵�ʱ��ļ�ʱ
    float invincibleTimer;

    //Ruby��ֹ����ʱ���ŵĶ�������
    Vector2 lookDirection = new Vector2(1, 0);

    //�ƶ���ά����
    Vector2 move;

    public GameObject projectilePrefab;

    
    void Start()
    {
        //����֡��Ϊ10֡
        //��Ϸ�Ǻ���ˢ�£��˴������ô�ֱͬ��Ϊ0����ֱͬ������������������Ϸ�����˺�ѸУ���֡��������Ϊ0��������֡��Ч
        //QualitySettings.vSyncCount = 0;
        //����֡��Ϊ10
        //  Application.targetFrameRate = 30;


        //��ȡ��ǰ��Ϸ����ĸ������
        rigidbody2D = GetComponent<Rigidbody2D>();

        //��ʼ����ǰ����ֵ
        currentHealth = maxHealth-1;

        //��ȡ�������
        animator = GetComponent<Animator>();

        //��ȡ����Դ�������
        audioSource = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
       

        //�жϽ�ɫ�Ƿ����޵�״̬������޵���ʼ���е���ʱ
        if(isInvincible)
        {
            invincibleTimer = invincibleTimer - Time.deltaTime;
            if(invincibleTimer<0)
            {
                isInvincible = false;
            }
        }

        move = new Vector2(horizontal, vertical);
        
        //�����������ƶ�
        if(!Mathf.Approximately(move.x,0.0f)||!Mathf.Approximately(move.y,0.0f))
        {
            //��ruby�����ƶ�����
            lookDirection.Set(move.x, move.y);

            //�÷�����������Ϊ��λ������ֻ��ʾ���򣬲���ʾ����λ��
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);

        //����Ruby�ٶȸ�blend tree
        //ʸ����magnitude���ԣ���������ʸ���ĳ��ȣ���һ������ֵ
        animator.SetFloat("Speed", move.magnitude);

        //��ӷ����ӵ�����ȡ����
        if(Input.GetKeyDown(KeyCode.C)||Input.GetAxis("Fire1")!=0)
        {
            Launch();
        }

        //����һ��������ײ��������������Ͷ����ײ��Ϣ

        if(Input.GetKeyDown(KeyCode.X))
        {
            //����1������Ͷ��λ��
            //����2��Ͷ�䷽��
            //����3��Ͷ�����
            //����4��Ͷ����Ч�Ĳ�
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

            if(hit.collider!=null)
            {
                Debug.Log($"������ײ�Ķ����ǣ�{hit.collider.gameObject}");

                //����npc�������
                NPC npc = hit.collider.GetComponent<NPC>();
                if(npc!=null)
                {
                    //����NPC�����ʾ�Ի��򷽷�
                    npc.DisplayDialog();
                }
            }
        }
    }
    //�̶�ʱ����ִ�еĸ��·���,ˢ��Ƶ�ʱ�Update����Ҫ��,0.02sִ��һ��
    private void FixedUpdate()
    {
        
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2D.position = position;
        

    }

    public void ChangeHealth(int amount)
    {
        //����������˺�ʱ����Ϊ2s
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }

            //����������޵У��ܵ����˺�����ı�Ϊ�޵�״̬ʹ��Ҳ����˺�����ʱ����ʼ��ʱ
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");

            //������Ѫ��Ƶ
            PlaySound(playerHitClip, playerHitsoundVol);

        }
       

        //�趨ÿ�μӼ�����ֵ�������޶ȣ����ߺ����� [0,maxHealth]
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.Instance.SetValue(currentHealth / (float)maxHealth);
       
    }

    //
    void Launch()
    {
        //�����ӵ���Ϸ����
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        //��ȡ�ӵ���Ϸ����Ľű����
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection,300);
        animator.SetTrigger("Launch");
        //������Ƶ
        PlaySound(throwCogClip, throwCogsoundVol);
        

    }

    //����һ�����з�����һ�����ã��Ქ��ָ������Ƶ
    public void PlaySound(AudioClip audioClip,float soundVol)
    {
        audioSource.PlayOneShot(audioClip);

    }

    
}
