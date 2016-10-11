using UnityEngine;
using System.Collections;

public class MessageBox : MonoBehaviour
{
  public enum EType
  {
    OK,
    OK_CANCEL,
    YES_NO,
  };

  public delegate void CallbackYes();
  public delegate void CallbackNo ();

  private GameObject  m_dialog;
  private CallbackYes m_listenerYes;
  private CallbackNo  m_listenerNo;
  private EType       m_eType;



  public static void Create(Canvas pParent, string strMessage, EType eType, CallbackYes delYes = null, CallbackNo delNo = null)
  {
    GUI gui = GUI.Instance();

    if (!gui.m_dlgMessageBox)
      return;

    GameObject obj = GameObject.Instantiate<GameObject>(gui.m_dlgMessageBox);
    obj.transform.SetParent(pParent.transform, false);
    MessageBox msgBox = obj.GetComponentInChildren<MessageBox>();
    if (!msgBox)
      return;

    msgBox.m_dialog = obj;
    UnityEngine.UI.Text txt = msgBox.GetComponentInChildren<UnityEngine.UI.Text>();
    txt.text = strMessage;

    if (delYes != null)
      msgBox.m_listenerYes  = delYes;
    if (delNo != null)
      msgBox.m_listenerNo   = delNo;
  }

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void OnYes()
  {
    if (m_listenerYes != null)
      m_listenerYes();

    Destroy(m_dialog);
  }

  public void OnNo()
  {
    if (m_listenerNo != null)
      m_listenerNo();
    Destroy(m_dialog);
  }
}
