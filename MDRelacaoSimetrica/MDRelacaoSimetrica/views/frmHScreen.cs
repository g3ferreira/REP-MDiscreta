/**
 * **********************************************************************************************************
 * @copyright   (c) 2016 - GitHub: g3ferreira      
 * @brief     Simetria entre pares ordenados
 *                                                                                                    
 * @details                                                                                      
 *                                                                                                    
 * @author      Genilson Ferreira <gr.ferreira@live.com>                                                                
 * @since       Mai 16, 2016                                                                     
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

        public frmHomeScreen()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            txtLog1.Text = string.Empty;
            txtLog2.Text = string.Empty;

            if (!(validarParametros(Convert.ToInt32(txtN.Text), Convert.ToInt32(txtM.Text), Convert.ToInt32(txtQ.Text))))
            {
                if ((Convert.ToInt32(txtQ.Text) == (Math.Pow(Convert.ToInt32(txtM.Text), 2))) || Convert.ToInt32(txtQ.Text) == 1)
                {
                    lblResultado.ForeColor = Color.Green;
                    lblResultado.Text = "1 - Pares Ordenados Simétricos";
                    criarParesOrdenados(Convert.ToInt32(txtN.Text), Convert.ToInt32(txtM.Text), Convert.ToInt32(txtQ.Text));
                }
                else
                {
                    lblResultado.ForeColor = Color.Red;
                    lblResultado.Text = "0 - Pares Ordenados Não Simétricos";                    
                    criarParesOrdenados(Convert.ToInt32(txtN.Text), Convert.ToInt32(txtM.Text), Convert.ToInt32(txtQ.Text));
                    txtLog2.Text = txtLog1.Text;
                    simetria();
                }
            }
        }


        /*
         * Metodo que irá exibir a simetria dos pares ordenados na tela 
         * 
         * 1 - pega os pares ordenados e remove os espacos em branco
         * 2 - criar os pares ordenados simetricos a partir do pares ordenados
         * 3 - remover todos os pares repetidos da lista de pares simetricos. Compara-se a os pares ordenados com os pares ordenados simetricos se os pares forem iguais, remove-se um.
         * 4 - listar os pares ordenados simetricos na tela
         * 
         */

        public void simetria()
        {
            string[] paresOrdenados = txtLog1.Text.Split();
            List<string> listaPares = new List<string>();
            List<string> listaParesSimetricos = new List<string>();

            listaPares = listarParesOrdenados(paresOrdenados);
            listaParesSimetricos = listarParesSimetricos(listaPares);
            listaParesSimetricos = removerParesSimetricosIguais(listaPares, listaParesSimetricos);
            listarParesSimetricosView(listaParesSimetricos);

        }

        //metodo que monta os pares ordenados 
        public List<string> listarParesOrdenados(string[] paresOrdenados)
        {
            List<string> listaParesOrdenados = new List<string>();
            for (int i = 0; i < paresOrdenados.Length; i++)
            {
                if (!(paresOrdenados[i].Equals("")))
                {
                    listaParesOrdenados.Add(paresOrdenados[i]);
                }
            }

            return listaParesOrdenados;
        }

        //metodo que monta os pares ordenados simetricos a partir dos pares ordenados
        public List<string> listarParesSimetricos(List<string> listaParesOrdenados)
        {
            string parSimetrico = string.Empty;
            List<string> listaParesSimetricos = new List<string>();
            foreach (var item in listaParesOrdenados)
            {
                string[] parSplitArray = item.Split('-');
                parSimetrico += parSplitArray[1] + "-";
                parSimetrico += parSplitArray[0];
                listaParesSimetricos.Add(parSimetrico);
                parSimetrico = string.Empty;
            }

            return listaParesSimetricos;
        }

        // metodo que remove os pares ordenados repetidos  
        public List<string> removerParesSimetricosIguais(List<string> listaParesOrdenados, List<string> listaParesSimetricos)
        {

            for (int i = 0; i < listaParesOrdenados.Count; i++)
            {
                if (listaParesSimetricos.Contains(listaParesOrdenados[i]))
                {
                    listaParesSimetricos.Remove(listaParesOrdenados[i]);
                }
            }

            return listaParesSimetricos;

        }

        //metodo para exibir os pares ordenados simétricos na tela
        public void listarParesSimetricosView(List<string> listaParesSimetricos)
        {
            foreach (var item in listaParesSimetricos)
            {
                txtLog2.Text += item;
                txtLog2.Text += Environment.NewLine;
            }

        }

        //metodo que monta os pares ordenados e os exibe na tela
        public void montarParOrdenado(int x, List<int> listaN, int quantidadeCombinacoes)
        {
            int y = 0; ;
            for (int i = 0; i <= quantidadeCombinacoes; i++)
            {
                if (i >= listaN.Count) break;
                y = listaN[i];
                if (!(x == y))
                {
                    Console.WriteLine("par: " + x + "-" + y);
                    txtLog2.Text += x + "-" + y;
                    txtLog2.Text += Environment.NewLine;
                }

            }
        }
        
        //metodo para validar parametros 
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
            lblResultado.Text = string.Empty;
            return ret;
        }

        //criar pares ordenados não simetricos e exibir os mesmos na tela
        public int parOrdenadoNaoSimetrico(int x, List<int> listaN, int limitePares, int contPares)
        {
            int y = 0;
            for (int i = 0; i <= listaN.Count; i++)
            {
                if (contPares <= limitePares)
                {
                    if (i >= listaN.Count) break;
                    y = listaN[i];
                    txtLog1.Text += x + "-" + y;
                    txtLog1.Text += Environment.NewLine;
                    contPares++;
                }
            } 

            return contPares;
        }

        //pegar os parametros de N, M e Q e criar os pares ordenados
        public void criarParesOrdenados(int n, int m, int q)
        {
            int contParOrdenado = 1;
            List<int> listaNumeros = new List<int>();
            for (int i = n; i <= m; i++)
            {
                listaNumeros.Add(i);

            }
            for (int i = 0; i < listaNumeros.Count; i++)
            {
                if (contParOrdenado <= q) contParOrdenado = parOrdenadoNaoSimetrico(listaNumeros[i], listaNumeros, q, contParOrdenado);

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
            txtM.Text = @"4";
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
