using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.ComponentModel;
using System.Drawing;
using BombardEngine.World;
using LinearMaths;

namespace BombardEngine
{
  public class Engine : BackgroundWorker
  {   
    // DirectX device
    private Device device;
    // Camera class
    private Camera camera;
    // Viewing area size ? Might not need this
    private Size viewingArea;
    // Drawing canvas
    private IntPtr handle;
    // World entities
    private BombEntity world;
    private BombEntity mousePointer;  // Defunct?
    // Indicates if the right mouse botton is pressed
    private bool rightMouse;

    private Microsoft.DirectX.Direct3D.Font font = null;
    
    public Engine(Size viewPort, IntPtr deviceHandle)
    {
      device = null;
      viewingArea = viewPort;
      handle = deviceHandle;
      // Configure the background thread for drawing
      WorkerSupportsCancellation = true;
      InitializeGraphics();
      camera = new Camera(new Vector3(0, 0, 0), new Vector3(0, 0, -1), new Vector3(0, -1, 0), new Vector3(-1, 0, 0), new RectangleF(-21.0f, -6.5f, 23.0f, 8.8f));
      world = new BombWorld(this);
      mousePointer = new MousePointer();
      mousePointer.Generate(ref device);
      world.Generate(ref device);
      rightMouse = false;
      CaptureEvents();
    }

    #region Properties

    public Device Device
    {
      get { return device; }
    }

    #endregion

    #region Protected Methods

    protected override void OnDoWork(DoWorkEventArgs e)
    {
      while (!CancellationPending)
      {
        device.Clear(ClearFlags.Target, System.Drawing.Color.CornflowerBlue, 1.0f, 0);
        // Setup the camera
        Matrix V = new Matrix();
        camera.GetViewMatrix(ref V);
        device.SetTransform(TransformType.View, V);
        device.BeginScene();        
        // Draw the world        
        world.Draw(ref device);
        // Draw the GUI
        DrawGUI();
        // Draw the mouse pointers
        // mousePointer.Draw(ref device);        
        device.EndScene();
        device.Present();
      }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// We will initialize our graphics device here
    /// </summary>
    private void InitializeGraphics()
    {
      // Set our presentation parameters
      PresentParameters presentParams = new PresentParameters();

      presentParams.Windowed = true;
      presentParams.SwapEffect = SwapEffect.Flip;

      // Create our device
      device = new Device(0, DeviceType.Hardware, handle, CreateFlags.HardwareVertexProcessing, presentParams);
      device.RenderState.Lighting = false;
      device.RenderState.CullMode = Cull.CounterClockwise;
      device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, viewingArea.Width / viewingArea.Height, 1.0f, 100.0f);
      InitializeGUI();
    }

    private void InitializeGUI()
    {
      System.Drawing.Font localFont = new System.Drawing.Font("Arial", 8.0f, FontStyle.Regular);
      
      // Create a font we can draw with
      font = new Microsoft.DirectX.Direct3D.Font(device, localFont);
    }

    private void DrawGUI()
    {
      DrawText("Bombard " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version, 0, 0);
      DrawText("X: " + camera.Position.X + " Y: " + camera.Position.Y + " Z: " + camera.Position.Z, 0, 10);
      DrawText("X: " + device.Transform.View.M11 + " Y: " + device.Transform.View.M22, 0, 20);     
    }

    private void DrawText(string message, int x, int y)
    {
      font.DrawText(null, message, new Rectangle(x, y, viewingArea.Width / 2, viewingArea.Height / 2), DrawTextFormat.NoClip | DrawTextFormat.ExpandTabs | DrawTextFormat.WordBreak, Color.White);
    }

    /// <summary>
    /// Override the containing controls events and handle them inside the engine
    /// </summary>
    private void CaptureEvents()
    {
      System.Windows.Forms.Control control = System.Windows.Forms.Control.FromHandle(handle);
      control.MouseMove += new System.Windows.Forms.MouseEventHandler(control_MouseMove);
      control.MouseDown += new System.Windows.Forms.MouseEventHandler(control_MouseDown);
      control.MouseUp += new System.Windows.Forms.MouseEventHandler(control_MouseUp);
    }

    void control_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      rightMouse = false;
    }

    void control_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      rightMouse = true;
    }

    void control_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      /*Ray r = camera.ScreenToWorldCoordinates(new PointF(e.X, e.Y));
      Ray mousePos = camera.TranslateToCameraPlain(r);
      Vector3 v = new Vector3(mousePos.Direction.X, mousePos.Direction.Y, mousePos.Direction.Z);
      mousePointer.Translate(v);*/
      if (rightMouse)      
      {
        float x = 0.05f;
        float y = 0.05f;
        double centerX = viewingArea.Width / 2;
        x = (float)((e.X - centerX) / viewingArea.Width);
        camera.Strafe(x);
        double centerY = viewingArea.Height / 2;
        y = -1.0f * (float)((e.Y - centerY) / viewingArea.Height);
        camera.Fly(y);
        // camera.MoveCamera(e.X, e.Y);
      }
    }    

    #endregion
  }
}
