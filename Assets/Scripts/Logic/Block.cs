
//-----------------------------------------------------------------------------------
namespace Logic
{
  public class Block
  {
    public enum EType
    {
      WHILE,
      RED,

      COLLAPSING,
    }

    public EType m_eType = EType.WHILE;
  }
}