using UnityEngine;
using System.Collections;

namespace GameGUI.States
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
      Game game = Game.instance;
      if (game.adsMan.ShouldShowAd())
        game.adsMan.Show();
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {

    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {
      Game.instance.adsMan.CancelShow();
    }


    //-----------------------------------------------------------------------------------
    public void OnPlay()
    {
      Game.instance.ui.SwitchToState(new GameSingleParams());
    }

    //-----------------------------------------------------------------------------------
    public void OnPlayPvp()
    {
      Game.instance.ui.SwitchToState(new WaitingGameParams());
    }

    //-----------------------------------------------------------------------------------
    public void OnScore()
    {
      Game.instance.ui.SwitchToState(new PlayersScoreParams(1));
    }

    //-----------------------------------------------------------------------------------
    public void OnSettings()
    {
      Game.instance.ui.SwitchToState(new SettingsParams());
    }

    //-----------------------------------------------------------------------------------
    public void OnQuit()
    {
      MessageBox.Create(GetCanvas(), "Are you sure?", MessageBox.EType.OK_CANCEL, () => { Application.Quit(); });
    }

    //-----------------------------------------------------------------------------------
    public void OnTest()
    {
      
    }

  }
}