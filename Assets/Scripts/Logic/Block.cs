
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

    public EType type { get { return m_eType;  } set { m_eType = value; } }

    EType m_eType = EType.WHILE;
  }
}