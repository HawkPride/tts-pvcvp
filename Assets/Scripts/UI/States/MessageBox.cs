using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GUI.States
{
  //Creation params
  public class MessageBoxParams : GameStateParams
  {
    public override string GetSceneName() { return "PopUp"; }
    public override EGameStateType GetStateType() { return EGameStateType.MESSAGE_BOX; }
  }


  public class MessageBox : GameState
  {
    public enum EType
    {
      OK,
      OK_CANCEL,
      YES_NO,
    };

    //Interface
    public Text    m_txtMessage;
    public Button  m_btnYes;
    public Button  m_btnNo;


    //Vars
    private EType         m_eType;

    //-----------------------------------------------------------------------------------
    public static void Create(Canvas pParent, string strMessage, EType eType, UnityAction delYes = null, UnityAction delNo = null)
    {
      var config = Game.Instance.GetConfig();
      if (!config.dlgMessageBox)
        return;

      GameObject obj = GameObject.Instantiate<GameObject>(config.dlgMessageBox);
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


    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.MESSAGE_BOX;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {

    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {


    }

    //-----------------------------------------------------------------------------------
    public void OnButton()
    {
      DeleteGameState();
    }
  }
}