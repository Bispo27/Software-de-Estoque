using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class menuVendas : Form
    {
        public menuVendas()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Gráficos_Vendas menu = new Gráficos_Vendas();
            menu.Show();
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            novaProposta menu = new novaProposta();
            menu.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cadastrar_proposta_no_controle menu = new Cadastrar_proposta_no_controle();
            menu.Show();
            this.Visible = false;
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            Consulta_Propostas menu = new Consulta_Propostas();
            menu.Show();
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Cobrança_de_propostas_em_aberto menu = new Cobrança_de_propostas_em_aberto();
            menu.Show();
            this.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Faturamento menu = new Faturamento();
            menu.Show();
            this.Visible = false;
        }
    }
}
