using Database;
using Model;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Windows.Forms;

namespace View
{
    public partial class CadastrarItem : Form
    {
        public CadastrarItem()
        {
            InitializeComponent();
            MaximizeBox = false; this.MinimizeBox = false;

            comboBox1.SelectedIndex = 0;
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
            bool k = false;
                if ((!Ativo_Fixo.Checked && !Materia_prima.Checked && !Revenda.Checked && !Material_consumo.Checked)  || (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox4.Text.Length == 0 || textBox5.Text.Length == 0 || textBox6.Text.Length == 0 || textBox7.Text.Length == 0 || textBox8.Text.Length == 0 || textBox11.Text.Length == 0 || textBox12.Text.Length == 0 ))
                {
                    ValidData = false;
                    MessageBox.Show("Preencha todas os campos");
                    DialogResult = DialogResult.Yes;
                    k = true;
                }





                var teste = MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.produto.Contains(textBox2.Text)), null);
                if(teste.Count != 0 && !k)
                {
                    ValidData = false;
                    MessageBox.Show("Já existe um produto com o mesmo nome no estoque");
                    DialogResult = DialogResult.Yes;
                }


                if (ValidData)
                {
                    Estoque novo = new Estoque();
                    novo.tipo = textBox1.Text;
                    novo.produto = textBox2.Text;
                    novo.descricao = textBox3.Text;
                    novo.setor = comboBox1.Text;
                    novo.fabricante = textBox5.Text;
                    novo.fornecedor = textBox4.Text;
                    novo.Preco = Double.Parse(textBox7.Text);
                    novo.partNumb = textBox12.Text;
                    novo.NF1 = textBox11.Text;
                    novo.codHVEX = gerarcodhvex(sender, e);
                    novo.quantidade = int.Parse(textBox8.Text);
                    novo.dataCompra = dateTimePicker1.Value;
                    novo.responsavel = textBox8.Text;

                    if (MongoConnection.InsertOne("Estoques", novo))
                    {
                        MessageBox.Show("PRODUTO CADASTRADO COM SUCESSO");
                        DialogResult = DialogResult.Yes;
                        clear();
                    }
                }
        }    
            
            private void Ativo_Fixo_CheckedChanged(object sender, EventArgs e)
            {
                if (Ativo_Fixo.Checked == true)
                {
                    Material_consumo.Enabled = false;
                    Materia_prima.Enabled = false;
                    Revenda.Enabled = false;
                }
                else
                {
                    Material_consumo.Enabled = true;
                    Materia_prima.Enabled = true;
                    Revenda.Enabled = true;
                }
            }

            private void Material_consumo_CheckedChanged(object sender, EventArgs e)
            {
                if (Material_consumo.Checked == true)
                {
                    Ativo_Fixo.Enabled = false;
                    Materia_prima.Enabled = false;
                    Revenda.Enabled = false;
                }
                else
                {
                    Ativo_Fixo.Enabled = true;
                    Materia_prima.Enabled = true;
                    Revenda.Enabled = true;
                }
            }

            private void Materia_prima_CheckedChanged(object sender, EventArgs e)
            {

                if (Materia_prima.Checked == true)
                {
                    Material_consumo.Enabled = false;
                    Ativo_Fixo.Enabled = false;
                    Revenda.Enabled = false;
                }
                else
                {
                    Material_consumo.Enabled = true;
                    Ativo_Fixo.Enabled = true;
                    Revenda.Enabled = true;
                }
            }

            private void Revenda_CheckedChanged(object sender, EventArgs e)
            {
                if (Revenda.Checked == true)
                {
                    Material_consumo.Enabled = false;
                    Materia_prima.Enabled = false;
                    Ativo_Fixo.Enabled = false;
                }
                else
                {
                    Material_consumo.Enabled = true;
                    Materia_prima.Enabled = true;
                    Ativo_Fixo.Enabled = true;
                }
            }

        public string gerarcodhvex(object sender, EventArgs e)
        {
            string a = "", b = "", entrada = "";
            string hvexcod, sig, dados, val = "";

            if (Ativo_Fixo.Checked)
            {
                a = "A";
            }
            if (Material_consumo.Checked)
            {
                a = "C";
            }
            if (Materia_prima.Checked)
            {
                a = "P";
            }
            if (Revenda.Checked)
            {
                a = "R";
            }
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
            if (checkBox1.Checked)
            {
                entrada = "X";
            }
            dados = textBox2.Text;
            if(dados.Length < 3)
            {
                dados = dados + "   ";
            }
            sig = dados.Substring(0, 3);
            var variacao = MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.codHVEX.Contains(sig)), null).Count();

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

        private void button2_Click_1(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
        }       
        public void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox11.Clear();
            textBox12.Clear();
           

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox8.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            textBox7.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            textBox12.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            textBox11.CharacterCasing = CharacterCasing.Upper;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.CharacterCasing = CharacterCasing.Upper;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)44 && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }
 } 