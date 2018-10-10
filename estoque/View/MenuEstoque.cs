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
    public partial class MenuEstoque : Form
    {
        public MenuEstoque()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Relatorio menu = new Relatorio();
            menu.Show();
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CadastrarItem menu = new CadastrarItem();
            menu.Show();
            this.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Pesquisa menu = new Pesquisa();
            menu.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Entrada menu = new Entrada();
            menu.Show();
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Saida menu = new Saida();
            menu.Show();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProdutoAcabado menu = new ProdutoAcabado();
            menu.Show();
            this.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ConsultaViabilidade menu = new ConsultaViabilidade();
            menu.Show();
            this.Visible = false;
        }
    }
}