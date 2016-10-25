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

    // Use this for initialization
    //-----------------------------------------------------------------------------------
    void Start()
    {

    }

    // Update is called once per frame
    //-----------------------------------------------------------------------------------
    void Update()
    {

    }

    //-----------------------------------------------------------------------------------
    public void OnButtonOk()
    {
      UserInterface.Instance().SwitchToState(EGameState.MAIN_MENU);
    }
  }

}