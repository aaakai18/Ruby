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
        //���û����������������г���100ʱ�Զ�����
        if (transform.position.magnitude > 100)
            Destroy(gameObject);
    }

    //���䵱ǰ�ӵ�
    public void Launch(Vector2 direction,float force)
    {
        //ͨ����������������ϵͳ��addforce����������Ϸ����ʩ��һ������ʹ���ƶ�
        rigidbody2D.AddForce(direction*force);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //��ȡ�ӵ���ײ���Ļ����˶���ű�
        EnermyController enermyController = collision.collider.GetComponent<EnermyController>();
        if(enermyController!=null)
        {
            enermyController.Fix();
        }

        Debug.Log($"�ӵ������ˣ�{collision.gameObject}");

        //ʵ������Ч��λ������ײ��λ�ã�����ת
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        
        //��ײ���ӵ�����
        Destroy(gameObject);

    }
}
