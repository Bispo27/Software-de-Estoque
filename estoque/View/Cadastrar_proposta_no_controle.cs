using Model;
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
    public partial class Cadastrar_proposta_no_controle : Form
    {       
        public Cadastrar_proposta_no_controle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ValidData = true;

            foreach (Control x in this.Controls)
            {
                if (x is TextBox)
                {
                    TextBox txt = x as TextBox;
                    ValidData &= !string.IsNullOrWhiteSpace(txt.Text);
                }
                if (x is ComboBox)
                {
                    ComboBox combo = x as ComboBox;
                    ValidData &= !string.IsNullOrWhiteSpace(combo.Text);
                }
            }
            if (ValidData)
            {
                Cadastrar_Proposta_venda novo = new Cadastrar_Proposta_venda();
                novo.Numero_proposta = textBox1.Text;
                novo.Data_envio = dateTimePicker1.Value;
                novo.Cliente = textBox3.Text;
                novo.Nome_contato = textBox4.Text;
                novo.Telefone = textBox5.Text;
                novo.Email = textBox6.Text;
                novo.Descrição = textBox7.Text;
                novo.Valor = double.Parse(textBox8.Text);
                novo.Observações = textBox9.Text;
            }

        }
    }
}
