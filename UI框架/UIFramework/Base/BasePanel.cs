using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public abstract class BasePanel : MonoBehaviour
{
    public float FadeSpeed=0.3f;
    public Image[] images;
    public Text[] Texts;
    private bool visibe;

    public bool Visibe { get => visibe; set => visibe = value; }

    /// <summary>
    /// 界面显示，进行入栈操作
    /// </summary>
    public virtual void OnEnter()
    {
        Visibe = true;
        ShowPanel();
    }
    /// <summary>
    /// 停止上一个界面
    /// </summary>
    public virtual void OnPause()
    {

    }
    /// <summary>
    /// 恢复上一个界面
    /// </summary>
    public virtual void OnResume()
    {

    }
    /// <summary>
    /// 界面隐藏，进行出栈操作
    /// </summary>
    public virtual void OnExit()
    {
        HidePanel();
    }
    void ShowPanel()
    {
        ShowImages();
        ShowTexts();
    }
    void ShowImages()
    {
        if (images.Length > 0)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].DOFade(0,0f);
                images[i].DOFade(1, FadeSpeed);
            }
        }
    }
    void ShowTexts()
    {
        if (Texts.Length > 0)
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].DOFade(0, 0f);
                Texts[i].DOFade(1, FadeSpeed);
            }
        }
    }

    public void HidePanel()
    {
        HideImage();
        HideText();
    }
    void HideImage()
    {
        if (images.Length > 0)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].DOFade(0, FadeSpeed);
            }
        }
    }
    void HideText()
    {
        if (Texts.Length > 0)
        {
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].DOFade(0, FadeSpeed);
            }
        }
    }
}
