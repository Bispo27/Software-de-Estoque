using Database;
using Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace View
{

    public partial class ConsultaViabilidade : Form
    {
       
       // List<int> quantidade = new List<int>();
        public Estoque Global;
        public produtoHVEX lista;
        bool flag = false;
        List<Estoque> lista_insumo = new List<Estoque>();
        List<Estoque> lista_compra = new List<Estoque>();
        string busca = "";
        int cont = 0;
        public ConsultaViabilidade()
        {
            InitializeComponent();
            button2.Visible = false;
            button1.Visible = false;
            button5.Enabled = false;

        }

        public void gerarprodutoHVEX()
        {
            if(textBox1.Text.Length > 0)
            {
                busca = textBox1.Text;
                if (MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.codHVEX.Contains(busca)), null).Count > 0){
                    var prodhvex = MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.codHVEX.Equals(busca)), null).First();
                    lista = prodhvex;
                }
                
            }
            if(textBox5.Text.Length > 0)
            {
                busca = textBox5.Text;
                if (MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.nome.Contains(busca)), null).Count > 0)
                {
                    var prodhvex = MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.nome.Contains(busca)), null).First();
                    lista = prodhvex;
                }
                
            }
            if (textBox3.Text.Length > 0)
            {
                busca = textBox3.Text;
                if (MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.descricao.Contains(busca)), null).Count > 0)
                {
                    var prodhvex = MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.descricao.Contains(busca)), null).First();
                    lista = prodhvex;
                }                    
            }

            
            if (button2.Enabled)
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            print_lista(lista_insumo);
            cont++;
            button5.Enabled = true;
            button1.Enabled = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            print_lista(lista_compra);
            cont++;

            button2.Enabled = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.CharacterCasing = CharacterCasing.Upper;
            if (textBox5.Text.Length > 0)
            {
                textBox1.Enabled = false;
                textBox3.Enabled = false;
            
            }
            else
            {
                textBox1.Enabled = true;
                textBox3.Enabled = true;
               
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.CharacterCasing = CharacterCasing.Upper;
            if (textBox1.Text.Length > 0)
            {
            
                textBox5.Enabled = false;
                textBox3.Enabled = false;

            }
            else
            {
              
                textBox3.Enabled = true;
                textBox5.Enabled = true;

            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.CharacterCasing = CharacterCasing.Upper;
            if (textBox3.Text.Length > 0)
            {
                textBox1.Enabled = false;
               
                textBox5.Enabled = false;

            }
            else
            {
                textBox1.Enabled = true;
               
                textBox5.Enabled = true;

            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if(cont > 0)
            {
                MenuEstoque menu = new MenuEstoque();
                menu.Show();
                this.Visible = false;
            }

            if (cont < 1)
            {
                List<Estoque> lista_aux = new List<Estoque>();
                gerarprodutoHVEX();

                button2.Visible = true;
                button1.Visible = true;

               
                Estoque aux = new Estoque();
                int MAX = 0;

                if (lista != null)
                {
                    lista_aux = lista.ProductCollection;

                    MAX = lista_aux.Count();

                    for (int i = 0; i < MAX; i++)
                    {

                        aux = lista_aux.ElementAt(i);
                        int a, b;
                        //cont = aux.quantidade - lista.ProductCollection.ElementAt(i).quantidade * int.Parse(textBox6.Text);
                        if (verifica_item(aux, i))
                        {
                            lista_insumo.Add(aux);
                        }
                        else
                        {
                            flag = true;
                            lista_compra.Add(aux);
                            b = aux.quantidade * int.Parse(textBox6.Text);

                            
                            var busca = MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.codHVEX.Equals(lista_compra.ElementAt(cont).codHVEX)), null);
                            a = busca.First().quantidade - b;
                            if (a < 0)
                            {
                                lista_compra.ElementAt(cont).quantidade = a * (-1);
                                aux.quantidade = a * (-1);

                            }
                            else
                            {
                                lista_compra.ElementAt(cont).quantidade = a;
                                aux.quantidade = a;
                            }
                            

                            cont++;
                        }
                    }
                    if (flag)
                    {


                        // print_lista(lista_compra);
                        button1.Enabled = false;
                        button2.Enabled = true;
                    }
                    else
                    {
                        //   print_lista(lista_insumo);
                        button1.Enabled = true;
                        button2.Enabled = false;
                    }
                }
                else
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    MessageBox.Show("Este Item não está cadastrado");
                    DialogResult = DialogResult.Yes;
                }

            }



        }

        public void print_lista(List<Estoque> lista)
        {
            int MAX = lista.Count();
            Estoque aux = new Estoque();
            if (flag)
            {
                for (int i = 0; i < MAX; i++)
                {
                    aux = lista.ElementAt(i);
                    DataGridViewRow item = new DataGridViewRow();
                    item.CreateCells(dataGridView1);
                    dataGridView1.Rows.Add(item);

                    dataGridView1.Rows[i].Cells[0].Value = aux.produto; // Seta os valores da célula
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true; //Trava a célula para o usuário não poder modificá-la

                    dataGridView1.Rows[i].Cells[1].Value = aux.quantidade;
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[2].Value = aux.Preco;
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[3].Value = aux.descricao;
                    dataGridView1.Rows[i].Cells[3].ReadOnly = true;
                }
            }
            else
            {
                for (int i = 0; i < MAX; i++)
                {
                    aux = lista.ElementAt(i);
                    DataGridViewRow item = new DataGridViewRow();
                    item.CreateCells(dataGridView1);
                    dataGridView1.Rows.Add(item);

                    dataGridView1.Rows[i].Cells[0].Value = aux.produto; // Seta os valores da célula
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true; //Trava a célula para o usuário não poder modificá-la

                    dataGridView1.Rows[i].Cells[1].Value = aux.quantidade;
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[2].Value = aux.Preco;
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[3].Value = aux.descricao;
                    dataGridView1.Rows[i].Cells[3].ReadOnly = true;
                }
            }
            
        }

        
        bool verifica_item(Estoque verifica, int i)
        {
            
            Estoque aux = new Estoque();

            //int aux2 = MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.codHVEX.Equals(verifica.codHVEX)), null).Count();
            aux = MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.produto.Contains(verifica.produto)), null).First();
           // quantidade.Add(aux.quantidade - lista.ProductCollection.ElementAt(i).quantidade * int.Parse(textBox6.Text));
            if (aux.quantidade-lista.ProductCollection.ElementAt(i).quantidade*int.Parse(textBox6.Text) >= 0)
            {
                return true;
            }
            return false;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            cont++;

            var MAX = lista.ProductCollection.Count();
            
            for (int i = 0; i < MAX; i++)
            {
                string busca = lista.ProductCollection.ElementAt(i).codHVEX;
                var filt = Builders<Estoque>.Filter.Where(c => c.codHVEX.Equals(busca));
                var aux = MongoConnection.QueryCollection("Estoques", filt, null).First();

               
                    var menos = lista.ProductCollection.ElementAt(i).quantidade;
                    aux.quantidade -= int.Parse(textBox6.Text) * menos; // Atualiza o banco de estoque
                    var a = lista.ProductCollection.ElementAt(i); // a recebe cada elemento de estoque
                    var b = Builders<Estoque>.Filter.Where(c => c.produto.Contains(lista.ProductCollection.ElementAt(i).produto));
                    MongoConnection.ReplaceOne("Estoques", b, aux);
                    verifica_item(aux, i);
                
            }
            lista.quantidade += int.Parse(textBox6.Text);

            var pesq = Builders<produtoHVEX>.Filter.Where(c => c.nome.Contains(lista.nome));
            MongoConnection.ReplaceOne("produtohvex", pesq, lista);

            if (true)
            {
                MessageBox.Show("Deu certo!");
                DialogResult = DialogResult.Yes;
            }
            button5.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.CharacterCasing = CharacterCasing.Upper;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
        }
    }


    
}