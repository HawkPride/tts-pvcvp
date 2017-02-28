using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GameGUI.States
{
  //Creation params
  public class SettingsParams : GameStateParams
  {
    public override string GetSceneName() { return "Settings"; }
    public override EGameStateType GetStateType() { return EGameStateType.SETTINGS; }
  }



  public class Settings : GameState
  {
    public InputField m_inputPlayerName;



    bool m_bNameDirty = false;

    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.SETTINGS;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      StatsProviderBase.Config cfg = Game.instance.stats.GetConfig();
      if (m_inputPlayerName != null)
      {
        m_inputPlayerName.text = cfg.m_strPlayerName;
        m_bNameDirty = false;
      }
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
    public void OnNameEntered()
    {
      m_bNameDirty = true;
    }


    //-----------------------------------------------------------------------------------
    public void OnOK()
    {
      StatsProviderBase.Config cfg = Game.instance.stats.GetConfig();
      if (m_inputPlayerName != null && m_bNameDirty)
      {
        cfg.m_strPlayerName = m_inputPlayerName.text;

      }

      if (m_bNameDirty)
        Game.instance.stats.Save();

      OnExit();
    }

    //-----------------------------------------------------------------------------------
    public void OnExit()
    {
      Game.instance.ui.SwitchToState(new MainMenuParams());
    }

  }
}