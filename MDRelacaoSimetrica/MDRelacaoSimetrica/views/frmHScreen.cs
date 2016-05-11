/**
 * **********************************************************************************************************
 * @copyright   (c) 2016 - g3ferreira      
 * @brief      PAA Merge Sort Trabalho
 *                                                                                                    
 * @details                                                                                      
 *                                                                                                    
 * @author      Genilson Ferreira <gr.ferreira@live.com>                                                                
 * @since       Abr 12, 2016                                                                     
 *                                                                                                    
 * @sa           
 **********************************************************************************************************
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Xml;
using System.Runtime.InteropServices;
using System.IO;
using System.Net.Sockets;
using MDRelacaoSimetrica.controllers;
using System.Threading;
using System.Diagnostics;

namespace MDRelacaoSimetrica
{
    public partial class frmHomeScreen : Form
    {
        public static System.Timers.Timer _timer = new System.Timers.Timer();

        public static Stopwatch cronometroWatch = new Stopwatch();
        public static string timeElapsed = string.Empty;
        public Thread threadMergeSort;

        public frmHomeScreen()
        {
            InitializeComponent();
        }



        public void atualiazarCronometroView(string timeElapsed)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(atualiazarCronometroView), new object[] { timeElapsed });
                return;
            }

        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            //txtPathVMI.Text = fbd.SelectedPath;
            // string[] files = Directory.GetFiles(fbd.SelectedPath);
            //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            //  tagXml.generateReport(docXML, "REPORT","001"); //gera o relatorio
            // tagXml.AtualizaValorNodo(docXML,"COMPONENT", "777%");
            // tagXml.setValueQR1Report(docXML, "COMPONENT", 0, "350%"); change value for QR1 tag

            // fillDataGrid();

            //  RepositoryWatcher.Run(@"C:\config-autovmi\pasta");

        }

        private void btnPathAOI_Click(object sender, EventArgs e)
        {
            txtN.Text = selectPath();
        }

        public String selectPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;

            DialogResult result = openFileDialog.ShowDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            return openFileDialog.FileName;
        }

        public string selectPathArquivoDesordenado()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            return fbd.SelectedPath;
        }

        private void btnPathVMI_Click(object sender, EventArgs e)
        {
            txtM.Text = selectPathArquivoDesordenado();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            txtLog1.Text = string.Empty;
            txtLog2.Text = string.Empty;
            //simuladResultados();
            if (!(validarParametros(Convert.ToInt32(txtN.Text), Convert.ToInt32(txtM.Text), Convert.ToInt32(txtQ.Text))))
            {
                if( Convert.ToInt32(txtQ.Text) ==   (Math.Pow(Convert.ToInt32(txtM.Text),2)))
                {
                    lblResultado.Text = "1 - Pares Ordenado Simétricos";
                }

                simetria(Convert.ToInt32(txtN.Text), Convert.ToInt32(txtM.Text), Convert.ToInt32(txtQ.Text));
            }

        }
        public bool validarParametros(int n, int m, int q)
        {

            bool ret = false;
            if (m < 0)
            {
                ret = true;
                MessageBox.Show("M deve ser maior ou igual a 0");

            }
            if (n > (Math.Pow(10, 2)))
            {
                ret = true;
                MessageBox.Show("N deve ser menor ou igual a:   " + Math.Pow(10, 2));
            }
            if (q < 1)
            {
                ret = true;
                MessageBox.Show("Q deve ser maior ou igual a 1");
            }
            if (q > (Math.Pow(10, 4)))
            {
                ret = true;
                MessageBox.Show("N deve ser menor ou igual a:    " + Math.Pow(10, 4));
            }
            if (q > (Math.Pow(m, 2)))
            {
                ret = true;
                MessageBox.Show("Q deve ser menor ou igual a:   " + Math.Pow(m, 2));
            }

            return ret;
        }
        public void simetria(int n, int m, int q)
        {
            
            int m2 = m;
            int n2 = n;
            if (q <= m2)
            {
                for (int i = n2; i <= q; i++)
                {
                    txtLog1.Text += n2 + "-" + n2;
                    txtLog1.Text += Environment.NewLine;

                    n2++;
                }
            }
            else
            {
                for (int i = n2; i <= m; i++)
                {
                    txtLog1.Text += n2 + "-" + n2;
                    txtLog1.Text += Environment.NewLine;

                    n2++;
                }
                n2 = n;
                m2 = m;
                for (int i = n2; i < m; i++)
                {
                    txtLog1.Text += n2 + "-" + (i + 1);
                    txtLog1.Text += Environment.NewLine;

                }

                n2 = n;
                m2 = m;
                for (int i = n2; i <= (q - m); i++)
                {
                    if (!(i > m))
                    {
                        if (!(m2 == i))
                        {
                            txtLog1.Text += m2 + "-" + i;
                            txtLog1.Text += Environment.NewLine;
                        }
                    }
                    else
                    {
                        if (!((m2-1) == (i-m)))
                        {
                            txtLog1.Text += (m2 - 1) + "-" + (i - m);
                            txtLog1.Text += Environment.NewLine;
                        }
                    }
                }


            }


        }

        public void recursivo(int n, int m)
        {
            int j = n;
            for (int i = n; i <= m; i++)
            {
                txtLog1.Text += n + "-" + j;
                txtLog1.Text += Environment.NewLine;
                j++;
            }
        }

        public void CreateMyMultilineTextBox()
        {
            txtLog1.Multiline = true;
            txtLog1.ScrollBars = ScrollBars.Vertical;
            txtLog1.AcceptsReturn = true;
            txtLog1.AcceptsTab = true;
            txtLog1.WordWrap = true;
            txtLog2.Multiline = true;
            txtLog2.ScrollBars = ScrollBars.Vertical;
            txtLog2.AcceptsReturn = true;
            txtLog2.AcceptsTab = true;
            txtLog2.WordWrap = true;

        }







        public void disableEnableFieldsHScreen(bool enableDisable)
        {
            txtN.Enabled = enableDisable;
            txtM.Enabled = enableDisable;

        }

        public bool validateParameters()
        {

            if (!(txtN.Text.Equals(string.Empty) || txtN.Text.Equals(string.Empty)))
            {
                return true;

            }
            else
            {
                return false;
            }

        }

        private void frmHomeScreen_Load(object sender, EventArgs e)
        {
            CreateMyMultilineTextBox();
            txtLog1.Height = 350;
            txtLog2.Height = 350;

            txtN.Text = @"1";
            txtM.Text = @"3";
            txtQ.Text = "2";
            lblResultado.Text = string.Empty;


        }

        public void simuladResultados()
        {
            lblResultado.Text = "0 - Fecho Não Simétrico";
            txtLog1.Text += "1,1";
            txtLog1.Text += Environment.NewLine;
            txtLog1.Text += "1,2";
            txtLog1.Text += Environment.NewLine;
            txtLog2.Text += "1,1";
            txtLog2.Text += Environment.NewLine;
            txtLog2.Text += "1,2";
            txtLog2.Text += Environment.NewLine;
            txtLog2.Text += "1,3";
            txtLog2.Text += Environment.NewLine;
            txtLog2.Text += "2,1";
            txtLog2.Text += Environment.NewLine;
            txtLog2.Text += "2,2";
            txtLog2.Text += Environment.NewLine;
            txtLog2.Text += "2,3";
            txtLog2.Text += Environment.NewLine;
            txtLog2.Text += "3,1";
            txtLog2.Text += Environment.NewLine;
            txtLog2.Text += "3,2";
            txtLog2.Text += Environment.NewLine;
            txtLog2.Text += "3,3";



        }






    }
}
