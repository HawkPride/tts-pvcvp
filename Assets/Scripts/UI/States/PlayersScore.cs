using UnityEngine;
using System.Collections;

namespace GUI
{
  public class PlayersScore : UIGameState
  {
    public DynamicScrollView m_scrollView;

    //-----------------------------------------------------------------------------------
    public override EGameState GetStateType()
    {
      return EGameState.PLAYER_SCORE;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      for (int i = 0; i < 10; i++)
      {
        StatsProviderBase.Stats rec;
        rec.m_strPlayerName = "blah";
        rec.m_nScore = i * 10;

        PlayersScoreItem item = m_scrollView.AddListItem().GetComponent<PlayersScoreItem>();
        item.Set(rec);
      }
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {


    }

    //-----------------------------------------------------------------------------------
    public void OnButtonOk()
    {
      Game.Instance.Ui.SwitchToState(EGameState.MAIN_MENU);
    }
  }

}