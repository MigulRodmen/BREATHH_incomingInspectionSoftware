using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
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
   int index = partNumberComboBox.Items.IndexOf(partNumberComboBox.Text);
          
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

     for (int i=0;i<5;i++)
     {
      dimensions[i]= dsrev.Tables[0].Rows[0][3+(i*2)].ToString();
      tolerances[i]= dsrev.Tables[0].Rows[0][4+(i*2)].ToString();
     }
     con.Close();
        
     PDFviewer viewer = new PDFviewer(this,partNumberComboBox.Text,rev,dimensions,tolerances);
     partNumberComboBox.SelectedIndex = -1;
     partNumberComboBox.Text="";
     viewer.Show();
    }
    else
    {
     MessageBox.Show("Missing");
     partNumberComboBox.Select();
    }
   }
   else
   {
    MessageBox.Show("Invalid part number");
    partNumberComboBox.Text = "";
    partNumberComboBox.Select();
    batchTextBox.Text="";
   }
  }    

  private void Partnumberselection_Load(object sender, EventArgs e)
  {
   con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\WINDELL-GINKJNK\LightCareFiles\incomingfiles\incomingDatabase.accdb");
   try
   {
    con.Open();
   }
   catch
   {
    MessageBox.Show("Database Failed");
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
  }

  private void partNumberComboBox_TextChanged(object sender, EventArgs e)
  {
                 
  }

  private void partNumberComboBox_TextUpdate(object sender, EventArgs e)
  {
         
  }
 }
}
