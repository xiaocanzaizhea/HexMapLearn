using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private float alphaSpeed = 4;
    public bool IsShow => isShow;
    protected bool isShow = false;

    private UnityAction hideCallback;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        Init();
    }

    public abstract void Init();

    public virtual void Show()
    {
        isShow = true;
        canvasGroup.alpha = 0;
        var gaussianBlur = GameManager.Instance.MainCamera.GetComponent<GaussianBlur>();
        if (gaussianBlur != null)
        {
            gaussianBlur.enabled = true;
            gaussianBlur.blurSize = BlurSize();
        }
    }

    public virtual void Hide(UnityAction callback)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        hideCallback = callback;
        var gaussianBlur = GameManager.Instance.MainCamera.GetComponent<GaussianBlur>();
        if (gaussianBlur != null)
        {
            gaussianBlur.enabled = false;
            gaussianBlur.blurSize = 0;
        }
    }

    public void SetAlphaSpeed(float speed) 
    {
        alphaSpeed = speed;
    }

    protected virtual void Update()
    {
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.unscaledDeltaTime;
            if (canvasGroup.alpha > 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        else if (!isShow)
        {
            canvasGroup.alpha -= alphaSpeed * Time.unscaledDeltaTime;
            if (canvasGroup.alpha <= 0)
            {
                hideCallback?.Invoke();
            }
        }
    }

    protected virtual int BlurSize()
    {
        return 3;
    }
}
