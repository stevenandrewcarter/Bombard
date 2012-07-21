using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;

namespace BombardEngine
{
  internal abstract class BombEntity
  {
    protected BaseTexture texture;
    protected System.Drawing.RectangleF bounds;
    protected CustomVertex.PositionColoredTextured[] vertices;
    protected float z;
    protected Matrix worldMatrix;

    public abstract void Transform(Vector3 v);

    public abstract void Translate(Vector3 v);

    public abstract void Draw(ref Device device);

    public abstract void Generate(ref Device device);
  }
}
