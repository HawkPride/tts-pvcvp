using System;


namespace Ads
{
  public enum EShowResult
  {
    OK,
    SKIPPED,
    FAILED,
  }

  public delegate void ShowCallback(EShowResult eResult);

  public class AdsManager
  {
    //-----------------------------------------------------------------------------------
    public void Init()
    {
      m_bShowOrdered = false;
      m_bShown = false;
      //Consume the ad was shown at the previous launch
      m_nLastShowGamesCount = Game.Instance.Stats.m_nGamesPlayed;

      m_adsProvider.Init(CompleteCallback);
    }

    //-----------------------------------------------------------------------------------
    public void Update()
    {
      if (m_bShowOrdered && !m_bShown)
      {
        m_bShown = m_adsProvider.ShowRewardedAd();
        if (m_bShown)
          m_nLastShowGamesCount = Game.Instance.Stats.m_nGamesPlayed;
      }
    }

    //-----------------------------------------------------------------------------------
    public void CancelShow()
    {
      m_bShowOrdered = false;
    }

    //-----------------------------------------------------------------------------------
    public bool ShouldShowAd()
    {
      //Repeat each n games
      const int REPEAT_RATE = 8;
      const int REPEAT_OFFSET = 3;
      int nCurrGamesCount = Game.Instance.Stats.m_nGamesPlayed;
      bool bShow = (nCurrGamesCount % REPEAT_RATE == REPEAT_OFFSET) && (nCurrGamesCount != m_nLastShowGamesCount);
      return bShow;
    }

    //-----------------------------------------------------------------------------------
    public void Show()
    {
      m_bShowOrdered = true;
      m_bShown = false;
    }

    //-----------------------------------------------------------------------------------
    private void CompleteCallback(EShowResult eResult)
    {
      m_bShown = false;
      m_bShowOrdered = false;
    }


    UnityAds  m_adsProvider = new UnityAds();

    bool  m_bShowOrdered = false;
    bool  m_bShown = false;
    int   m_nLastShowGamesCount = 0;
  }
}
