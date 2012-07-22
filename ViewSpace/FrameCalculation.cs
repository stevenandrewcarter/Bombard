
namespace ViewSpace {
  public class FrameCalculation {
    // Frames per second calculation
    private int frameCount;
    private float timeElapsed;
    private float fps;

    public FrameCalculation() {
      frameCount = 0;
      timeElapsed = 0;
      fps = 0;
    }

    public void Calculate(float timeDelta) {
      frameCount++;
      timeElapsed += timeDelta;
      if (timeElapsed >= 1.0f) {
        fps = (float)frameCount / timeElapsed;
        timeElapsed = 0.0f;
        frameCount = 0;
      }
    }
  }
}
