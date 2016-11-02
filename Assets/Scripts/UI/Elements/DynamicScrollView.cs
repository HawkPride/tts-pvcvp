﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GUI
{
  public class DynamicScrollView : MonoBehaviour
  {

    public GameObject m_listItem = null;


    ScrollRect m_scrollRect;


    // Use this for initialization
    void Start()
    {
      m_scrollRect = GetComponent<ScrollRect>();
      if (m_scrollRect)
        m_scrollRect.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual GameObject AddListItem()
    {
      if (!m_scrollRect)
        return null;
      RectTransform content = m_scrollRect.content;
      float fCurrHeight = content.rect.height;
    
      GameObject newObj = GameObject.Instantiate(m_listItem);

      RectTransform rt = newObj.GetComponent<RectTransform>();
      rt.SetParent(content, false);
      rt.anchoredPosition = new Vector3(0, -fCurrHeight, 0);
      fCurrHeight += rt.rect.height;
      content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fCurrHeight);

      //Scroll for 1/3 of each child
      float fScrollDelta = fCurrHeight / content.childCount * 0.33f;
      m_scrollRect.scrollSensitivity = fScrollDelta;
      return newObj;
    }

  }
}