using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    public float speed = 3.0f;

    //�Ƿ�����Y������
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2d;
    float timer;
    int direction = 1;

    Animator animator;

    //��ȡ������Ч
    public ParticleSystem smokeEffect; 

    //�趨boolֵ����ʼֵ��ʾ�����˸տ�ʼ�ǻ���
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
            //�õ�״̬����ֱ���˳����û����˲����ƶ�
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
            //�õ�״̬����ֱ���˳����û����˲����ƶ�
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


    //������ײ�¼�
    //��Ӷ�����˺�
    private void OnCollisionEnter2D(Collision other)
    {
        //
        RubyController rubyController = other.gameObject.GetComponent<RubyController>();
        if(rubyController!=null)
        {
            rubyController.ChangeHealth(-1);
        }

    }

    //�޸������˷���
    public void Fix()
    {
        //����״̬Ϊ���޸�
        broked = false;
        //�������ܱ���ײ��ȡ��������������Ч��
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        //Destroy(smokeEffect);

        //��Чֹͣ�����µ�����,ԭ�е���Ч��ִ����
        smokeEffect.Stop();

        //��ȡ�ͻ����˹ҽӵ�����Դ���������ֹͣ��������
        GetComponent<AudioSource>().Stop();
    }
}
