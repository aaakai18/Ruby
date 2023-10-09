using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    //���˺�����Ѫ��
    public int damageNum = -3;
    private void OnTriggerStay2D(Collider2D other)
    {
        RubyController rubyController = other.GetComponent<RubyController>();
        if(rubyController!=null)
        {
            rubyController.ChangeHealth(damageNum);
        }
    }
}
