using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameGUI.States
{
  public abstract class GameStateImplTpl<TParams> : GameState
    where TParams : GameStateParams, new()
  {
    //Static part
    private static TParams s_paramsAccessor = new TParams();


    //Dynamic part
    private TParams m_creationParams;


    public sealed override EGameStateType GetStateType()
    {
      return s_paramsAccessor.GetStateType();
    }

    public sealed override void SetParams(GameStateParams stateParams)
    {
      m_creationParams = (TParams)stateParams;
    }

    protected TParams GetParams()
    {
      return m_creationParams;
    }

  }
}
