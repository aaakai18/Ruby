using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //创建公有静态成员，获取当前血条本身，也就是一个静态的血条实例对象
    public static UIHealthBar Instance { get; private set; }
    //创建图形对象Mask
    public Image mask;
    //记录遮罩层初始长度
    float originalSize;
    // Start is called before the first frame update

    private void Awake()
    {
        //设置静态实例为当前类对象
        Instance = this;
    }
    void Start()
    {
        //获取遮罩层图像初始宽度
        originalSize = mask.rectTransform.rect.width;
        
    }

    //创建一个方法用来设置现在的mask遮罩层宽度
    public void SetValue(float value)
    {
        //设置更改mask遮罩层的宽度，依据传过来的参数进行更改
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,originalSize*value);
    }

  
}
