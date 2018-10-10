using Database;
using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace View
{
    public partial class Pesquisa : Form
    {
        bool controle = true;
        string Buscar = "";
        int cont = 1;
        public Pesquisa()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
        }
        public void clean()
        {

            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {

                    dataGridView1.Rows[i].Cells[j].Value = "";
                }
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            clean();
            CollectionEstoque aux = new CollectionEstoque();
            List<produtoHVEX> lista = new List<produtoHVEX>();
            Estoque resultado = new Estoque();
            produtoHVEX Resultado = new produtoHVEX();

            bool flag_busca = true;

            if (!checkBox1.Checked)
            {
                //Estoque aux = new Estoque();

                var builder = Builders<Estoque>.Filter;

                if (codehvex.Text.Length > 0)
                {
                    Buscar = Buscar + codehvex.Text;
                    aux.AddRange(MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.codHVEX.Contains(Buscar)), null));
                    //resultado = aux.First();
                }
                if (textBox2.Text.Length > 0)
                {
                    Buscar = Buscar + textBox2.Text;

                    aux.AddRange(MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.produto.Contains(Buscar)), null));
                    // resultado = aux.First();
                }
                if (textBox3.Text.Length > 0)
                {
                    Buscar = Buscar + textBox3.Text;
                    aux.AddRange(MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.descricao.Contains(Buscar)), null));
                    //resultado = aux.First();
                }
                int MAX = aux.Count();

                if (codehvex.Text.Length == 0 && textBox2.Text.Length == 0 && textBox3.Text.Length == 0) flag_busca = false;
                if (codehvex.Text.Length > 0)
                {
                    for (int i = 0; i < aux.Count(); i++)
                    {
                        /*MessageBox.Show(Buscar);
                        DialogResult = DialogResult.Yes;*/
                        if (aux.ElementAt(i).codHVEX == Buscar || aux.ElementAt(i).descricao == Buscar || aux.ElementAt(i).produto == Buscar)
                        {
                            resultado = aux.ElementAt(i);
                        }
                        else
                        {
                            flag_busca = false;

                        }
                    }

                    if (flag_busca)
                    {
                        imprime(resultado, MAX);
                    }


                }
                else
                {
                    for (int i = 0; i < aux.Count(); i++)
                    {
                        /*MessageBox.Show(Buscar);
                        DialogResult = DialogResult.Yes;*/
                        
                            resultado = aux.ElementAt(i);
                       
                        if (flag_busca)
                        {
                            imprime(resultado, i);
                        }

                    }
                }
                if (aux.Count() == 0)
                {
                    MessageBox.Show("Não existe esse item no estoque");
                    DialogResult = DialogResult.Yes;
                }

            }
            else
            {
             
                if (codehvex.Text.Length > 0)
                {
                    Buscar = codehvex.Text;
                    lista.AddRange(MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.codHVEX.Contains(Buscar)), null));
                  
                }
                if (textBox2.Text.Length > 0)
                {
                    Buscar = Buscar + textBox2.Text;
                    lista.AddRange(MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.nome.Contains(Buscar)), null));
                   
                }
                if (textBox3.Text.Length > 0)
                {
                    Buscar = Buscar + textBox3.Text;
                    lista.AddRange(MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.descricao.Contains(Buscar)), null));
                    
                }
                int MAX = lista.Count();

                if (codehvex.Text.Length == 0 && textBox2.Text.Length == 0 && textBox3.Text.Length == 0) flag_busca = false;
                if (codehvex.Text.Length > 0)
                {
                    for (int i = 0; i < MAX; i++)
                    {
                        /*MessageBox.Show(Buscar);
                        DialogResult = DialogResult.Yes;*/
                        if (lista.ElementAt(i).codHVEX == Buscar || lista.ElementAt(i).descricao == Buscar || lista.ElementAt(i).nome == Buscar)
                        {
                            Resultado = lista.ElementAt(i);
                        }
                        else
                        {
                            flag_busca = false;

                        }
                    }

                    if (flag_busca)
                    {
                        imprime_prod_hvex(Resultado, MAX);
                    }

                }
                else
                {
                    for (int i = 0; i < lista.Count(); i++)
                    {
                       
                       
                            Resultado = lista.ElementAt(i);
                       
                        if (flag_busca)
                        {
                            imprime_prod_hvex(Resultado, MAX);
                        }

                    }
                }
                if (lista.Count() == 0)
                {
                    MessageBox.Show("Não existe esse item no estoque");
                    DialogResult = DialogResult.Yes;
                }
            }

            cont++;
            Buscar = "";
        }

        public void imprime_prod_hvex(produtoHVEX resultado, int i)
        {
                DataGridViewRow item = new DataGridViewRow();
                item.CreateCells(dataGridView1);
                dataGridView1.Rows.Add(item);

                dataGridView1.Rows[i].Cells[1].Value = resultado.nome;
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;

                dataGridView1.Rows[i].Cells[2].Value = resultado.descricao;
                dataGridView1.Rows[i].Cells[2].ReadOnly = true;

                dataGridView1.Rows[i].Cells[3].Value = resultado.setor;
                dataGridView1.Rows[i].Cells[3].ReadOnly = true;
                
                dataGridView1.Rows[i].Cells[6].Value = resultado.quantidade;
                dataGridView1.Rows[i].Cells[6].ReadOnly = true;

                dataGridView1.Rows[i].Cells[7].Value = string.Concat("R$",resultado.preco);
                dataGridView1.Rows[i].Cells[7].ReadOnly = true;
                
                dataGridView1.Rows[i].Cells[9].Value = resultado.codHVEX;
                dataGridView1.Rows[i].Cells[9].ReadOnly = true;

            
        }

        public void imprime(Estoque resultado, int MAX)
        {
                DataGridViewRow item = new DataGridViewRow();
                item.CreateCells(dataGridView1);
                dataGridView1.Rows.Add(item);

                dataGridView1.Rows[MAX].Cells[0].Value = resultado.tipo; // Seta os valores da célula
                dataGridView1.Rows[MAX].Cells[0].ReadOnly = true; //Trava a célula para o usuário não poder modificá-la

                dataGridView1.Rows[MAX].Cells[1].Value = resultado.produto;
                dataGridView1.Rows[MAX].Cells[1].ReadOnly = true;

                dataGridView1.Rows[MAX].Cells[2].Value = resultado.descricao;
                dataGridView1.Rows[MAX].Cells[2].ReadOnly = true;

                dataGridView1.Rows[MAX].Cells[3].Value = resultado.setor;
                dataGridView1.Rows[MAX].Cells[3].ReadOnly = true;

                dataGridView1.Rows[MAX].Cells[4].Value = resultado.fabricante;
                dataGridView1.Rows[MAX].Cells[4].ReadOnly = true;

                dataGridView1.Rows[MAX].Cells[5].Value = resultado.fornecedor;
                dataGridView1.Rows[MAX].Cells[5].ReadOnly = true;

                dataGridView1.Rows[MAX].Cells[6].Value = resultado.quantidade;
                dataGridView1.Rows[MAX].Cells[6].ReadOnly = true;

                dataGridView1.Rows[MAX].Cells[7].Value = string.Concat("R$",resultado.Preco);
                dataGridView1.Rows[MAX].Cells[7].ReadOnly = true;

                dataGridView1.Rows[MAX].Cells[8].Value = resultado.partNumb;
                dataGridView1.Rows[MAX].Cells[8].ReadOnly = true;

                dataGridView1.Rows[MAX].Cells[9].Value = resultado.codHVEX;
                dataGridView1.Rows[MAX].Cells[9].ReadOnly = true;

        }

       private void textBox1_TextChanged_1(object sender, EventArgs e)
       {
            codehvex.CharacterCasing = CharacterCasing.Upper;
            if (codehvex.Text.Length == 0)
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                
            }
            else
            {
                textBox2.Enabled = false;
                textBox3.Enabled = false;
            }
            dataGridView1.ReadOnly = true;
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            textBox2.CharacterCasing = CharacterCasing.Upper;
            if (textBox2.Text.Length == 0)
            {
                codehvex.Enabled = true;
                textBox3.Enabled = true;
            }
            else
            {
                codehvex.Enabled = false;
                textBox3.Enabled = false;
            }
            dataGridView1.ReadOnly = true;
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            textBox3.CharacterCasing = CharacterCasing.Upper;
            if (textBox3.Text.Length == 0)
            {
                codehvex.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                codehvex.Enabled = false;
                textBox2.Enabled = false;
            }
            dataGridView1.ReadOnly = true;

        }

        
        

        private void button5_Click(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clean();
            if (checkBox1.Checked)
            {
                var a = MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.nome.Contains("")), null);
                for (int i = 0; i < a.Count(); i++)
                {
                    imprime_prod_hvex(a.ElementAt(i), i);
                }
            }
            else
            {
                var a = MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.produto.Contains("")), null);
                for (int i = 0; i < a.Count(); i++)
                {
                    imprime(a.ElementAt(i), i);
                }
            }
            

        }
    }
}