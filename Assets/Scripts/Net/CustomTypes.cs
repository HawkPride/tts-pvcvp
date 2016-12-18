
using ExitGames.Client.Photon;

namespace Net
{
  class CustomTypes
  {
    public static void Register()
    {
      PhotonPeer.RegisterType(typeof(GameSyncPos), (byte)'p', SzerializePos, DeserializePos);
    }

    public static readonly byte[] memPos = new byte[3];
    private static short SzerializePos(StreamBuffer outStream, object customobject)
    {
      GameSyncPos pos = (GameSyncPos)customobject;
      lock (memPos)
      {
        byte[] bytes = memPos;
        memPos[0] = (byte)pos.posX;
        memPos[1] = (byte)pos.posY;
        memPos[2] = (byte)pos.rot;
        outStream.Write(bytes, 0, 3);
      }

      return 3;
    }

    private static object DeserializePos(StreamBuffer inStream, short length)
    {
      GameSyncPos pos = new GameSyncPos();
      lock (memPos)
      {
        inStream.Read(memPos, 0, 3);
        pos.posX = memPos[0];
        pos.posY = memPos[1];
        pos.rot = memPos[2];
      }

      return pos;
    }
  }
}
