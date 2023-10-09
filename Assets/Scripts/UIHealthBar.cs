using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //�������о�̬��Ա����ȡ��ǰѪ������Ҳ����һ����̬��Ѫ��ʵ������
    public static UIHealthBar Instance { get; private set; }
    //����ͼ�ζ���Mask
    public Image mask;
    //��¼���ֲ��ʼ����
    float originalSize;
    // Start is called before the first frame update

    private void Awake()
    {
        //���þ�̬ʵ��Ϊ��ǰ�����
        Instance = this;
    }
    void Start()
    {
        //��ȡ���ֲ�ͼ���ʼ���
        originalSize = mask.rectTransform.rect.width;
        
    }

    //����һ�����������������ڵ�mask���ֲ���
    public void SetValue(float value)
    {
        //���ø���mask���ֲ�Ŀ�ȣ����ݴ������Ĳ������и���
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,originalSize*value);
    }

  
}
