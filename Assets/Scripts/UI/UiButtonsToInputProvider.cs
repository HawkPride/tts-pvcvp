using UnityEngine;
using UnityEngine.EventSystems;

namespace GameGUI
{
  public class UiButtonsToInputProvider : MonoBehaviour
  {

    //Buttons
    public EventTrigger   m_btnUp     = null;
    public EventTrigger   m_btnDown   = null;
    public EventTrigger   m_btnLeft   = null;
    public EventTrigger   m_btnRight  = null;

    //-----------------------------------------------------------------------------------
    public void LinkToIp(InputProvider ip)
    {
      //Add callbacks to button
      RegisterButton(ip, m_btnUp, InputProvider.EInputAction.ROTATE);
      RegisterButton(ip, m_btnDown, InputProvider.EInputAction.DOWN);
      RegisterButton(ip, m_btnLeft, InputProvider.EInputAction.LEFT);
      RegisterButton(ip, m_btnRight, InputProvider.EInputAction.RIGHT);
    }


    //-----------------------------------------------------------------------------------
    void RegisterButton(InputProvider ip, EventTrigger btn, InputProvider.EInputAction eFlag)
    {
      if (btn == null)
        return;
      EventTrigger.Entry down = new EventTrigger.Entry();
      down.eventID = EventTriggerType.PointerDown;
      down.callback.AddListener((eventData) => { ip.OnInputUiButtonDown((int)eFlag); });
      btn.triggers.Add(down);
      EventTrigger.Entry up = new EventTrigger.Entry();
      up.eventID = EventTriggerType.PointerUp;
      up.callback.AddListener((eventData) => { ip.OnInputUiButtonUp((int)eFlag); });
      btn.triggers.Add(up);
    }
  }
}
