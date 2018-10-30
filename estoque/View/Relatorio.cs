using Database;
using Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;



namespace View
{
    public partial class Relatorio : Form
    {
        int flag_cont = 0;
        
        public Relatorio()
        {
            InitializeComponent();
            MaximizeBox = false; this.MinimizeBox = false;

        }

        
        public void button1_Click(object sender, EventArgs e)
        {
            if(flag_cont > 0)
            {
                clean();
            }
            DateTime menor_data = dateTimePicker1.Value;
            DateTime maior_data = dateTimePicker2.Value;
            menor_data.AddDays(-1);
            List<Estoque> busca = new List<Estoque>();
            List<RegistroSaida> lista_out = new List<RegistroSaida>();
            List<RegistroEntrada> lista_in = new List<RegistroEntrada>();
            bool control = true;
            if (dateTimePicker2.Value <= dateTimePicker1.Value)
            {
                control = false;
                MessageBox.Show("Coloque uma data inicial menor que a data final");
                DialogResult = DialogResult.Yes;
            }
            if(control && comboBox1.Text.Length == 0 )
            {
                control = false;
                MessageBox.Show("Insira na box um tipo de relatório válido");
                DialogResult = DialogResult.Yes;
            }
            if(control &&  (comboBox1.Text != "Entrada" && comboBox1.Text != "Saída" && comboBox1.Text != "Saída de Produtos HVEX"))
            {
                control = false;
                MessageBox.Show("Insira na box um tipo de relatório válido");
                DialogResult = DialogResult.Yes;
            }
            if (control)
            {
                

                Estoque teste = new Estoque();

                var filt_out = Builders<RegistroSaida>.Filter.And(
                    Builders<RegistroSaida>.Filter.Gte(x => x.data, menor_data),
                    Builders<RegistroSaida>.Filter.Lte(x => x.data, maior_data));
                var filt_in = Builders<RegistroEntrada>.Filter.And(
                    Builders<RegistroEntrada>.Filter.Gte(x => x.data, menor_data),
                    Builders<RegistroEntrada>.Filter.Lte(x => x.data, maior_data));


                if (comboBox1.Text == "Entrada")
                {
                    var aux_in = MongoConnection.QueryCollection("registerin", filt_in, null);
                    Preenche_tabela_entrada(aux_in, aux_in.Count());
                }

                else if (comboBox1.Text == "Saída")
                {
                    var aux_out = MongoConnection.QueryCollection("registerout", filt_out, null);
                    Preenche_tabela_saida(aux_out, aux_out.Count());
                }
                else if(comboBox1.Text == "Saída de Produtos HVEX")
                {
                    var aux_out = MongoConnection.QueryCollection("registerout", filt_out, null);
                    Preenche_tabela_saida_prodhvex(aux_out, aux_out.Count());
                }

            }

        }
        public void Preenche_tabela_saida_prodhvex(List<RegistroSaida> produto, int quant)
        {
            int control = 0;

            for (int i = 0; i < produto.Count(); i++)
            {
                if (MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.codHVEX.Equals(produto.ElementAt(i).codigo)), null).Count > 0)
                {
                    DataGridViewRow item = new DataGridViewRow();
                    item.CreateCells(dataGridView1);
                    dataGridView1.Rows.Add(item);

                    var a = MongoConnection.QueryCollection("produtohvex", Builders<produtoHVEX>.Filter.Where(c => c.codHVEX.Equals(produto.ElementAt(i).codigo)), null).First();

                    dataGridView1.Rows[i].Cells[0].Value = produto.ElementAt(i).data; // Seta os valores da célula
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true; //Trava a célula para o usuário não poder modificá-la*/

                    dataGridView1.Rows[i].Cells[1].Value = a.nome;
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToString(produto.ElementAt(i).quantidade);
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[3].Value = produto.ElementAt(i).responsavel;
                    dataGridView1.Rows[i].Cells[3].ReadOnly = true;
                    control++;
                }
                
                
            }
            if(control == 0)
            {
                MessageBox.Show("Não existe registro de saída desse produto neste intervalo");
                DialogResult = DialogResult.Yes;
            }
            flag_cont++;
        }




        public void clean()
        {
            for(int j = 0; j < 4; j++)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = "";
                }
            }
        }

        public void Preenche_tabela_saida(List<RegistroSaida> produto, int quant)
        {

            int control = 0;
            int cont = 0;
            for (int i = 0; i < produto.Count(); i++)
            {
                
                if (MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.codHVEX.Equals(produto.ElementAt(i).codigo)), null).Count() > 0)
                {

                    DataGridViewRow item = new DataGridViewRow();
                    item.CreateCells(dataGridView1);
                    dataGridView1.Rows.Add(item);

                    var a = MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.codHVEX.Equals(produto.ElementAt(i).codigo)), null).First();

                    dataGridView1.Rows[cont].Cells[0].Value = produto.ElementAt(i).data; // Seta os valores da célula
                    dataGridView1.Rows[cont].Cells[0].ReadOnly = true; //Trava a célula para o usuário não poder modificá-la*/

                    dataGridView1.Rows[cont].Cells[1].Value = a.produto;
                    dataGridView1.Rows[cont].Cells[1].ReadOnly = true;

                    dataGridView1.Rows[cont].Cells[2].Value = Convert.ToString(produto.ElementAt(i).quantidade);
                    dataGridView1.Rows[cont].Cells[2].ReadOnly = true;

                    dataGridView1.Rows[cont].Cells[3].Value = produto.ElementAt(i).responsavel;
                    dataGridView1.Rows[cont].Cells[3].ReadOnly = true;
                    control++;
                    cont++;
                }

                
            }
            if (control == 0)
            {
                MessageBox.Show("Não existe registro de saída desse produto neste intervalo");
                DialogResult = DialogResult.Yes;
            }
            flag_cont++;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
        }

        public void Preenche_tabela_entrada(List<RegistroEntrada> produto, int quant)
        {
            int control = 0;            

            for (int i = 0; i < produto.Count(); i++)
            {
                var Buscar = produto.ElementAt(i).codigo;
                if (MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.codHVEX.Equals(Buscar)), null).Count() > 0)
                {
                    DataGridViewRow item = new DataGridViewRow();
                    item.CreateCells(dataGridView1);
                    dataGridView1.Rows.Add(item);

                    var a = MongoConnection.QueryCollection("Estoques", Builders<Estoque>.Filter.Where(c => c.codHVEX.Equals(Buscar)), null).First();

                    dataGridView1.Rows[i].Cells[0].Value = produto.ElementAt(i).data; // Seta os valores da célula
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true; //Trava a célula para o usuário não poder modificá-la*/

                    dataGridView1.Rows[i].Cells[1].Value = a.produto;
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToString(produto.ElementAt(i).quantidade);
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;

                    dataGridView1.Rows[i].Cells[3].Value = produto.ElementAt(i).responsavel;
                    dataGridView1.Rows[i].Cells[3].ReadOnly = true;
                    control++;
                   
                }

             }
            if (control == 0)
            {
                MessageBox.Show("Não existe registro de entrada deste produto neste intervalo");
                DialogResult = DialogResult.Yes;
            }

            flag_cont++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MenuEstoque menu = new MenuEstoque();
            menu.Show();
            this.Visible = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value == dateTimePicker2.Value && dateTimePicker1.Value <= dateTimePicker2.Value)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value == dateTimePicker2.Value && dateTimePicker1.Value <= dateTimePicker2.Value)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }
    }          
}
