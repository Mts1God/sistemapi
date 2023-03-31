using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjetoPI
{
    public partial class frmFuncionarios : Form
    {
        public frmFuncionarios()
        {
            InitializeComponent();
            txtCodigo.Text = carregacodfunc().ToString();
        }

        public int carregacodfunc()
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select codfunc +1 from tbfuncionarios order by codfunc desc";
            comm.CommandType = CommandType.Text;

            comm.Connection = Conexao.obterConexao();

            MySqlDataReader DR;
            DR = comm.ExecuteReader();
            DR.Read();

            int codigo = DR.GetInt32(0);

            Conexao.fecharConexao();

            return codigo;

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal voltar = new frmMenuPrincipal();
            voltar.Show();
            this.Hide();
        }

        public void cadastrarFuncionarios()
        {
            MySqlCommand comm = new MySqlCommand();

            comm.CommandText = "insert into tbFuncionarios(cargo,nome,email,endereco,telefone,cpf,cep,siglaEst,cidade,bairro,numero,complemento)values(@cargo,@nome,@email,@endereco,@telefone,@cpf,@cep,@siglaEst,@cidade,@bairro,@numero,@complemento); ";
            
            comm.CommandType = CommandType.Text;

            comm.Parameters.Clear();

            comm.Parameters.Add("@cargo", MySqlDbType.VarChar, 100).Value = txtCargo.Text;
            comm.Parameters.Add("@nome", MySqlDbType.VarChar, 100).Value = txtNome.Text;
            comm.Parameters.Add("@email", MySqlDbType.VarChar, 100).Value = txtEmail.Text;
            comm.Parameters.Add("@endereco", MySqlDbType.VarChar, 100).Value = txtEndereco.Text;
            comm.Parameters.Add("@telefone", MySqlDbType.VarChar, 14).Value = mskTelefone.Text;
            comm.Parameters.Add("@cpf", MySqlDbType.VarChar, 14).Value = mskCPF.Text;
            comm.Parameters.Add("@cep", MySqlDbType.VarChar, 8).Value = mskCEP.Text;
            comm.Parameters.Add("@siglaEst", MySqlDbType.VarChar, 2).Value = cbbEstado.Text;
            comm.Parameters.Add("@cidade", MySqlDbType.VarChar, 50).Value = txtCidade.Text;
            comm.Parameters.Add("@bairro", MySqlDbType.VarChar, 50).Value = txtBairro.Text;
            comm.Parameters.Add("@numero", MySqlDbType.VarChar, 10).Value = txtNum.Text;
            comm.Parameters.Add("@complemento", MySqlDbType.VarChar, 50).Value = txtComplemento.Text;


            comm.CommandType = CommandType.Text;

            comm.Connection = Conexao.obterConexao();

            int i = comm.ExecuteNonQuery();

            MessageBox.Show("Funcionario cadastrado com sucesso!!!" + i);

            Conexao.fecharConexao();
        }

        public int pesquisacodigo(int codigo)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select * from tbfuncionarios where codFunc = '" + codigo + "';";
            comm.CommandType = CommandType.Text;

            comm.Connection = Conexao.obterConexao();

            MySqlDataReader DR;
            DR = comm.ExecuteReader();
            DR.Read();

            codigo = DR.GetInt32(0);

            Conexao.fecharConexao();

            return codigo;
        }

        public void cadastarUsuario(int codigo)
        {
            MySqlCommand comm = new MySqlCommand();

            comm.CommandText = "insert into tbusuarios(codUsu,nomeUsu,emailUsu,senhaUsu,codFunc)values(@codUsu,@nomeUsu,@emailUsu,@senhaUsu,@codFunc " + codigo + ");";
            comm.CommandType = CommandType.Text;

            comm.Parameters.Clear();

            comm.Parameters.Add("@nomeUsu", MySqlDbType.VarChar, 100).Value = txtNome.Text;
            comm.Parameters.Add("@emailUsu", MySqlDbType.VarChar, 100).Value = txtEmail.Text;
            //comm.Parameters.Add("@senhaUsu", MySqlDbType.VarChar, 18).Value = txtSenha.Text;
            //comm.Parameters.Add("@telefoneUsu", MySqlDbType.VarChar, 14).Value = .Text;
            

            comm.CommandType = CommandType.Text;

            comm.Connection = Conexao.obterConexao();

            int i = comm.ExecuteNonQuery();

            MessageBox.Show("Funcionário cadastrado com sucesso!!!" + i);

            Conexao.fecharConexao();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            cadastrarFuncionarios();
            int codUsu = pesquisacodigo(Convert.ToInt32(txtCodigo.Text));
            cadastarUsuario(Convert.ToInt32(codUsu));
        }
    }
}
