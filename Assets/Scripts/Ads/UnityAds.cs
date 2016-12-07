using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
#if UNITY_ANDROID
  public class UnityAds : IAdsProvoder
  {
    ShowCallback m_callback;

    //-----------------------------------------------------------------------------------
    public void Init(ShowCallback callback)
    {
      if (callback != null)
        m_callback += callback;

      //Advertisement.Initialize()
    }

    //-----------------------------------------------------------------------------------
    public bool ShowAd()
    {
      return ShowImpl("video");
    }

    //-----------------------------------------------------------------------------------
    public bool ShowRewardedAd()
    {
      return ShowImpl("rewardedVideo");
    }

    //-----------------------------------------------------------------------------------
    private bool ShowImpl(string strZoneId)
    {
      if (Advertisement.IsReady(strZoneId))
      {
        var options = new ShowOptions { resultCallback = HandleShowResult };
        Advertisement.Show(strZoneId, options);
        return true;
      }
      return false;
    }

    //-----------------------------------------------------------------------------------
    private void HandleShowResult(ShowResult result)
    {
      EShowResult res = EShowResult.FAILED;
      switch (result)
      {
      case ShowResult.Finished:
        res = EShowResult.OK;
        break;
      case ShowResult.Skipped:
        res = EShowResult.SKIPPED;
        break;
      case ShowResult.Failed:
        res = EShowResult.FAILED;
        break;
      }
      m_callback(res);
    }
  }
#endif
}