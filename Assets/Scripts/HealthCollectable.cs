using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    //��¼��ײ����
    int collideCount = 0;

    //��ݮ���ӵ�Ѫ��
    public int amount = 1;

    //����һ����������Ƶ����������������unity inspectior �йҽ�Ҫ���ŵ���Ƶ
    public AudioClip collectClip;

    //������Ƶ������С
    public float soundVol = 1.0f;

    public ParticleSystem cureEffect;
    //�����ײ�������¼���ÿ����ײ������ʱִ�д���
    private void OnTriggerEnter2D(Collider2D other)
    {
        collideCount++;
       
        //��ȡRuby��Ϸ����Ľű�����
        RubyController rubyController = other.GetComponent<RubyController>();

        if (rubyController != null)
        {
            //�жϵ�ǰ����ֵ�Ƿ������ģ�����������򲻳Բ�ݮ
            if (rubyController.health < rubyController.maxHealth)

            { 
                //��������ֵ
                rubyController.ChangeHealth(amount);

                //���ٵ�ǰ��Ϸ���󣬲�ݮ�Ե���ʧ
                Destroy(gameObject);

                //���ų�Ѫ��ʱ��Ƶ
                rubyController.PlaySound(collectClip,soundVol);


               // Instantiate(cureEffect, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("��ǰ����ֵ����");
            }
        }
        else
        {

        }

       

    }
}
