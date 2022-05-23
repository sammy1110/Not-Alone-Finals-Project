using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Image mask;
    float OriginalSize;

    public static UIHealthBar instance { get; private set; }

     void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        OriginalSize = mask.rectTransform.rect.width;
    }

   public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, OriginalSize * value);
    }
}
