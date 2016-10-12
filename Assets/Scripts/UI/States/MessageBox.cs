using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;

namespace GUI
{

  public class MessageBox : UILayout
  {
    public enum EType
    {
      OK,
      OK_CANCEL,
      YES_NO,
    };

    //Interface
    public UnityEngine.UI.Text    m_txtMessage;
    public UnityEngine.UI.Button  m_btnYes;
    public UnityEngine.UI.Button  m_btnNo;


    //Vars
    private EType         m_eType;

    //-----------------------------------------------------------------------------------
    public static void Create(Canvas pParent, string strMessage, EType eType, UnityAction delYes = null, UnityAction delNo = null)
    {
      UserInterface gui = UserInterface.Instance();

      if (!gui.m_dlgMessageBox)
        return;

      GameObject obj = GameObject.Instantiate<GameObject>(gui.m_dlgMessageBox);
      obj.transform.SetParent(pParent.transform, false);
      MessageBox msgBox = obj.GetComponentInChildren<MessageBox>();
      if (!msgBox)
        return;
      msgBox.Init(strMessage, eType, delYes, delNo);
    }

    //-----------------------------------------------------------------------------------
    void Init(string strMessage, EType eType, UnityAction delYes, UnityAction delNo)
    {
      if (m_txtMessage != null)
        m_txtMessage.text = strMessage;

      m_eType = eType;

      if (m_btnYes != null)
      {
        if (delYes != null)
          m_btnYes.onClick.AddListener(delYes);
        m_btnYes.onClick.AddListener(OnButton);
      }
      if (m_btnNo != null)
      {
        if (delNo != null)
          m_btnNo.onClick.AddListener(delNo);
        m_btnNo.onClick.AddListener(OnButton);
      }
    }

    // Use this for initialization
    //-----------------------------------------------------------------------------------
    void Start()
    {

    }

    // Update is called once per frame
    //-----------------------------------------------------------------------------------
    void Update()
    {

    }

    //-----------------------------------------------------------------------------------
    public void OnButton()
    {
      DeleteLayout();
    }
  }
}