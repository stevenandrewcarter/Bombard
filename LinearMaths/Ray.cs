using Microsoft.DirectX;

namespace LinearMaths
{
  public class Ray
  {
    private Vector3 origin;
    private Vector3 direction;

    public Ray(Vector3 aOrigin, Vector3 aDirection)
    {
      origin = aOrigin;
      direction = aDirection;
    }

    public Vector3 PlaneIntersection(Plane p)
    {
      float t1 = -p.D - Vector3.Dot(p.N, origin);
      float t2 = Vector3.Dot(p.N, direction);
      if (t2 != 0)
      {
        float t = t1 / t2;
        Vector3 intersect = origin + Vector3.Multiply(direction, t);
        return intersect;
      }
      else
      {
        return new Vector3();
      }
    }

    public Vector3 Origin 
    {
      get { return origin; }
      set { origin = value; }
    }
    public Vector3 Direction 
    { 
      get { return direction; }
      set { direction = value; }
    }
  }
}
