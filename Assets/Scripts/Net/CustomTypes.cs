
using ExitGames.Client.Photon;

namespace Net
{
  public struct PosRot
  {
    public int x;
    public int y;
    public int r;
  }

  class CustomTypes
  {
    public static void Register()
    {
     PhotonPeer.RegisterType(typeof(PosRot), (byte)'p', SzerializePos, DeserializePos);
    }

    public static readonly byte[] memPos = new byte[3];
    private static short SzerializePos(StreamBuffer outStream, object customobject)
    {
      PosRot pos = (PosRot)customobject;
      lock (memPos)
      {
        byte[] bytes = memPos;
        memPos[0] = (byte)pos.x;
        memPos[1] = (byte)pos.y;
        memPos[2] = (byte)pos.r;
        outStream.Write(bytes, 0, 3);
      }

      return 3;
    }

    private static object DeserializePos(StreamBuffer inStream, short length)
    {
      PosRot pos;
      lock (memPos)
      {
        inStream.Read(memPos, 0, 3);
        pos.x = memPos[0];
        pos.y = memPos[1];
        pos.r = memPos[2];
      }

      return pos;
    }
  }
}
