using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace FAI
{
 public partial class PDFviewer : Form
 {
  OleDbConnection con;
  bool accept= true;
  DateTime thisDay;
  Partnumberselection pnsg;
  ComboBox[] comboboxinspections;
  NumericUpDown[] numericinspections;
  Label[] labeling;
  string[] order;
  int samples=1;
  Excel.Application oXL;
  Excel._Workbook oWB;
  Excel._Worksheet oSheet;
  Excel.Range oRng;
  string partnumber;
  string rev;
  string description;
  string[] dimensions;
  string[] tolerances;
  string vendor;
  string batchsize;
  string batchnum;
  string[,] samplings; 
  int ncounting;
  int scounting;
  bool[] finaldisposition;
  string user;
  public PDFviewer(Partnumberselection pns,string partnumber,string rev, string description, string[] dimensions,string[] tolerances, string vendor, string batchsize,string batchnum,string user1)
  {
   user= user1;
   this.partnumber = partnumber;
   this.rev= rev;
   this.description = description;
   this.dimensions=dimensions;
   this.tolerances= tolerances;
   this.vendor= vendor;
   this.batchsize=batchsize;
   this.batchnum=batchnum;

   ncounting =0;
   scounting=0;
   int usedcombo=0;
   int usedtext=0;
   order=new string[5];
   pnsg = pns;

   InitializeComponent();
   axAcroPDF1.LoadFile(@"\\WINDELL-GINKJNK\LightCareFiles\incomingFiles\Drawings\" + partnumber+rev+".pdf");
   axAcroPDF1.setPageMode("PDUseNone: 1");
   axAcroPDF1.setPageMode("pageMode:1");
   axAcroPDF1.setPageMode("1");
   axAcroPDF1.setPageMode("none");
   axAcroPDF1.setShowScrollbars(false);
   axAcroPDF1.setShowToolbar(false);
   axAcroPDF1.setLayoutMode("none");
   sampleslabel.Text="SAMPLE #"+samples.ToString()+"/3";
   for(int i=0;i<5;i++)
   {
    if(dimensions[i]!="N/A")
    {
     if(IsNumeric(dimensions[i]))
     {
      ncounting++;
      order[i]="numeric";
     }
     else
     {
      scounting++;
      order[i]="string";
     }
    }
   }
   samplings= new string[3,(ncounting+scounting)];
   finaldisposition= new bool[ncounting+scounting];
   labeling= new Label[scounting+ncounting];
   comboboxinspections= new ComboBox[scounting];
   numericinspections = new NumericUpDown[ncounting];

   for(int i=0;i<ncounting;i++)
   {
    numericinspections[i]=new NumericUpDown();
    numericinspections[i].BackColor= Color.FromArgb(48, 44, 52); 
    numericinspections[i].Controls[0].Hide();
    numericinspections[i].Controls[1].Width=Width-4;
    numericinspections[i].ForeColor=Color.White;
   }

    for(int i=0;i<scounting;i++)
    {
     comboboxinspections[i]=new ComboBox();
     comboboxinspections[i].BackColor= Color.FromArgb(48, 44, 52); 
    }

    for(int i=0;i<(scounting+ncounting);i++)
    {
     labeling[i]= new Label();
     this.Controls.Add(labeling[i]);
     labeling[i].Text="Dimension "+(i+1).ToString();
     labeling[i].Left=770;
     labeling[i].Top=62+(85*(i));
     labeling[i].ForeColor=Color.White;
     labeling[i].Font = new Font("Belle Sans Ultra Cond Bd", labeling[i].Font.Size);

     if (order[i]=="string")
     {
      this.Controls.Add(comboboxinspections[usedcombo]);
      comboboxinspections[usedcombo].Name="Dimension "+(i+1).ToString();
      comboboxinspections[usedcombo].Left= 770;
      comboboxinspections[usedcombo].Top=(85*(i+1));
      comboboxinspections[usedcombo].TabIndex=i;
      comboboxinspections[usedcombo].TabStop=true;
      comboboxinspections[usedcombo].ForeColor=Color.White;
      comboboxinspections[usedcombo].Select(0,comboboxinspections[usedcombo].Text.Length);
      if(i==0)
      {
       comboboxinspections[i].Select();
      }
      usedcombo++; 
     }
     else 
     {
      this.Controls.Add(numericinspections[usedtext]);
      numericinspections[usedtext].Name= "Dimension "+(i+1).ToString();
      numericinspections[usedtext].Left=770;
      numericinspections[usedtext].Top=(85*(i+1));
      numericinspections[usedtext].DecimalPlaces=2;
      numericinspections[usedtext].TabIndex= i;
      numericinspections[usedtext].TabStop=true;
      numericinspections[usedtext].Maximum = 1000000;
      numericinspections[usedtext].Controls[0].Hide();
      numericinspections[usedtext].Controls[1].Width=Width-4;
      numericinspections[usedtext].Select(0, numericinspections[usedtext].Text.Length);
      numericinspections[usedtext].MouseClick += Commoncontrolselector;
                        
      if(i==0)
      {
       numericinspections[i].Select();
      }
      usedtext++;
     }
    }
   }
   
   public bool IsNumeric(string value)
   {
    double myNum;
    return Double.TryParse(value, out myNum);
   }
   private void PDFviewer_Load(object sender, EventArgs e)
   {
    pnsg.Hide();
    waitingforpdf.Start();
    labelName.Text = user;
   }

   private void PDFviewer_FormClosed(object sender, FormClosedEventArgs e)
   {
    pnsg.Show();
    pnsg.partNumberComboBox.Select();
   }

   private void submitButton_Click(object sender, EventArgs e)
   {
    samples++;

    for (int i=0; i<(ncounting + scounting);i++)
    {
     Control ctn = this.Controls["Dimension " + (i + 1).ToString()];
     samplings[samples-2,i]=ctn.Text;
     var ctnnumeric= (dynamic)null;
        
     if(ctn is NumericUpDown)
     {
      ctnnumeric=(NumericUpDown)ctn;
      ctnnumeric.Value=0;               
      ctnnumeric.Select(0, ctnnumeric.Text.Length);
     }
     else
     {
      ctnnumeric=(TextBox)ctn ;
      ctnnumeric.Text="";
     }
    }
    
    if (samples>3)
    {
     MessageBox.Show("Sampling Finished");
     DateTime thisDay = DateTime.Today;
     string strSqlinsertdata = "INSERT INTO incomingRecordsTable (Partnumber, Dimension1, Status1, Dimension2, Status2, Dimension3, Status3, Dimension4, Status4, Dimension5, Status5, Batch, Dating, Status, Comments, Inspector, Vendor, Batchsize, Rev) VALUES (";
     try
     {
      con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\WINDELL-GINKJNK\LightCareFiles\incomingFiles\incomingDatabase.accdb");
      try
      {
       con.Open();
      }
      catch
      {
      MessageBox.Show("Database Failed");
      }  


      this.Close();
      string path = @"\\WINDELL-GINKJNK\LightCareFiles\F-826-001-A Incoming Inspection.xlsx";
      oXL = new Excel.Application();
      oXL.Visible = false;
      oXL.DisplayAlerts = false;
      Excel.Workbook mWorkBook;
      Excel.Sheets mWorkSheets;
      Excel.Worksheet mWSheet1;
      mWorkBook = oXL.Workbooks.Open(path, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true,
      false, 0, true, false, false);
      mWorkSheets = mWorkBook.Worksheets;
      mWSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)mWorkSheets.get_Item("Report");

/////tu string se llena aqui 

      mWSheet1.Cells[4,4].value =partnumber;
      mWSheet1.Cells[4,13].value = rev;
      mWSheet1.Cells[4,16].value= description;   
      mWSheet1.Cells[5,16].value=vendor;
      mWSheet1.Cells[5,20].value= batchnum;
      mWSheet1.Cells[5,23].value  =batchsize;          
      mWSheet1.Cells[43,13].value = thisDay.ToString("d");



      for (int i=0;i<dimensions.Length; i++)
      {
       if(dimensions[i]!="N/A")
       {
        mWSheet1.Cells[8+(i*2),2].value =dimensions[i]; 
       } 
      } 

      for(int i=0; i<tolerances.Length;i++)
      {
       if(tolerances[i]!="N/A")
       { 
        if(order[i]=="numeric")
        {
         int indexs=tolerances[i].IndexOf("/"); 
         string sminimum=tolerances[i].Substring(0,indexs);
         string smaximum=tolerances[i].Substring(indexs+1);
         double minimum = Convert.ToDouble(sminimum);
         double maximum = Convert.ToDouble(smaximum);
         maximum=Convert.ToDouble(dimensions[i])+maximum;
         minimum=Convert.ToDouble(dimensions[i])-minimum;
         mWSheet1.Cells[8+(i*2),3].value=maximum.ToString();
         mWSheet1.Cells[9+(i*2),3].value=minimum.ToString();
                                 
         for(int j=0; j<3;j++)
         {
                                    
          mWSheet1.Cells[8+(i*2),4+(j*3)].value=samplings[j,i];
          if((Convert.ToDouble(samplings[j, i])<=maximum) && (Convert.ToDouble(samplings[j, i]) >= minimum))
          {
           mWSheet1.Cells[8 + (i * 2), 4 + (j * 3)].Font.Color= System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
          }
          else
          {
            accept = false;
            mWSheet1.Cells[8 + (i * 2), 4 + (j * 3)].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
          }
          

          string sampla = samplings[j, i];
         }

         if(accept)
         {
          mWSheet1.Cells[8 + (i * 2), 16].value = "N/A";
          mWSheet1.Cells[8 + (i * 2), 17].value="X";
          mWSheet1.Cells[8 + (i * 2), 18].value = " ";
          mWSheet1.Cells[8 + (i * 2), 13].value = "PASSED";
          mWSheet1.Cells[8 + (i * 2), 19].value = "STORAGE";
          finaldisposition[i]= true;
         }
         else
         {
          finaldisposition[i]= false;
          mWSheet1.Cells[8 +(i * 2), 18].value="X";
          mWSheet1.Cells[8 +(i * 2), 17].value = " ";
         }
        }
       }
      }     
      bool yn= true;
      for(int i=0;i<(ncounting+scounting);i++)
      {
       if(!finaldisposition[i])
       {
        yn=false;
       }
      }          
      if(yn)
      {
       mWSheet1.Cells[44, 20].value = "X";
       mWSheet1.Cells[45, 20].value = " ";
       mWSheet1.Cells[39, 3].value = "N/A";
      }        
      else
      {
       mWSheet1.Cells[45, 20].value = "X";
       mWSheet1.Cells[44, 20].value = " ";
      }

      mWSheet1.Cells[43, 3].value = user;
      // mWSheet1.PrintOutEx(Type.Missing, Type.Missing, Type.Missing, Type.Missing,
      //Type.Missing, Type.Missing, Type.Missing, Type.Missing);
      mWorkBook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, @"\\WINDELL-GINKJNK\LightCareFiles\incomingReports\" + partnumber+".pdf");
      mWorkBook.Close();

      OleDbCommand insertcommanddata = new OleDbCommand(strSqlinsertdata, con);
      int result = insertcommanddata.ExecuteNonQuery();
      if (result < 0)
      {
       MessageBox.Show("Error writing to query");
      }
      this.Close();

     }
     catch
     {
      MessageBox.Show("Excel Failed");
     }
    }
    else
    {
     sampleslabel.Text="SAMPLE #"+samples.ToString()+"/3";
     Control ctn = this.Controls["Dimension 1"];
     NumericUpDown ctnnumeric=(NumericUpDown)ctn;
     ctn.Select();
     ctnnumeric.Select(0, ctnnumeric.Text.Length);
     
    }
   }
   private void waitingforpdf_Tick(object sender, EventArgs e)
   {
    if (order[0] == "numeric")
    {
     numericinspections[0].Select();
     this.ActiveControl = numericinspections[0];
     numericinspections[0].Focus();
    }
    else
    {
     comboboxinspections[0].Select();
     this.ActiveControl = comboboxinspections[0];
     comboboxinspections[0].Focus();
    }
    this.ActiveControl.Select();
    this.ActiveControl.Focus();
    waitingforpdf.Stop();
   }

   private void closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        int xPos;
        int yPos;
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

        private void label8_MouseMove(object sender, MouseEventArgs e)
        {
         {
          if (e.Button == System.Windows.Forms.MouseButtons.Left)
          {
           this.Left += e.X - xPos;
           this.Top += e.Y - yPos;
          }
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
        private void Commoncontrolselector(object sender, EventArgs e)
        {
         Control ctn = sender as Control;
         NumericUpDown ctnnumeric = (NumericUpDown)ctn;
         ctnnumeric.Select(0, ctn.Text.Length); 
        }
    }

      
    }
