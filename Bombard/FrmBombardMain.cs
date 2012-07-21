using BombardEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Bombard
{
  public partial class FrmBombardMain : Form
  {
    private BombardEngine.Engine engine;
    public FrmBombardMain()
    {      
      InitializeComponent();      
    }

    public void Start()
    {
      engine = new BombardEngine.Engine(Size, Handle);
      engine.RunWorkerAsync();
    }

    private void tlsbBombardExit_Click(object sender, EventArgs e)
    {
      Dispose();
    }
  }
}