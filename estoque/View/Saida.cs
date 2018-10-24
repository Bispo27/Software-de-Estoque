using Database;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MongoDB.Driver;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class Saida : Form
    {
        bool controle = true;
        string flag = "";
        public Saida()
        {
            InitializeComponent();
            MaximizeBox = false; this.MinimizeBox = false;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool botao = false;
            if (((comboBox1.Text.Length > 0) && (textBox5.Text.Length > 0) && (textBox4.Text.Length > 0)) && (textBox1.Text.Length > 0 || textBox2.Text.Length > 0 || textBox3.Text.Length > 0) && int.Parse(textBox5.Text) > 0)
            {
                botao = true;
            }
            else
            {
                MessageBox.Show("PREENCHA TODOS OS CAMPOS");
                DialogResult = DialogResult.Yes;
            }

            bool validData = true;
            CollectionEstoque Lista = new CollectionEstoque();
            List<produtoHVEX> lista = new List<produtoHVEX>();
            Estoque atualiza = new Estoque();
            produtoHVEX Atualiza = new produtoHVEX();
            var filt = Builders<Estoque>.Filter.Where(c => c.codHVEX.Equals(flag));
            var Filt = Builders<produtoHVEX>.Filter.Where(c => c.codHVEX.Equals(flag));

            if (botao)
            {
                if (controle)
                {
                    if (textBox1.Text.Length > 0)
                    {
                        flag = textBox1.Text;
                        filt = Builders<Estoque>.Filter.Where(c => c.codHVEX.Contains(flag));
                        Lista.AddRange(MongoConnection.QueryCollection("Estoques", filt, null));
                    }
                    if (textBox2.Text.Length > 0)
                    {
                        flag = textBox2.Text;
                        filt = Builders<Estoque>.Filter.Where(c => c.produto.Contains(flag));
                        Lista.AddRange(MongoConnection.QueryCollection("Estoques", filt, null));
                    }
                    if (textBox3.Text.Length > 0)
                    {
                        flag = textBox3.Text;
                        filt = Builders<Estoque>.Filter.Where(c => c.descricao.Contains(flag));
                        Lista.AddRange(MongoConnection.QueryCollection("Estoques", filt, null));
                    }
                    if(Lista.Count() > 0)
                    {

                        var subtrai = Lista.First().quantidade - int.Parse(textBox5.Text);
                        if (subtrai < 0)
                        {
                            validData = false;
                            MessageBox.Show("Você está tentando retirar mais do que tem no estoque");
                            DialogResult = DialogResult.Yes;
                        }
                        else
                        {
                            validData = true;
                            atualiza = Lista.First();
                            atualiza.quantidade = subtrai;
                        }

                        if (validData)
                        {
                            RegistroSaida r = new RegistroSaida();
                            r.data = dateTimePicker1.Value;
                            r.NF = textBox4.Text;
                            r.responsavel = comboBox1.Text;
                            r.codigo = atualiza.codHVEX;
                            r.quantidade = int.Parse(textBox5.Text);
                            r.responsavel = comboBox1.Text;

                            if (MongoConnection.InsertOne("registerout", r) && MongoConnection.ReplaceOne("Estoques", filt, atualiza))
                            {
                                MessageBox.Show("Deu certo!");
                                DialogResult = DialogResult.Yes;
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Não existe tal produto em estoque");
                        DialogResult = DialogResult.Yes;
                    }
                    
                }
                else
                {
                    if (textBox1.Text.Length > 0)
                    {
                        flag = textBox1.Text;
                        Filt = Builders<produtoHVEX>.Filter.Where(c => c.codHVEX.Equals(flag));
                        lista.AddRange(MongoConnection.QueryCollection("produtohvex", Filt, null));
                    }
                    if (textBox2.Text.Length > 0)
                    {
                        flag = textBox2.Text;
                        Filt = Builders<produtoHVEX>.Filter.Where(c => c.nome.Contains(flag));
                        lista.AddRange(MongoConnection.QueryCollection("produtohvex", Filt, null));
                    }
                    if (textBox3.Text.Length > 0)
                    {
                        flag = textBox3.Text;
                        Filt = Builders<produtoHVEX>.Filter.Where(c => c.descricao.Contains(flag));
                        lista.AddRange(MongoConnection.QueryCollection("produtohvex", Filt, null));
                    }
                    if (lista.Count() > 0)
                    {
                        var subtrai = lista.First().quantidade - int.Parse(textBox5.Text);
                        if (subtrai < 0)
                        {
                            validData = false;
                            MessageBox.Show("Este produto não tem o suficiente no estoque");
                            DialogResult = DialogResult.Yes;
                        }
                        else
                        {
                            validData = true;
                            Atualiza = lista.First();
                            Atualiza.quantidade = subtrai;
                        }
                        if (validData)
                        {
                            RegistroSaida r = new RegistroSaida();
                            r.data = dateTimePicker1.Value;
                            r.NF = textBox4.Text;
                            r.responsavel = comboBox1.Text;
                            r.codigo = Atualiza.codHVEX;
                            r.quantidade = int.Parse(textBox5.Text);

                            if (MongoConnection.InsertOne("registerout", r) && MongoConnection.ReplaceOne("produtohvex", Filt, Atualiza))
                            {
                                MessageBox.Show("Deu certo!");
                                DialogResult = DialogResult.Yes;
                            }
                        }

                    }
                    else
                    { 
                         MessageBox.Show("Não existe tal produto em estoque");
                         DialogResult = DialogResult.Yes;
                    }
                    
                }
            }

            
        }

        

        
        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            textBox3.CharacterCasing = CharacterCasing.Upper;
            if (textBox3.Text.Length > 0)
            {
                textBox2.Enabled = false;
                textBox1.Enabled = false;
                String.Concat(flag, textBox3.Text);
            }
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;

            }
            
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            textBox2.CharacterCasing = CharacterCasing.Upper;
            if (textBox2.Text.Length > 0)
            {
                textBox1.Enabled = false;
                textBox3.Enabled = false;
                String.Concat(flag, textBox2.Text);
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
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                String.Concat(flag, textBox1.Text);
            }
            else
            {
                textBox2.Enabled = true;
                textBox3.Enabled = true;

            }
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.CharacterCasing = CharacterCasing.Upper;

            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.CharacterCasing = CharacterCasing.Upper;
           

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            controle = false;
        }

        

        private void button5_Click(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
    }
}
