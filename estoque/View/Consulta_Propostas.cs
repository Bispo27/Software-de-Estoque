using Database;
using Model;
using MongoDB.Driver;
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
    public partial class Consulta_Propostas : Form
    {
        string Buscar = "";
        public Consulta_Propostas()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
        }

        private void Controle_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {

            Estoque aux = new Estoque();

            if (textBox1.Text.Length > 0)
            {
                aux = MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.codHVEX.Equals(Buscar)), null).First();
            }
            if (textBox2.Text.Length > 0)
            {
                aux = MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.produto.Contains(Buscar)), null).First();
            }
            if (textBox3.Text.Length > 0)
            {
                aux = MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.descricao.Contains(Buscar)), null).First();
            }

            for (int i = 0; i < 1; i++)
            {
                DataGridViewRow item = new DataGridViewRow();

                item.CreateCells(dataGridView1);
                dataGridView1.Rows.Add(item);

               /* dataGridView1.Rows[i].Cells[0].Value = aux.tipo; // Seta os valores da célula
                dataGridView1.Rows[i].Cells[0].ReadOnly = true; //Trava a célula para o usuário não poder modificá-la

                dataGridView1.Rows[i].Cells[1].Value = aux.produto;
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;

                dataGridView1.Rows[i].Cells[2].Value = aux.descricao;
                dataGridView1.Rows[i].Cells[2].ReadOnly = true;

                dataGridView1.Rows[i].Cells[3].Value = aux.setor;
                dataGridView1.Rows[i].Cells[3].ReadOnly = true;

                dataGridView1.Rows[i].Cells[4].Value = aux.fabricante;
                dataGridView1.Rows[i].Cells[4].ReadOnly = true;

                dataGridView1.Rows[i].Cells[5].Value = aux.fornecedor;
                dataGridView1.Rows[i].Cells[5].ReadOnly = true;

                dataGridView1.Rows[i].Cells[6].Value = aux.quantidade;
                dataGridView1.Rows[i].Cells[6].ReadOnly = true;

                dataGridView1.Rows[i].Cells[7].Value = aux.Preco;
                dataGridView1.Rows[i].Cells[7].ReadOnly = true;

                dataGridView1.Rows[i].Cells[8].Value = aux.partNumb;
                dataGridView1.Rows[i].Cells[8].ReadOnly = true;

                dataGridView1.Rows[i].Cells[9].Value = aux.codHVEX;
                dataGridView1.Rows[i].Cells[9].ReadOnly = true;

                dataGridView1.Rows[i].Cells[10].Value = aux.codHVEX;
                dataGridView1.Rows[i].Cells[10].ReadOnly = true;

                dataGridView1.Rows[i].Cells[11].Value = aux.codHVEX;
                dataGridView1.Rows[i].Cells[11].ReadOnly = true;
                */
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                Buscar += textBox3.Text;
            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
            }
            dataGridView1.ReadOnly = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                textBox1.Enabled = true;
                textBox3.Enabled = true;
                Buscar += textBox2.Text;
            }
            else
            {
                textBox1.Enabled = false;
                textBox3.Enabled = false;
            }
            dataGridView1.ReadOnly = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                Buscar += textBox1.Text;
            }
            else
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
            }
            dataGridView1.ReadOnly = true;
        }
    }
}
