using System;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace BombardEngine.World {
  internal class BombWorld : BombEntity {
    private Engine engine;
    private BombEntity skyBox;

    public BombWorld(Engine renderEngine) {
      engine = renderEngine;
      skyBox = new SkyBox();
    }

    public override void Generate(ref Device device) {
      skyBox.Generate(ref device);
    }

    public override void Transform(Vector3 v) {
      throw new NotImplementedException();
    }

    public override void Translate(Vector3 v) {
      throw new NotImplementedException();
    }

    public override void Draw(ref Device device) {
      skyBox.Draw(ref device);
    }
  }
}
