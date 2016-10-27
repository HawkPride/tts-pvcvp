using UnityEngine;
using System.Collections;

namespace GUI
{
  public class PlayersScore : UIGameState
  {
    //-----------------------------------------------------------------------------------
    public override EGameState GetStateType()
    {
      return EGameState.PLAYER_SCORE;
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
    public void OnButtonOk()
    {
      Game.Instance.Ui.SwitchToState(EGameState.MAIN_MENU);
    }
  }

}