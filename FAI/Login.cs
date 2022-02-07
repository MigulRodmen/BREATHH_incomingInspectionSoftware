using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FAI
{
 public partial class Login : Form
 {
  OleDbConnection con;
  public Login()
  {
   InitializeComponent();
  }
  private void Login_Load(object sender, EventArgs e)
  {
   con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\WINDELL-GINKJNK\LightCareFiles\credentialsDatabase.accdb");
   try
   {
    con.Open();
   }
   catch
   {
    MessageBox.Show("Database Failed");
   }  
  }
  private void blindbutton_MouseDown(object sender, MouseEventArgs e)
  {
   blindbutton.BackgroundImageLayout = ImageLayout.Stretch;
   blindbutton.BackgroundImage = FAI.Properties.Resources.openedeye;
   passwordTextBox.UseSystemPasswordChar = false;
  }
  private void blindbutton_MouseUp(object sender, MouseEventArgs e)
  {
   blindbutton.BackgroundImageLayout = ImageLayout.Stretch;
   blindbutton.BackgroundImage = FAI.Properties.Resources.blindeye;
   passwordTextBox.UseSystemPasswordChar = true;
   passwordTextBox.Select();
  }
  private void closebutton_Click_1(object sender, EventArgs e)
  {
   Application.Exit();
  }
  private void minimizeButton_Click(object sender, EventArgs e)
  {
   this.WindowState = FormWindowState.Minimized;
  }
  int xPos;
  int yPos;
  private void flowLayoutPanel2_MouseDown(object sender, MouseEventArgs e)
  {
   if (e.Button == System.Windows.Forms.MouseButtons.Left)
   {
    xPos = e.X;
    yPos = e.Y;
   }
  }
  private void flowLayoutPanel2_MouseMove(object sender, MouseEventArgs e)
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

        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left += e.X - xPos;
                this.Top += e.Y - yPos;
            }
        }

        private void label8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                xPos = e.X;
                yPos = e.Y;
            }
        }

        private void label8_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left += e.X - xPos;
                this.Top += e.Y - yPos;
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
         int blankuser = 0;
         if (usernameTextBox.Text != null)
         {
          if (!string.IsNullOrWhiteSpace(usernameTextBox.Text))
          {
           blankuser = 1;
          }
         }
         if(blankuser!=1)
         {
          passwordTextBox.Text="";
          MessageBox.Show("Please fullfill your username");
         }
         else
         {
          int blankpassword = 0;
         if (passwordTextBox.Text != null)
         {
          if (!string.IsNullOrWhiteSpace(passwordTextBox.Text))
          {
           blankpassword = 1;
          }
         }
         if(blankpassword!= 1)
         {
          passwordTextBox.Select();
          MessageBox.Show("Please fullfill the password");
         }
         else
         {
          string strSql = "SELECT * FROM credentialsTable WHERE Username LIKE '%" + usernameTextBox.Text.Trim() + "%'";
          OleDbCommand command = new OleDbCommand(strSql, con);
          command.CommandType = CommandType.Text;
          OleDbDataAdapter da = new OleDbDataAdapter(command);
          DataSet ds = new DataSet();
          da.Fill(ds);
          String pass;
          try
          {
           pass = ds.Tables[0].Rows[0][2].ToString();
          }
          catch
          {
           pass="";
           MessageBox.Show("Username doens't exist");
           usernameTextBox.Text="";
           passwordTextBox.Text="";
           usernameTextBox.Select();
          }
          if(pass!="")
          {
           if (passwordTextBox.Text == pass)
           {
            usernameTextBox.Text="";
            passwordTextBox.Text="";
            string position= ds.Tables[0].Rows[0][5].ToString();
                   
            if (position.IndexOf("incomingInspector")!=-1||position.IndexOf("SrTech")!=-1|| position.IndexOf("JrTech")!= -1||position.IndexOf("Management")!=-1)
            {
             string user= ds.Tables[0].Rows[0][3].ToString()+" "+ds.Tables[0].Rows[0][4].ToString();
             string enumber= ds.Tables[0].Rows[0][6].ToString();
             mainForm mainy= new mainForm(user,enumber,position,this);
             mainy.Show();
             this.Hide();
            }
            else
            {
             MessageBox.Show("You don't have enough permissions for this app");
             usernameTextBox.Select();
            }
           }
           else
           {
            MessageBox.Show("Wrong Password!");
            passwordTextBox.Text="";
            passwordTextBox.Select();
           }
           string spass=pass.ToString();
          }
         }
        }
       }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
         con.Close();
        }
    }
}
