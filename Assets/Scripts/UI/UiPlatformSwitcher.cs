using UnityEngine;
using System.Collections;

public class UiPlatformSwitcher : MonoBehaviour
{
  public bool m_bAndroid  = false;
  public bool m_bIos      = false;
  public bool m_bWin      = false;
  public bool m_bMac      = false;
  public bool m_bEditor   = false;

  // Use this for initialization
  void Start()
  {
    bool bShow = false;
#if UNITY_ANDROID
    if (m_bAndroid)
      bShow = true;
#elif UNITY_IOS
    if (m_bIos)
      bShow = true;
#elif UNITY_STANDALONE_WIN
    if (m_bWin)
      bShow = true;
#elif UNITY_STANDALONE_OSX
    if (m_bMac)
      bShow = true;
#elif UNITY_EDITOR
    if (m_bEditor)
      bShow = true;
#endif
    Show(bShow);
  }
  
  void Show(bool bFlag)
  {
    Canvas obj = GetComponent<Canvas>();
    if (obj)
      obj.enabled = bFlag;
  }
}
