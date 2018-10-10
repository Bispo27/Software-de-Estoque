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
    public partial class ProdutoAcabado : Form
    {
        int cont = 0;
        public double soma = 0;
        produtoHVEX novo = new produtoHVEX();
        List<Estoque> lista = new List<Estoque>();
        string flag;
        bool control = true;
        public ProdutoAcabado()
        {
            InitializeComponent();
            inicializacao();
        }
        public void inicializacao()
        {
            textBox3.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;
            button4.Visible = false;
            button3.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0 || textBox2.Text.Length > 0 )
            {
                var filt = Builders<produtoHVEX>.Filter.Where(c => c.nome.Contains(textBox1.Text));
                var a = MongoConnection.QueryCollection("produtohvex", filt, null).Count;

                if (a > 0)
                {
                    control = false;
                    MessageBox.Show("Esse produto já está cadastrado");
                    DialogResult = DialogResult.Yes;
                }
                if (control)
                {
                    textBox3.ReadOnly = false;
                    textBox6.ReadOnly = false;
                    textBox7.ReadOnly = false;
                    textBox8.ReadOnly = false;
                    button4.Visible = true;
                    button3.Visible = true;
                }
                else
                {
                    textBox3.ReadOnly = true;
                    textBox6.ReadOnly = true;
                    textBox7.ReadOnly = true;
                    textBox8.ReadOnly = true;
                    button4.Visible = false;
                    button3.Visible = false;
                }
            }
            
        }



        private void button4_Click(object sender, EventArgs e)
        {            
           
            var busca = Busca(flag);
            Estoque aux = new Estoque();
            if(busca.Count > 0)
            {
                aux = busca.First();
                lista.Add(aux);
            }
            else
            {
                MessageBox.Show("Item não encontrado");
                DialogResult = DialogResult.Yes;
            }

            //  novo.ProductCollection.ElementAt(cont).quantidade = int.Parse(textBox8.Text);
            for (int i = 0; i < busca.Count(); i++)
            {
                soma += busca.ElementAt(i).Preco;
                
            }
            string a = Convert.ToString(soma);
            if (lista.Count() > 0 && textBox8.Text.Length > 0 && MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.produto.Contains(textBox6.Text)), null).Count() > 0)
            {
                lista.ElementAt(cont).quantidade = int.Parse(textBox8.Text);
                cont++;
            }
            
            textBox5.Text = a;
            textBox3.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
          
        }

        public List<Estoque> Busca(string busca)
        {
            CollectionEstoque Lista = new CollectionEstoque();

            var filt = Builders<Estoque>.Filter.Where(c => c.codHVEX.Equals(flag));
            if (textBox3.Text.Length > 0)
            {
                flag = textBox3.Text;
                filt = Builders<Estoque>.Filter.Where(c => c.codHVEX.Contains(flag));
                Lista.AddRange(MongoConnection.QueryCollection("Estoques", filt, null));
            }
            if (textBox6.Text.Length > 0)
            {
                flag = textBox6.Text;
                filt = Builders<Estoque>.Filter.Where(c => c.produto.Contains(flag));
                Lista.AddRange(MongoConnection.QueryCollection("Estoques", filt, null));
            }
            if (textBox7.Text.Length > 0)
            {
                flag = textBox7.Text;
                filt = Builders<Estoque>.Filter.Where(c => c.descricao.Contains(flag));
                Lista.AddRange(MongoConnection.QueryCollection("Estoques", filt, null));
            }
            
            return Lista;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.CharacterCasing = CharacterCasing.Upper;
            if (textBox3.Text.Length > 0)
            {
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                String.Concat(flag, textBox1.Text);
            }
            else
            {
                textBox6.Enabled = true;
                textBox7.Enabled = true;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.CharacterCasing = CharacterCasing.Upper;
            if (textBox6.Text.Length > 0)
            {
                textBox3.Enabled = false;
                textBox7.Enabled = false;
                String.Concat(flag, textBox1.Text);
            }
            else
            {
                textBox3.Enabled = true;
                textBox7.Enabled = true;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            textBox7.CharacterCasing = CharacterCasing.Upper;
            if (textBox7.Text.Length > 0)
            {
                textBox3.Enabled = false;
                textBox6.Enabled = false;
                String.Concat(flag, textBox1.Text);
            }
            else
            {
                textBox3.Enabled = true;
                textBox6.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool control = true;
            if ((textBox1.Text.Length == 0) || (textBox2.Text.Length == 0) )
            {
                control = false;
            }
            else
            {
                novo.nome = textBox1.Text;
                novo.descricao = textBox2.Text;
                novo.setor = comboBox1.Text;
                novo.quantidade = 0;
                novo.preco = textBox5.Text;
                novo.codHVEX = gerarcodhvex(sender, e);
                
                control = true;
            }


            

            novo.ProductCollection = lista;

            var teste = MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.nome.Contains(textBox1.Text)), null);
            if (teste.Count != 0)
            {
                control = false;
                MessageBox.Show("Já existe um produto com o mesmo nome no estoque");
                DialogResult = DialogResult.Yes;
            }



            if (control)
            {
                MongoConnection.InsertOne("produtohvex", novo);
                MessageBox.Show("Deu bão!");
                DialogResult = DialogResult.Yes;
                soma = 0;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.CharacterCasing = CharacterCasing.Upper;
        }

       

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox8.CharacterCasing = CharacterCasing.Upper;
        }

        public string gerarcodhvex(object sender, EventArgs e)
        {
            string a = "PROD", b = "", entrada = "";
            string hvexcod, sig, dados, val = "";

            
            if (comboBox1.SelectedIndex == 0)
            {
                b = "E";
            }
            if (comboBox1.SelectedIndex == 1)
            {
                b = "L";
            }
            if (comboBox1.SelectedIndex == 2)
            {
                b = "C";
            }
            if (comboBox1.SelectedIndex == 3)
            {
                b = "M";
            }
            if (comboBox1.SelectedIndex == 4)
            {
                b = "A";
            }
            if (comboBox1.SelectedIndex == 5)
            {
                b = "T";
            }
            if (comboBox1.SelectedIndex == 6)
            {
                b = "H";
            }
            
            dados = textBox1.Text;
            if (dados.Length < 3)
            {
                dados = dados + "   ";
            }
            sig = dados.Substring(0, 3);
            var variacao = MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.codHVEX.Contains(sig)), null).Count();

            if (variacao > 9 && variacao < 100)
            {
                val = string.Concat("0", variacao);
            }
            if (variacao < 10)
            {
                val = string.Concat("00", variacao);
            }
            hvexcod = string.Concat(a, b, "-", sig, val, entrada);
            return hvexcod;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
        }
    }
}
