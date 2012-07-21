using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;

namespace BombardEngine.World
{
  internal class MousePointer : BombEntity
  {

    public override void Generate(ref Device device)
    {
      worldMatrix = device.Transform.World;
      z = 0.0f;
      bounds = new System.Drawing.RectangleF(0.0f, 0.0f, 0.1f, 0.1f);
      texture = Microsoft.DirectX.Direct3D.TextureLoader.FromFile(
                device,
                Environment.CurrentDirectory + "\\Texture\\Cursor\\MOUSE_POINTER.BMP",
                0,
                0,
                1,
                Usage.None,
                Format.Unknown,
                Pool.Managed,
                Filter.None,
                Filter.None,
                System.Drawing.Color.Black.ToArgb());

      float yOffSet = bounds.Height / 2;
      vertices = new CustomVertex.PositionColoredTextured[4];
      vertices[0].Position = new Vector3(bounds.X, bounds.Y + yOffSet, z);
      vertices[0].Tu = 1.0f; vertices[0].Tv = 0.0f;
      vertices[0].Color = System.Drawing.Color.White.ToArgb();

      vertices[1].Position = new Vector3(bounds.X, bounds.Y - yOffSet, z);            
      vertices[1].Tu = 1.0f; vertices[1].Tv = 1.0f;
      vertices[1].Color = System.Drawing.Color.White.ToArgb();

      vertices[2].Position = new Vector3(bounds.X + bounds.Width, bounds.Y + yOffSet, z);
      vertices[2].Tu = 0.0f; vertices[2].Tv = 0.0f;      
      vertices[2].Color = System.Drawing.Color.White.ToArgb();     

      vertices[3].Position = new Vector3(bounds.X + bounds.Width, bounds.Y - yOffSet, z);
      vertices[3].Tu = 0.0f; vertices[3].Tv = 1.0f;
      vertices[3].Color = System.Drawing.Color.White.ToArgb();      
    }

    public override void Draw(ref Device device)
    {
      device.SetTexture(0, texture);
      Matrix world = device.Transform.World;
      device.Transform.World = worldMatrix;
      device.VertexFormat = CustomVertex.PositionColoredTextured.Format;
      device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, vertices);
      device.Transform.World = world;
    }

    public override void Translate(Vector3 v)
    {
      worldMatrix.Translate(v);
    }

    public override void Transform(Vector3 v)
    {
      
    }
  }
}
