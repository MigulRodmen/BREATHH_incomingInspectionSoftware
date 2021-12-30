using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace FAI
{
 public partial class mainForm : Form
 {
  string user;
  string enumber;
  public mainForm(string user1,string enumber1)
  {
   user = user1;
   enumber = enumber1;
   InitializeComponent();
  }
  class OvalPictureBox : PictureBox
  {
   public OvalPictureBox()
   {
    this.BackColor = Color.DarkGray;
   }
   protected override void OnResize(EventArgs e)
   {
    base.OnResize(e);
    using (var gp = new GraphicsPath())
    {
     gp.AddEllipse(new Rectangle(0, 0, this.Width - 1, this.Height - 1));
     this.Region = new Region(gp);
    }
   }
  }
  private void mainForm_Load(object sender, EventArgs e)
  {
   label1.Text = user;
   label9.Text = enumber;
   OvalPictureBox ovalito = new OvalPictureBox();
   ovalito.Size = new Size(200, 200);
   ovalito.Left =7;
   ovalito.Top = 35;
   this.Controls.Add(ovalito);
   ovalito.BackgroundImageLayout = ImageLayout.Stretch;
   ovalito.BackgroundImage = Image.FromFile(@"\\WINDELL-GINKJNK\\LightCareFiles\userPics\"+enumber+".jpg");
   typeof(Control).GetMethod("OnResize",
   BindingFlags.Instance | BindingFlags.NonPublic)
   .Invoke(ovalito, new object[] { EventArgs.Empty });
  }
  private void closebutton_Click(object sender, EventArgs e)
  {
   Application.Exit();
  }
  private void minimizeButton_Click(object sender, EventArgs e)
  {
   this.WindowState = FormWindowState.Minimized;
  }
 }
}
