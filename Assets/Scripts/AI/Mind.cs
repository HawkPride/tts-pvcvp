using Math;
using System;

namespace AI
{
  class Mind
  {

    private Logic.Glass   m_managedGlass = null;
    private TimeInterval  m_reactionTimer;

    //-----------------------------------------------------------------------------------
    public void Init(Logic.Glass targetGlass, float fReactionTime)
    {
      m_managedGlass = targetGlass;
      m_reactionTimer = new TimeInterval(fReactionTime);
    }

    //-----------------------------------------------------------------------------------
    public void Update()
    {
      if (m_managedGlass == null)
        return;

      if (!m_reactionTimer.StartNewInterval())
        return;

      InputProvider.EInputAction eNextAction = EvaluateNextStep();
      if (eNextAction != InputProvider.EInputAction.NONE)
      {
        //Simulate key press
        m_managedGlass.ProcessInput((int)eNextAction, true);
        m_managedGlass.ProcessInput((int)eNextAction, false);
      }
    }


    //-----------------------------------------------------------------------------------
    private InputProvider.EInputAction EvaluateNextStep()
    {
      VecInt2 vNewPos = FindBestPos();

      return InputProvider.EInputAction.NONE;
    }


    //-----------------------------------------------------------------------------------
    private VecInt2 FindBestPos()
    {
      return new VecInt2(0, 0);
    }
  }
}
