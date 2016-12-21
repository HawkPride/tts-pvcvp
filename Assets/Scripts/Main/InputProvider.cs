using UnityEngine;
using UnityEngine.EventSystems;

public class InputProvider : MonoBehaviour
{
  public enum EInputAction
  {
    NONE = 0,
    ROTATE = 1 << 0,
    LEFT = 1 << 1,
    RIGHT = 1 << 2,
    DOWN = 1 << 3,

    LAST,
  };

  int           m_nCurrInputFlags = 0;
  int           m_nCurrButtonFlags = 0;
  TimeInterval  m_timer;
  bool          m_bLocal = true;

  public delegate void OnNewInputState(int nStateFlags);
  public delegate void OnInputEvent(int nKey, bool bDown);
  public OnNewInputState  m_delNewInputState;
  public OnInputEvent     m_delEvent;

  public float            m_fInputRepeat  = 0.1f;

  public bool local { set { m_bLocal = value; } }

  // Use this for initialization
  //-----------------------------------------------------------------------------------
  void Start()
  {
    m_timer = new TimeInterval(m_fInputRepeat);

    if (m_bLocal)
    {
      GameGUI.UiButtonsToInputProvider translator = FindObjectOfType<GameGUI.UiButtonsToInputProvider>();
      if (translator != null)
        translator.LinkToIp(this);
    }
  }

  // Update is called once per frame
  //-----------------------------------------------------------------------------------
  void Update()
  {
    int nInputFlags = 0;
    if (m_bLocal)
    {
      float fX = Input.GetAxis("Horizontal");
      float fY = Input.GetAxis("Vertical");

      if (fX < 0.0)
        nInputFlags |= (int)EInputAction.LEFT;
      else if (fX > 0.0)
        nInputFlags |= (int)EInputAction.RIGHT;
      if (fY < 0.0)
        nInputFlags |= (int)EInputAction.DOWN;
      else if (fY > 0.0)
        nInputFlags |= (int)EInputAction.ROTATE;
    }

    nInputFlags |= m_nCurrButtonFlags;

    if (m_nCurrInputFlags != nInputFlags)
    {
      if (m_delNewInputState != null)
        m_delNewInputState(nInputFlags);
      m_timer.Reset();
    }
    bool bRepeatOk = m_timer.StartNewInterval();

    for (int nKey = 1; nKey < (int)EInputAction.LAST; nKey = nKey << 1)
    {
      bool bNewState = (nInputFlags & nKey) != 0;
      bool bCurState = (m_nCurrInputFlags & nKey) != 0;
      //State changed
      if (bNewState != bCurState)
      {
        //m_timer.Reset();
        m_delEvent(nKey, bNewState);
      }
      //Send repeat
      else if (bCurState && bRepeatOk
        && nKey != (int)EInputAction.ROTATE)
      {
        m_delEvent(nKey, true);
      }
    }

    m_nCurrInputFlags = nInputFlags;
  }
  

  //-----------------------------------------------------------------------------------
  public void ForceSetFlags(int nFlags)
  {
    m_nCurrButtonFlags = nFlags;
  }

  //-----------------------------------------------------------------------------------
  public void OnInputUiButtonClick(int nFlag)
  {
    /*
    m_nCurrInputFlags |= nFlag;
    m_eventListeners(m_nCurrInputFlags);
    m_nCurrInputFlags &= ~nFlag;
    */
  }

  //-----------------------------------------------------------------------------------
  public void OnInputUiButtonDown(int nFlag)
  {
    m_nCurrButtonFlags |= nFlag;
  }

  //-----------------------------------------------------------------------------------
  public void OnInputUiButtonUp( int nFlag )
  {
    m_nCurrButtonFlags &= ~nFlag;
  }


}
