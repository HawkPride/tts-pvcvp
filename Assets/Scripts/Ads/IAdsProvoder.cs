using System;


namespace Ads
{
  interface IAdsProvoder
  {
    void Init(ShowCallback callback);
    bool ShowAd();
    bool ShowRewardedAd();
  }
}
