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
            txtCodigo.Text = carregaCodigoFunc().ToString();
        }

        public int carregaCodigoFunc()
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

            comm.CommandText = "insert into DBSeuLixoAqui(nome,email,telefone,cpf,endereco,numero,cep,complemento,bairro,cidade,siglaEst)values(@nome,@email,@telefone,@cpf,@endereco,@numero,@cep,@complemento,@bairro,@cidade,@siglaEst); ";
            
            comm.CommandType = CommandType.Text;

            comm.Parameters.Clear();

            comm.Parameters.Add("@nomefunc", MySqlDbType.VarChar, 100).Value = txtNome.Text;
            comm.Parameters.Add("@emailfunc", MySqlDbType.VarChar, 100).Value = txtEmail.Text;
            comm.Parameters.Add("@telefone", MySqlDbType.VarChar, 14).Value = mskTelefone.Text;
            comm.Parameters.Add("@cpf", MySqlDbType.VarChar, 14).Value = mskCPF.Text;

            comm.CommandType = CommandType.Text;

            comm.Connection = Conexao.obterConexao();

            int i = comm.ExecuteNonQuery();

            MessageBox.Show("Paciente cadastrado com sucesso!!!" + i);

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

        public void cadastrarUsuario(int codigo)
        {
            MySqlCommand comm = new MySqlCommand();

            comm.CommandText = "insert into tbusuarios(nomeUsu,senhaUsu, codfunc)values(@nomeUsu,@senhaUsu, " + codigo + ");";
            comm.CommandType = CommandType.Text;

            comm.Parameters.Clear();

            comm.Parameters.Add("@nomeUsu", MySqlDbType.VarChar, 100).Value = txtUsuario.Text;
            comm.Parameters.Add("@senhaUsu", MySqlDbType.VarChar, 100).Value = txtSenha.Text;


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
            cadastrarUsuario(Convert.ToInt32(codUsu));
        }
    }
}
