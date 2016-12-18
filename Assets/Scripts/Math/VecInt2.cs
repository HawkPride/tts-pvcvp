using System;

namespace Math
{
  [Serializable]
  public class VecInt2
  {
    public int x = 0;
    public int y = 0;

    public VecInt2()
    {
    }

    public VecInt2( VecInt2 other )
    {
      x = other.x;
      y = other.y;
    }

    public VecInt2( int x, int y )
    {
      this.x = x;
      this.y = y;
    }

    public static VecInt2 operator +( VecInt2 one, VecInt2 other )
    {
      return new VecInt2(one.x + other.x, one.y + other.y);
    }

    public static VecInt2 operator-( VecInt2 one, VecInt2 other )
    {
      return new VecInt2(one.x - other.x, one.y - other.y);
    }
  }

}
