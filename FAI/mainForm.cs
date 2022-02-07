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
using System.Data.OleDb;

namespace FAI
{
 public partial class mainForm : Form
 {
  string user;
  string enumber;
  string position;
  Login login;
  OleDbConnection con;
  public mainForm(string user1,string enumber1,string position1,Login login1)
  {
   login= login1;
   user = user1;
   enumber = enumber1;
   position = position1;
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
   con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\WINDELL-GINKJNK\LightCareFiles\incomingFiles\incomingDatabase.accdb");
   string strSql = "SELECT * FROM incomingRecordsTable WHERE Inspector LIKE '%" + user + "%'";
   OleDbCommand command = new OleDbCommand(strSql, con);
   command.CommandType = CommandType.Text;
   OleDbDataAdapter da = new OleDbDataAdapter(command);
   DataSet ds = new DataSet();
   da.Fill(ds);
   dataGridView1.DataSource=ds.Tables[0];
   dataGridView1.Columns["ID"].Visible = false;
   dataGridView1.Columns["Dimension1-1"].Visible = false;
   dataGridView1.Columns["Dimension1-2"].Visible = false;
   dataGridView1.Columns["Dimension1-3"].Visible = false;
   dataGridView1.Columns["Dimension2-1"].Visible = false;
   dataGridView1.Columns["Dimension2-2"].Visible = false;
   dataGridView1.Columns["Dimension2-3"].Visible = false;
   dataGridView1.Columns["Dimension3-1"].Visible = false;
   dataGridView1.Columns["Dimension3-2"].Visible = false;
   dataGridView1.Columns["Dimension3-3"].Visible = false;
   dataGridView1.Columns["Dimension4-1"].Visible = false;
   dataGridView1.Columns["Dimension4-2"].Visible = false;
   dataGridView1.Columns["Dimension4-3"].Visible = false;
   dataGridView1.Columns["Dimension5-1"].Visible = false;
   dataGridView1.Columns["Dimension5-2"].Visible = false;
   dataGridView1.Columns["Dimension5-3"].Visible = false;
   dataGridView1.Columns["Tolerance1"].Visible = false;
   dataGridView1.Columns["Tolerance2"].Visible = false;
   dataGridView1.Columns["Tolerance3"].Visible = false;
   dataGridView1.Columns["Tolerance4"].Visible = false;
   dataGridView1.Columns["Tolerance5"].Visible = false;
   dataGridView1.Columns["Inspector"].Visible = false;
   
   login.Hide();
   nameLabel.Text = user;
   label9.Text = enumber;
   if(position=="SrTech"||position=="JrTech"||position=="Management")
   {
    for(int i=2;i<6;i++)
    {
     Control ctn = this.Controls["position" + i.ToString()+"label"];
     ctn.Text="";
    }
    if(position=="SrTech")
    {
     position1label.Text="Senior Technician";
    }
    else if(position=="JrTech")
    {
     position1label.Text="Junior Technician";
    }
    else if (position=="Management")
    {
     position1label.Text="Management";
    }
   }
   else
   {
    position1label.Text= position;
   }

     for(int i=1;i<6;i++)
    {
     Control ctn = this.Controls["position" + i.ToString()+"label"];
     int newsize = 0;
     newsize=ctn.Width-56;
     newsize=79-(newsize/2);
     ctn.Left= newsize;
    }
    int labelsize=0;
    labelsize=nameLabel.Width-68;
    labelsize=72-(labelsize/2);
    nameLabel.Left=labelsize;
    
   
   OvalPictureBox ovalito = new OvalPictureBox();
   ovalito.Size = new Size(200, 200);
   ovalito.Left =7;
   ovalito.Top = 35;
   OvalPictureBox ovalito2 = new OvalPictureBox();
   ovalito2.Size = new Size(204, 204);
   ovalito2.Left = 5;
   ovalito2.Top = 33;
   ovalito2.BackgroundImage = Image.FromFile(@"C:\Users\Miguel Rodmen\Desktop\gray.png");
  /// ovalito.BackColor = Color.White;

            

   this.Controls.Add(ovalito);
   this.Controls.Add(ovalito2);
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

  private void button3_MouseMove(object sender, MouseEventArgs e)
  {
    contextStrip.Text="Select to start the inspection of a new incomed "+"\n"+ "supplier batch, this inspection will generate a " + "\n" + "FAI report, in case that the batch is not accceptable " + "\n" + " a Non-Conforming Report will be created";
 
  }

        private void mainForm_MouseMove(object sender, MouseEventArgs e)
        {
            contextStrip.Text = "";
        }

        private void incomingButton_Click(object sender, EventArgs e)
        {
         this.Hide();
         Partnumberselection pns =new Partnumberselection(user,this);
         pns.Show();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
         this.Close();
         login.Show();
        }
        int xPos;
        int yPos;
        private void flowLayoutPanel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left += e.X - xPos;
                this.Top += e.Y - yPos;
            }
        }

        private void flowLayoutPanel2_MouseDown(object sender, MouseEventArgs e)
        {
         if (e.Button == System.Windows.Forms.MouseButtons.Left)
         {
          xPos = e.X;
          yPos = e.Y;
         }
        }

        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left += e.X - xPos;
                this.Top += e.Y - yPos;
            }
        }

        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                xPos = e.X;
                yPos = e.Y;
            }
        }

        private void NCMRButton_Click(object sender, EventArgs e)
        {

        }

        private void NCMRButton_MouseMove(object sender, MouseEventArgs e)
        {
         contextStrip.Text = "Select to start to review the non-conforming" + "\n" + "materials, release them or reject them";
        }


        private void returnedButton_Click(object sender, EventArgs e)
        {

        }

        private void returnedButton_MouseMove(object sender, MouseEventArgs e)
        {
            contextStrip.Text = "Select to review returned devices";

        }
    }
}
