using Microsoft.DirectX;

namespace LinearMaths {
  public class Plane {
    private Vector3 n;
    private Vector3 p;
    private float d;

    public Plane(Vector3 normal, Vector3 pointOnPlane, float offSet) {
      n = normal;
      p = pointOnPlane;
      d = offSet;
    }

    public Plane(Vector3 p0, Vector3 p1, Vector3 p2) {
      Vector3 u = p1 - p0;
      Vector3 v = p2 - p0;
      n = Vector3.Cross(u, v);
      p = p0;
      d = -(Vector3.Dot(n, p0));
    }

    public float D { get { return d; } }
    public Vector3 N { get { return n; } }
    public Vector3 P { get { return p; } }
  }
}
