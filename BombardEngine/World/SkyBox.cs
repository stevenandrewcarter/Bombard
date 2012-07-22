using System;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace BombardEngine.World {
  internal class SkyBox : BombEntity {
    public override void Generate(ref Device device) {
      bounds = new System.Drawing.RectangleF(-30.0f, 0.0f, 60.0f, 30.0f);
      texture = Microsoft.DirectX.Direct3D.TextureLoader.FromFile(
                device,
                Environment.CurrentDirectory + "\\Texture\\SKY_BOX_EGYPT.BMP",
                0,
                0,
                1,
                Usage.None,
                Format.Unknown,
                Pool.Managed,
                Filter.None,
                Filter.None,
                System.Drawing.Color.Magenta.ToArgb());

      float yOffSet = bounds.Height / 2;
      vertices = new CustomVertex.PositionColoredTextured[4];
      vertices[0].Position = new Vector3(bounds.X, bounds.Y + yOffSet, -15.0f);
      vertices[0].Tu = 0.0f; vertices[0].Tv = 0.0f;
      vertices[0].Color = System.Drawing.Color.LightBlue.ToArgb();

      vertices[1].Position = new Vector3(bounds.X, bounds.Y - yOffSet, -15.0f);
      vertices[1].Color = System.Drawing.Color.White.ToArgb();
      vertices[1].Tu = 0.0f; vertices[1].Tv = 1.0f;

      vertices[2].Position = new Vector3(bounds.X + bounds.Width, bounds.Y + yOffSet, -15.0f);
      vertices[2].Color = System.Drawing.Color.LightBlue.ToArgb();
      vertices[2].Tu = 1.0f; vertices[2].Tv = 0.0f;

      vertices[3].Position = new Vector3(bounds.X + bounds.Width, bounds.Y - yOffSet, -15.0f);
      vertices[3].Color = System.Drawing.Color.White.ToArgb();
      vertices[3].Tu = 1.0f; vertices[3].Tv = 1.0f;
    }

    public override void Draw(ref Device device) {
      device.SetTexture(0, texture);
      device.VertexFormat = CustomVertex.PositionColoredTextured.Format;
      device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, vertices);
    }

    public override void Translate(Vector3 v) {
      throw new NotImplementedException();
    }

    public override void Transform(Vector3 v) {

      throw new NotImplementedException();
    }
  }
}
