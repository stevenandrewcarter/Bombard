using LinearMaths;
using System;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace BombardEngine
{
  public class Camera
  {    
    private RectangleF bounds;

    private Vector3 right;
    private Vector3 up;
    private Vector3 look;
    private Vector3 pos;
    
    public Camera(Vector3 cameraPosition, Vector3 cameraLook, Vector3 cameraUp, Vector3 cameraRight, RectangleF newBounds)
    {
      pos = cameraPosition;
      look = cameraLook;
      up = cameraUp;
      right = cameraRight;
      bounds = newBounds;
    }

    #region Properties
   
    public Vector3 Position
    {
      get { return pos; }
      set { pos = value; }
    }

    public Vector3 Right
    {
      get { return right; }
    }

    public Vector3 Up
    {
      get { return up; }
    }

    public Vector3 Look
    {
      get { return look; }
    }

    #endregion

    /*public void Setup()
    {
      engine.Device.Transform.View = Matrix.LookAtLH(cameraRay.Origin, cameraRay.Direction, new Vector3(0.0f, 1.0f, 0.0f));
    }

    public Ray ScreenToWorldCoordinates(PointF p)
    {
      // Calculate the new screen ray
      Vector3 castRay = new Vector3();
      castRay.X = ((2.0f * p.X / engine.Device.Viewport.Width) - 1.0f) * (1.0f / engine.Device.Transform.Projection.M11);
      castRay.Y = ((-2.0f * p.Y / engine.Device.Viewport.Height) + 1.0f) * (1.0f / -engine.Device.Transform.Projection.M22);
      castRay.Z = 1.0f;
      Ray r = new Ray(cameraRay.Origin, castRay);
      r.Origin.TransformCoordinate(engine.Device.Transform.View);
      r.Direction.TransformNormal(engine.Device.Transform.View);
      return r;
    }

    public Ray TranslateToCameraPlain(Ray r)
    {
      // Calculate the point where the ray intersects with the near plane
      LinearMaths.Plane p = new LinearMaths.Plane(new Vector3(0.0f, 0.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.0f, 1.0f, 1.0f));
      LinearMaths.Plane c = new LinearMaths.Plane(new Vector3(0.0f, 0.0f, 5.0f), new Vector3(1.0f, 1.0f, 5.0f), new Vector3(0.0f, 1.0f, 5.0f));
      Vector3 lookAt = r.PlaneIntersection(p);
      if(lookAt == new Vector3())
        return cameraRay;
      //Ray o = new Ray(lookAt, Vector3.Multiply(p.N, -1));
      Ray o = new Ray(lookAt, p.N);
      Vector3 origin = o.PlaneIntersection(c);
      if (origin == new Vector3())
        return cameraRay;
      // Apply bounds on the camera
      if (origin.X < bounds.X)
      {
        origin.X = bounds.X;
        lookAt.X = bounds.X;
      }
      if (origin.X > bounds.X + bounds.Width)
      {
        origin.X = bounds.X + bounds.Width;
        lookAt.X = bounds.X + bounds.Width;
      }
      if (origin.Y < bounds.Y)
      {
        origin.Y = bounds.Y;
        lookAt.Y = bounds.Y;
      }
      if (origin.Y > bounds.Y + bounds.Height)
      {
        origin.Y = bounds.Y + bounds.Height;
        lookAt.Y = bounds.Y + bounds.Height;
      }
      return new Ray(origin, lookAt);
    }

    public void MoveCamera(int screenX, int screenY)
    {
      Ray r = ScreenToWorldCoordinates(new PointF(screenX, screenY));
      cameraRay = TranslateToCameraPlain(r);      
    }*/

    /// <summary>
    /// 
    /// </summary>
    /// <param name="V"></param>
    public void GetViewMatrix(ref Matrix V)
    {
      // Keep camera's axes orthogonal to each other:
      look.Normalize();
      up = Vector3.Cross(look, right);
      up.Normalize();
      right = Vector3.Cross(up, look);
      right.Normalize();
      
      // Build the view matrix:
      float x = -Vector3.Dot(right, pos);
      float y = -Vector3.Dot(up, pos);
      float z = -Vector3.Dot(look, pos);

      V.M11 = right.X;
      V.M12 = up.X;
      V.M13 = look.X;
      V.M14 = 0.0f;
      V.M21 = right.Y;
      V.M22 = up.Y;
      V.M23 = look.Y;
      V.M24 = 0.0f;
      V.M31 = right.Z;
      V.M32 = up.Z;
      V.M33 = look.Z;
      V.M34 = 0.0f;
      V.M41 = x;
      V.M42 = y;
      V.M43 = z;
      V.M44 = 1.0f;
    }
    
    /// <summary>
    /// left/right
    /// </summary>
    /// <param name="units"></param>
    public void Strafe(float units)
    {
      // if (_cameraType == LANDOBJECT)
      pos += new Vector3(right.X, 0.0f, right.Z) * units;
      if (pos.X < bounds.X)
        pos.X = bounds.X;
      if (pos.X > bounds.Width)
        pos.X = bounds.Width;
      //if (_cameraType == AIRCRAFT)
      //  pos += _right * units;
    }

    /// <summary>
    /// up/down
    /// </summary>
    /// <param name="units"></param>
    public void Fly(float units)
    {
      // if (_cameraType == AIRCRAFT)
      pos += up * units;
      if (pos.Y < bounds.Y)
        pos.Y = bounds.Y;
      if (pos.Y > bounds.Height)
        pos.Y = bounds.Height;
    }

    /// <summary>
    /// forward/backward
    /// </summary>
    /// <param name="units"></param>
    public void Walk(float units)
    {
      pos += new Vector3(look.X, 0.0f, look.Z) * units;
      // if( _cameraType == AIRCRAFT )
      //  _pos += _look * units;
    } 

    /// <summary>
    /// rotate on right vector
    /// </summary>
    /// <param name="angle"></param>
    public void Pitch(float angle)
    {
      Matrix T = Matrix.RotationAxis(right, angle);      
      // rotate _up and _look around _right vector
      up = Vector3.TransformCoordinate(up, T);
      look = Vector3.TransformCoordinate(look, T);
    }

    /// <summary>
    /// rotate on up vector
    /// </summary>
    /// <param name="angle"></param>
    public void Yaw(float angle)
    {
      Matrix T = Matrix.RotationAxis(up, angle);
      // rotate around world y (0, 1, 0) always for land object
      //if (_cameraType == LANDOBJECT)
      //  D3DXMatrixRotationY(&T, angle);
      // rotate around own up vector for aircraft
      //if (_cameraType == AIRCRAFT)
      //  D3DXMatrixRotationAxis(&T, &_up, angle);
      // rotate _right and _look around _up or y-axis
      right = Vector3.TransformCoordinate(right, T);
      look = Vector3.TransformCoordinate(look, T);
    }

    /// <summary>
    /// Rotate on look vector
    /// </summary>
    /// <param name="angle"></param>
    public void Roll(float angle)
    {
      // only roll for aircraft type
      //if (_cameraType == AIRCRAFT)
      //{
        Matrix T = Matrix.RotationAxis(look, angle);        
        // rotate _up and _right around _look vector
        right = Vector3.TransformCoordinate(right, T);
        up = Vector3.TransformCoordinate(up, T);
      //}
    }
  }
}
