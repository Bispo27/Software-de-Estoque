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
    public partial class Entrada : Form
    {
        string flag = "";
        
        public Entrada()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool validData = true;
            CollectionEstoque Lista = new CollectionEstoque();

            var filt = Builders<Estoque>.Filter.Where(c => c.codHVEX.Equals(flag));
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



            Estoque atualiza = new Estoque();
            int a = int.Parse(textBox5.Text);
            int b = Lista.First().quantidade;
            var soma = a + b;
            if (soma != 0)
            {
                validData = true;
                atualiza = Lista.First();
                atualiza.quantidade = soma;
            }
            
            if (validData)
            {
                RegistroEntrada r = new RegistroEntrada();
                r.data = dateTimePicker1.Value;
                r.NF = textBox4.Text;
                r.codigo = atualiza.codHVEX;
                r.quantidade = int.Parse(textBox5.Text);
                r.responsavel = comboBox1.Text;

                if (MongoConnection.InsertOne("registerin", r) && MongoConnection.ReplaceOne("Estoques", filt, atualiza))
                {
                    MessageBox.Show("Deu certo!");
                    DialogResult = DialogResult.Yes;
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
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

        private void textBox2_TextChanged(object sender, EventArgs e)
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

        private void textBox3_TextChanged(object sender, EventArgs e)
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.CharacterCasing = CharacterCasing.Upper;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
