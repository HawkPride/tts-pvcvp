using UnityEngine;

class TimeInterval
{
  public TimeInterval(float fInterval)
  {
    m_fInterval = fInterval;
    Reset();
  }

  public float Passed()
  {
    return Time.time - m_fStartTime;
  }

  public void Reset()
  {
    m_fStartTime = Time.time;
  }

  public bool StartNewInterval()
  {
    if (IsIntervalPassed())
    {
      float fTimeLeft = Passed() - m_fInterval;
      m_fStartTime = Time.time + fTimeLeft;
      return true;
    }
    return false;
  }

  public bool IsIntervalPassed()
  {
    float fPassed = Passed();
    return fPassed > m_fInterval;
  }

  public float Interval { set { m_fInterval = value; } get { return m_fInterval; } }

  float m_fStartTime;
  float m_fInterval = 0.1f;
}