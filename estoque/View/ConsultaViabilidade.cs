using Database;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
        int quant_lista = 0;
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
            MaximizeBox = false; this.MinimizeBox = false;

            button2.Visible = false;
            button1.Visible = false;
            button5.Enabled = false;
          //  button6.Visible = false;
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

        public class menor_preco
        {
            public double preco;
            public string fornecedor;
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            print_lista_insumo(lista_insumo);
            cont++;
            button5.Enabled = true;
            button1.Enabled = false;
        }
        public List<menor_preco> verifica_mais_barato(List<Estoque> a)
        {
            List<menor_preco> list_preco = new List<menor_preco>();
            
            Estoque busca = new Estoque();
            List<RegistroEntrada> aux = new List<RegistroEntrada>();
            RegistroEntrada aux_2 = new RegistroEntrada();
            
            for (int i = 0; i < a.Count(); i++)
            {
                string Buscar = a.ElementAt(i).codHVEX;
                aux = MongoConnection.QueryCollection("registerin", Builders<RegistroEntrada>.Filter.Where(c => c.codigo.Contains(Buscar)), null);
                if(aux.Count() > 0)
                {
                    aux_2 = aux.First();
                    for (int k = 0; k < aux.Count(); k++)
                    {
                        if (aux_2.preco > aux.ElementAt(k).preco)
                        {
                            aux_2 = aux.ElementAt(k);
                        }
                    }
                    menor_preco menorpreco_aux = new menor_preco();
                    menorpreco_aux.fornecedor = aux_2.fornecedor;
                    menorpreco_aux.preco = aux_2.preco;
                    list_preco.Add(menorpreco_aux);
                    
                }
                else
                {
                    menor_preco menorpreco_aux = new menor_preco();
                    menorpreco_aux.fornecedor = a.ElementAt(i).fornecedor;
                    menorpreco_aux.preco = a.ElementAt(i).Preco;
                    list_preco.Add(menorpreco_aux);
                }
                
            }
            return list_preco;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //button6.Visible = true;

            if(verifica_mais_barato(lista_compra).Count() > 0)
            {
                print_lista(lista_compra, verifica_mais_barato(lista_compra));
                cont++;
            }
            else
            {
                MessageBox.Show("ERRO! UM DOS INSUMOS DESTE ITEM NÃO ESTÁ CADASTRADO NO BANCO DE DADOS!!");
                DialogResult = DialogResult.Yes;
            }
            

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
            bool control = true;
            if(cont > 0)
            {
                MenuEstoque menu = new MenuEstoque();
                menu.Show();
                this.Visible = false;
            }
            if ((textBox5.Text.Length == 0 || textBox1.Text.Length == 0 || textBox3.Text.Length == 0 ) && textBox6.Text.Length == 0)
            {
                control = false;
                MessageBox.Show("Preencha todos os campos");
                DialogResult = DialogResult.Yes;
            }
            if (control)
            {
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



        }

        public void print_lista(List<Estoque> lista, List<menor_preco> menor_preco)
        {
            quant_lista = 0;
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

                    dataGridView1.Rows[i].Cells[1].Value = Convert.ToString(aux.quantidade);
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[2].Value = String.Concat("R$",menor_preco.ElementAt(i).preco);
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[3].Value = aux.descricao;
                    dataGridView1.Rows[i].Cells[3].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[4].Value = menor_preco.ElementAt(i).fornecedor;
                    dataGridView1.Rows[i].Cells[4].ReadOnly = true;
                    quant_lista++;
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

                    dataGridView1.Rows[i].Cells[1].Value = Convert.ToString(aux.quantidade);
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[2].Value = String.Concat("R$", menor_preco.ElementAt(i).preco);
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[3].Value = aux.descricao;
                    dataGridView1.Rows[i].Cells[3].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[4].Value = menor_preco.ElementAt(i).fornecedor;
                    dataGridView1.Rows[i].Cells[4].ReadOnly = true;
                    quant_lista++;
                }
            }
            
        }
        public void print_lista_insumo(List<Estoque> lista)
        {
            quant_lista = 0;
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

                    dataGridView1.Rows[i].Cells[1].Value = Convert.ToString(aux.quantidade);
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[2].Value = String.Concat("R$", aux.Preco);
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[3].Value = aux.descricao;
                    dataGridView1.Rows[i].Cells[3].ReadOnly = true;
                    quant_lista++;
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

                    dataGridView1.Rows[i].Cells[1].Value = Convert.ToString(aux.quantidade);
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[2].Value = String.Concat("R$", aux.Preco);
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[3].Value = aux.descricao;
                    dataGridView1.Rows[i].Cells[3].ReadOnly = true;
                    quant_lista++;
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PDF novo = new PDF();
            Document doc = novo.novo_arquivo(textBox5.Text);
            List<string> produto = new List<string>();
            PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);

          


            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                table.AddCell(new Phrase(dataGridView1.Columns[j].HeaderText));
            }

            table.HeaderRows = 1;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int k = 0; k < dataGridView1.Columns.Count; k++)
                {
                    if (dataGridView1[k, i].Value != null)
                    {
                        string escreve = dataGridView1[k, i].Value.ToString(); 
                        if (k == 2) escreve = string.Concat("R$ ", dataGridView1[k, i].Value.ToString());
                        
                        table.AddCell(new Phrase(escreve));
                    }
                }
            }
            doc.Open(); // abre o arquivo
            
            string dados = "\n\n";
            novo.escreve(doc, dados);
           // doc.AddTitle("Hello World example\n");
            doc.Add(table); // escreve a tabela
            doc.Close(); // fecha o arquivo

            MessageBox.Show("Arquivo criado na área de trabalho");
            DialogResult = DialogResult.Yes;

        }
    }
}
