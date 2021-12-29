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
 public partial class Partnumberselection : Form
 {
  OleDbConnection con; 
  public Partnumberselection()
  {  
   InitializeComponent();
  }
  private void searchButton_Click(object sender, EventArgs e)
  { 
   int index = partNumberComboBox.FindString(partNumberComboBox.Text);

   if (index!=-1)
   {
    int partnumbers = 0;
    int batches = 0;
    if (partNumberComboBox.Text != null)
    {
     if (!string.IsNullOrWhiteSpace(partNumberComboBox.Text))
     {
      partnumbers = 1;
     }
    }
          
    if (batchTextBox.Text != null)
    {
     if (!string.IsNullOrWhiteSpace(batchTextBox.Text))
     {
      batches = 1;
     }
    }

    if (partnumbers==1&&batches==1)
    {    
     string [] dimensions=new string[5];
     string [] tolerances=new string[5];
     string strSqlrev = "SELECT * FROM incomingMaterialsTable WHERE PartNumber LIKE '%" + partNumberComboBox.Text + "%'";
     OleDbCommand commandrev = new OleDbCommand(strSqlrev, con);
     commandrev.CommandType = CommandType.Text;
     OleDbDataAdapter darev = new OleDbDataAdapter(commandrev);
     DataSet dsrev = new DataSet();
     darev.Fill(dsrev);  
     
     String rev = dsrev.Tables[0].Rows[0][2].ToString();
     String description= dsrev.Tables[0].Rows[0][3].ToString();

     for (int i=0;i<5;i++)
     {
      dimensions[i]= dsrev.Tables[0].Rows[0][4+(i*2)].ToString();
      tolerances[i]= dsrev.Tables[0].Rows[0][5+(i*2)].ToString();
     }
     con.Close();
        
     PDFviewer viewer = new PDFviewer(this,partNumberComboBox.Text,rev,description,dimensions,tolerances,vendorComboBox.Text,batchsize.Value.ToString(),batchTextBox.Text);
     partNumberComboBox.SelectedIndex = -1;
     partNumberComboBox.Text="";
     batchTextBox.Text="";
     vendorComboBox.Text="";
     batchsize.Value=0;
     viewer.Show();
    }
    else
    {
     System.Windows.Forms.MessageBox.Show("Missing Batch or part number");
     partNumberComboBox.Select();
    }
   }
   else
   {
    System.Windows.Forms.MessageBox.Show("Invalid part number");
    partNumberComboBox.Text = "";
    partNumberComboBox.Select();
    batchTextBox.Text="";
   }
  }    

  private void Partnumberselection_Load(object sender, EventArgs e)
  {  
   batchsize.Select(0, batchsize.Text.Length);
   con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\WINDELL-GINKJNK\LightCareFiles\incomingfiles\incomingDatabase.accdb");
   try
   {
    con.Open();
   }
   catch
   {
    System.Windows.Forms.MessageBox.Show("Database Failed");
   }
   string strSql = "SELECT PartNumber from incomingMaterialsTable";
   OleDbCommand command = new OleDbCommand(strSql, con);
   command.CommandType = CommandType.Text;
   OleDbDataAdapter dt = new OleDbDataAdapter(command);
   DataTable ds = new DataTable();
   dt.Fill(ds);
   partNumberComboBox.DisplayMember = "PartNumber";
   partNumberComboBox.ValueMember = "PartNumber";
   partNumberComboBox.DataSource = ds;
   partNumberComboBox.Text = "";
   partNumberComboBox.Select();

   string strSql1 = "SELECT Supplier from suppliersTable";
   OleDbCommand command1 = new OleDbCommand(strSql1, con);
   command1.CommandType = CommandType.Text;
   OleDbDataAdapter dt1 = new OleDbDataAdapter(command1);
   DataTable ds1 = new DataTable();
   dt1.Fill(ds1);
   vendorComboBox.DisplayMember = "Supplier";
   vendorComboBox.ValueMember = "Supplier";
   vendorComboBox.DataSource = ds1;
   vendorComboBox.Text = "";
  }

  private void partNumberComboBox_TextChanged(object sender, EventArgs e)
  {
                 
  }

  int xPos;
  int yPos;
  private void partNumberComboBox_TextUpdate(object sender, EventArgs e)
  {
         
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
    this.Left +=  e.X- xPos;
    this.Top +=  e.Y- yPos ;          
    }
   }

   private void closebutton_Click(object sender, EventArgs e)
   {
            System.Windows.Forms.Application.Exit();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

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

        private void label8_MouseMove(object sender, MouseEventArgs e)
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

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void batchsize_Click(object sender, EventArgs e)
        {
         batchsize.Select(0, batchsize.Text.Length);
        }

        private void batchsize_ValueChanged(object sender, EventArgs e)
        {
            batchsize.Select(0, batchsize.Text.Length);
        }
    }
}
