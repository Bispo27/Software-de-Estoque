using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace View
{
    public partial class Gráficos_Vendas : Form
    {
        double Prop_aceitas = 20, Prop_n_aceitas = 50, Prop_abertas = 30;

        public Gráficos_Vendas()
        {
            InitializeComponent();
            gera_tres_linhas();
            gera_grafico();
        }

        private void cht_MacroOndas_Click(object sender, EventArgs e)
        {

        }

        public void gera_tres_linhas()
        {
            for (int i = 0; i < 3; i++)
            {
                DataGridViewRow item = new DataGridViewRow();

                item.CreateCells(dataGridView1);
                dataGridView1.Rows.Add(item);

                if (i == 0)
                {
                    dataGridView1.Rows[i].Cells[0].Value = "Propostas Aceitas";
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;

                   // Prop_aceitas = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);    Recebe o valor das células de percentagem e atribui no gráfico
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                }
                if (i == 1)
                {
                    dataGridView1.Rows[i].Cells[0].Value = "Propostas Não Aceitas";
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;

                  //  Prop_n_aceitas = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                }
                if (i == 2)
                {
                    dataGridView1.Rows[i].Cells[0].Value = "Propostas em Aberto";
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;

                   // Prop_abertas = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                }
            }
        }

        public void gera_grafico()
        {
            //Zera configurações padrões do componente
            cht_MacroOndas.Series.Clear();

            //Inicia novas configurações
            cht_MacroOndas.Series.Add("GraficoPizza");

            //Seta o tipo pizza
            cht_MacroOndas.Series["GraficoPizza"].ChartType = SeriesChartType.Pie;

            //Adiciona Valores
            cht_MacroOndas.Series["GraficoPizza"].Points.Add(Prop_aceitas);
            cht_MacroOndas.Series["GraficoPizza"].Points.Add(Prop_n_aceitas);
            cht_MacroOndas.Series["GraficoPizza"].Points.Add(Prop_abertas);

            //Seta descrições da legenda
            cht_MacroOndas.Series["GraficoPizza"].LegendText = "Propostas Aceitas ";
            cht_MacroOndas.Series["GraficoPizza"].LegendText = "Propostas Não Aceitas";
            cht_MacroOndas.Series["GraficoPizza"].LegendText = "Propostas em Aberto";

            //Seta descrição em cada pedaço da pizza
           // cht_MacroOndas.Series["GraficoPizza"].Label = string.Concat("Propostas Aceitas: ",Prop_aceitas);
           // cht_MacroOndas.Series["GraficoPizza"].Label = string.Concat("Propostas Não Aceitas: ", Prop_n_aceitas);
           // cht_MacroOndas.Series["GraficoPizza"].Label = string.Concat("Propostas em Aberto: ", Prop_abertas);

            //Seta Cores de cada pedaço
            cht_MacroOndas.Series["GraficoPizza"].Points[0].Color = System.Drawing.Color.Green;
            cht_MacroOndas.Series["GraficoPizza"].Points[1].Color = System.Drawing.Color.Orange;
            cht_MacroOndas.Series["GraficoPizza"].Points[2].Color = System.Drawing.Color.Red;
        }
    }
}
