
//-----------------------------------------------------------------------------------
namespace Net
{
  public class PlayerGlass : Photon.MonoBehaviour
  {
    public Logic.Glass    glass { get { return m_glass;  } }
    public InputProvider  iP    { get { return m_inputProvider; } }

    //-----------------------------------------------------------------------------------
    void OnEnable()
    {
      if (photonView.isMine)
      {
        Logic.GlassLocal glass = new Logic.GlassLocal();
        glass.m_delNewFigure += OnGlassNewFigure;
        glass.m_delChangePos += OnGlassChangePos;
        glass.m_delFigurePlaced += OnGlassFigurePlaced;
        glass.m_delLineAdded += OnGlassLineAdded;
        glass.m_delGameEnd += OnGlassGameEnd;
        m_glass = glass;
      }
      else
      {
        m_glass = new Logic.GlassRemote();
      }
      m_glass.Init();

      m_inputProvider = GetComponent<InputProvider>();
      if (m_inputProvider != null)
      {
        m_inputProvider.m_delEvent += m_glass.ProcessInput;

        if (photonView.isMine)
        {
          m_inputProvider.local = true;
          m_inputProvider.m_delNewInputState += OnInputNewState;
        }
        else
          m_inputProvider.local = false;
      }

      Game.instance.netMan.players.Add(this);
    }

    //-----------------------------------------------------------------------------------
    void OnDisable()
    {
      if (m_inputProvider != null)
      {
        m_inputProvider.m_delEvent -= m_glass.ProcessInput;
        if (photonView.isMine)
          m_inputProvider.m_delNewInputState -= OnInputNewState;
      }

      Game.instance.netMan.players.Remove(this);

      m_glass = null;
    }

    //-----------------------------------------------------------------------------------
    void Update()
    {
      m_glass.Update();
    }

    //-----------------------------------------------------------------------------------
    void OnGlassNewFigure()
    {
      photonView.RPC("RpcNewFigure", PhotonTargets.Others, m_glass.figure.type, GetPos());
    }
    [PunRPC]
    void RpcNewFigure(Logic.Figure.EType eType, PosRot pos)
    {
      Logic.GlassRemote remote = (Logic.GlassRemote)m_glass;
      remote.NewFigure(eType, pos);
    }

    //-----------------------------------------------------------------------------------
    void OnGlassChangePos()
    {
      //photonView.RPC("RpcChangePos", PhotonTargets.Others, GetPos());
    }
    [PunRPC]
    void RpcChangePos(PosRot pos)
    {
      //Logic.GlassRemote remote = (Logic.GlassRemote)m_glass;
      //remote.SetPos(pos, false);
    }

    //-----------------------------------------------------------------------------------
    void OnGlassFigurePlaced()
    {
      photonView.RPC("RpcFigurePlaced", PhotonTargets.Others, GetPos());
    }
    [PunRPC]
    void RpcFigurePlaced(PosRot pos)
    {
      Logic.GlassRemote remote = (Logic.GlassRemote)m_glass;
      remote.SetPos(pos, true);
    }

    //-----------------------------------------------------------------------------------
    void OnGlassLineAdded(bool[] arBusy)
    {
      photonView.RPC("RpcLineAdded", PhotonTargets.Others, arBusy);
    }
    [PunRPC]
    void RpcLineAdded(bool[] arBusy)
    {
      m_glass.AddOneLine(arBusy);
    }

    //-----------------------------------------------------------------------------------
    void OnGlassGameEnd()
    {
      photonView.RPC("RpcGameEnd", PhotonTargets.Others);
    }
    [PunRPC]
    void RpcGameEnd()
    {
      Logic.GlassRemote remote = (Logic.GlassRemote)m_glass;
      remote.GameEnd();
    }

    //-----------------------------------------------------------------------------------
    void OnInputNewState(int nStateFlags)
    {
      photonView.RPC("RpcInputNewState", PhotonTargets.Others, nStateFlags, GetPos());
    }
    [PunRPC]
    void RpcInputNewState(int nStateFlags, PosRot pos)
    {
      m_inputProvider.ForceSetFlags(nStateFlags);
      Logic.GlassRemote remote = (Logic.GlassRemote)m_glass;
      remote.SetPos(pos, false);
    }

    //-----------------------------------------------------------------------------------
    PosRot GetPos()
    {
      PosRot res = new PosRot();
      if (m_glass.figure == null)
        return res;

      res.x = m_glass.figure.pos.x;
      res.y = m_glass.figure.pos.y;
      res.r = m_glass.figure.rot;
      return res;
    }

    //Private
    Logic.Glass    m_glass;
    InputProvider  m_inputProvider;

  }
}
