using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FAI
{
  public partial class PDFviewer : Form
  {
   Partnumberselection pnsg;
   ComboBox[] comboboxinspections;
   NumericUpDown[] numericinspections;
   Label[] labeling;
   string[] order;
   int samples=1;
   public PDFviewer(Partnumberselection pns, string partnumber,string rev,string[] dimensions,string[] tolerances)
   { 
    int ncounting=0;
    int scounting=0;
    int usedcombo=0;
    int usedtext=0;
    order=new string[5];
    pnsg = pns;

    InitializeComponent();

    axAcroPDF1.LoadFile(@"C:\Users\Miguel Rodmen\Desktop\LightCareFiles\incomingFiles\Drawings\"+partnumber+rev+".pdf");
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

    labeling= new Label[scounting+ncounting];
    comboboxinspections= new ComboBox[scounting];
    numericinspections = new NumericUpDown[ncounting];

    for(int i=0;i<ncounting;i++)
    {
     numericinspections[i]=new NumericUpDown();
     numericinspections[i].Controls[0].Hide();
     numericinspections[i].Controls[1].Width=Width-4;
    }

    for(int i=0;i<scounting;i++)
    {
     comboboxinspections[i]=new ComboBox();
    }

    for(int i=0;i<(scounting+ncounting);i++)
    {
     labeling[i]= new Label();
     this.Controls.Add(labeling[i]);
     labeling[i].Text="Dimension "+(i+1).ToString();
     labeling[i].Left=770;
     labeling[i].Top=32+(55*(i));

     if (order[i]=="string")
     {
      this.Controls.Add(comboboxinspections[usedcombo]);
      comboboxinspections[usedcombo].Name="Dimension "+(i+1).ToString();
      comboboxinspections[usedcombo].Left= 770;
      comboboxinspections[usedcombo].Top=(55*(i+1));
      comboboxinspections[usedcombo].TabIndex=i;
      comboboxinspections[usedcombo].TabStop=true;
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
      numericinspections[usedtext].Top=(55*(i+1));
      numericinspections[usedtext].DecimalPlaces=2;
      numericinspections[usedtext].TabIndex= i;
      numericinspections[usedtext].TabStop=true;
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
   }

   private void PDFviewer_FormClosed(object sender, FormClosedEventArgs e)
   {
    pnsg.Show();
    pnsg.partNumberComboBox.Select();
   }

   private void submitButton_Click(object sender, EventArgs e)
   {
    samples++;
    sampleslabel.Text="SAMPLE #"+samples.ToString()+"/3";
   }

   private void PDFviewer_Shown(object sender, EventArgs e)
   {
    if(order[0]=="numeric")
    {
     numericinspections[0].Select();
     this.ActiveControl= numericinspections[0];
     numericinspections[0].Focus();
    }
    else
    {
     comboboxinspections[0].Select();
     this.ActiveControl= comboboxinspections[0];
     comboboxinspections[0].Focus();
    }    
   }
  }
 }
