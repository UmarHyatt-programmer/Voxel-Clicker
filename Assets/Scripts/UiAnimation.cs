using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UiAnimation : MonoBehaviour
{
    public static UiAnimation instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    Vector2 initPos;
    public Vector2 deltaVector2;
    public enum AnimType
    {
        none,
        scale,
        panelPop,
        slide,
        SlideRepeat
    }
    public AnimType type;
    public float duration;
    public bool snapping;
    private void Start()
    {
        if (type == AnimType.scale)
        {
            transform.GetComponent<RectTransform>().DOScale(deltaVector2, duration).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        }
    }
    public void OnEnable()
    {
        PopPanel();
        SlideObject();
        SlideRepeat();
    }
    private void OnDisable()
    {
        PopOffPanel();
        SlideOffObject();
    }
    public void PopPanel()
    {
        if (type == AnimType.panelPop)
        {
            initPos = transform.GetComponent<RectTransform>().localScale;
            transform.GetComponent<RectTransform>().DOScale(deltaVector2, duration).SetUpdate(true).SetEase(Ease.OutElastic);
        }
    }
    public void PopOffPanel()
    {
        if (type == AnimType.panelPop)
        {
            GetComponent<RectTransform>().localScale = initPos;
        }
    }
    public void SlideObject()
    {
        if (type == AnimType.slide)
        {
            initPos = transform.GetComponent<RectTransform>().anchoredPosition;
            transform.GetComponent<RectTransform>().DOAnchorPos(deltaVector2, duration).SetUpdate(true);
        }
    }
    public void SlideOffObject()
    {
        if (type == AnimType.slide)
        {
            GetComponent<RectTransform>().anchoredPosition = initPos;
        }
    }
    public void SlideRepeat()
    {
        if(type == AnimType.SlideRepeat)
        {
            transform.GetComponent<RectTransform>().DOAnchorPos(deltaVector2,duration).SetLoops(-1,LoopType.Yoyo).SetUpdate(true);
        }
    }
}
