using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    //��ӶԻ�����ʾʱ��
    public float displayTime = 4.0f;
    public GameObject dialogBox;//�Ի�����Ϸ����
    float timerDisplay;//��ʱ��

    //������Ϸ�����ȡTMP�ؼ�
    public GameObject dlgTxtProGameObject;
    //������Ϸ��������
    TextMeshProUGUI tmTxtBox;

    //���ñ����洢��ǰҳ��
    int _currentPage = 1;
    //������ҳ��
    int _totalPages;

    // Start is called before the first frame update
    void Start()
    {
        //��Ϸ��ʼ����ʾ�Ի���
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;//��ʱ���տ�ʼʱ������

        //��ȡ�Ի������
        tmTxtBox = dlgTxtProGameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //��ȡ�Ի�������У��Ի����ֵ����ҳ����start�����л�ȡ������
        _totalPages = tmTxtBox.textInfo.pageCount;
        if(timerDisplay>=0.0f)
        {
            //��ҳ����
            //����û����룬ÿ��space������ʱ����
            if(Input.GetKeyUp(KeyCode.Space))
            {
                if(_currentPage<_totalPages)
                {
                    _currentPage++;
                }
                else
                {
                    _currentPage = 1;
                }
                
                //��ʾ�ڼ�ҳ
                tmTxtBox.pageToDisplay = _currentPage;
            }

            timerDisplay -= Time.deltaTime;
        }

        else
        {
            dialogBox.SetActive(false);
        }
    }

    public void DisplayDialog()
    {

        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
