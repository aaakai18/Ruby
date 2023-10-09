using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    //添加对话框显示时长
    public float displayTime = 4.0f;
    public GameObject dialogBox;//对话框游戏对象
    float timerDisplay;//计时器

    //创建游戏对象获取TMP控件
    public GameObject dlgTxtProGameObject;
    //创建游戏组件类对象
    TextMeshProUGUI tmTxtBox;

    //设置变量存储当前页数
    int _currentPage = 1;
    //设置总页数
    int _totalPages;

    // Start is called before the first frame update
    void Start()
    {
        //游戏开始不显示对话框
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;//计时器刚开始时不可用

        //获取对话框组件
        tmTxtBox = dlgTxtProGameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //获取对话框组件中，对话文字的最大页数（start方法中获取不到）
        _totalPages = tmTxtBox.textInfo.pageCount;
        if(timerDisplay>=0.0f)
        {
            //翻页功能
            //检测用户输入，每次space键弹起时激活
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
                
                //显示第几页
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
