using UnityEngine;
using System.Collections;

namespace GUI.States
{
  //Creation params
  public class MainMenuParams : GameStateParams
  {
    public override string GetSceneName() { return "MainMenu"; }
    public override EGameStateType GetStateType() { return EGameStateType.MAIN_MENU; }
  }



  public class MainMenu : GameState
  {

    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.MAIN_MENU;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {
      

    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {

    }


    //-----------------------------------------------------------------------------------
    public void OnPlay()
    {
      Game.Instance.Ui.SwitchToState(new GameSingleParams());
    }

    //-----------------------------------------------------------------------------------
    public void OnScore()
    {
      Game.Instance.Ui.SwitchToState(new PlayersScoreParams(1));
    }

    //-----------------------------------------------------------------------------------
    public void OnSettings()
    {
      MessageBox.Create(GetCanvas(), "Hello world", MessageBox.EType.OK, OnTest);
    }

    //-----------------------------------------------------------------------------------
    public void OnQuit()
    {
      MessageBox.Create(GetCanvas(), "Are you sure?", MessageBox.EType.OK_CANCEL, () => { Application.Quit(); });
    }

    //-----------------------------------------------------------------------------------
    public void OnTest()
    {
      GameMessage.Create(GetCanvas(), "YOU WON!", null, 5.0f);
      /*StatsProviderBase sp = new StatsProviderLocal();
      for (int i = 0; i < 15; i++)
      {
        StatsProviderBase.Stats stats = new StatsProviderBase.Stats();
        stats.m_strPlayerName = "Hawk" + i.ToString();
        stats.m_nScore = Random.Range(10, 100);
        sp.AddStats(stats);
      }
      sp.Save();*/

    }

  }
}