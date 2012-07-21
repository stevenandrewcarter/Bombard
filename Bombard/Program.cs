using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bombard
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      FrmBombardMain main = new FrmBombardMain();      
      main.Start();
      Application.Run(main);
    }
  }
}