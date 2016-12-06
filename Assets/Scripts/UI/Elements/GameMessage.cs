using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;


namespace GameGUI
{

  public class GameMessage : MonoBehaviour
  {
    public Text       m_txtText;

    UnityEvent    m_endCallback = new UnityEvent();
    TimeInterval  m_timerShow;

    //-----------------------------------------------------------------------------------
    public static void Create(Canvas pParent, string strMessage, UnityAction delEndCallback = null, float fShowTime = 3.0f)
    {
      var config = Game.Instance.GetConfig();
      if (!config.gameMessage)
        return;

      GameObject obj = GameObject.Instantiate<GameObject>(config.gameMessage);
      obj.transform.SetParent(pParent.transform, false);
      GameMessage msg = obj.GetComponentInChildren<GameMessage>();
      if (!msg)
        return;
      msg.Init(strMessage, delEndCallback, fShowTime);
    }

    //-----------------------------------------------------------------------------------
    void Init(string strMessage, UnityAction delEndCallback, float fShowTime)
    {
      if (m_txtText != null)
        m_txtText.text = strMessage;

      m_timerShow = new TimeInterval(fShowTime);

      if (delEndCallback != null)
        m_endCallback.AddListener(delEndCallback);
    }


    //-----------------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {

    }

    //-----------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
      if (m_timerShow.IsIntervalPassed())
      {
        m_endCallback.Invoke();
        Destroy(gameObject);
      }
    }
  }

}