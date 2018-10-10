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
    public partial class Cobrança_de_propostas_em_aberto : Form
    {
        public Cobrança_de_propostas_em_aberto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int MAX = dataGridView1.Columns.Count;

            for(int i = 0; i < MAX; i++)
            {
                if (dataGridView1.Rows[i].Cells[11].Value == "sim" || dataGridView1.Rows[i].Cells[11].Value == "Sim" || dataGridView1.Rows[i].Cells[11].Value == "SIM")
                {
                    string num_proposta = dataGridView1.Rows[i].Cells[0].ToolTipText;
                    cobrar(num_proposta);
                }
            }
        }

        public void cobrar(string num_proposta)
        {

        }
    }
}
