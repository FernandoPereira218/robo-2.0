using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Media;
using robo.pgm;
using System.Data;
using System.Linq;
using System.Drawing;
using robo;
using System.ComponentModel;
using robo.Control.Novo;
using robo.Control.Implementacoes;

namespace Robo
{
    public partial class RoboForm : Form, IContratos.IMainForms
    {

        // Permite movimentação do formulario via mouse no "Painel do Menu"
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        public static string versaoRobo;
        private bool logout = false;
        private Point labelPonto1 = new Point(2, 3);
        private Point labelPonto2 = new Point(147, 3);
        private Point labelPonto3 = new Point(2, 50);
        private Point objetoPonto1 = new Point(2, 25);
        private Point objetoPonto2 = new Point(145, 25);
        private Point objetoPonto3 = new Point(2, 72);
        private static ImplementacaoPresenter presenter;

        private List<string> cbListModosExecucao = new List<string>();

        public RoboForm()
        {
            versaoRobo = Program.login.Permissao;
            presenter = new ImplementacaoPresenter(this);
            Dados.VerificaSemestre();

            InitializeComponent();
            InitializeBackgroundWorker();
            Program.formInterface = this;
            CreateData();
            radioBaixarDocumento.Visible = true;
            radioBuscarStatus.Visible = true;
            radioBuscarStatus.Checked = true;
            barraProgressoImportacao.Visible = false;
            lbProcessando.Visible = false;
            tbBarraStatus.BackColor = Color.White;
            tbBarraStatus.Visible = false;

            if (versaoRobo == "CAE")
            {
                panelMenu.BringToFront();
                btnAlunos.Visible = false;
                btnLogins.Visible = false;
                btUsuarios.Visible = false;
                btLogout.Visible = true;
                btLogout.Location = new Point(0, 40);

                // Panel
                panelMenu.Height = 82;

                // Cb
                cbPlataforma.Enabled = true;
                cbFaculdade.Enabled = true;
                cbSemestre.Enabled = true;
                cbExecucao.Enabled = true;
                cbPlataforma.Enabled = true;
                cbCampus.Enabled = true;

            }
            else if (versaoRobo == "operacoesFinanceiras")
            {
                btnAlunos.Visible = true;
                btnLogins.Visible = true;
                cbExecucao.DataSource = presenter.PreencherListaExecucao();
                cbExecucao.SelectedIndex = cbExecucao.FindStringExact("ADITAMENTO");
                cbExecucao.Enabled = true;
                txtCPF.Visible = false;
                labelCPF.Visible = false;
                labelCPFCaracteres.Visible = false;
            }
            radioBaixarDocumento.Checked = true;
            radioBaixarDocumento.Visible = false;
            radioBuscarStatus.Visible = false;
            labelModoOperacao.Visible = false;

            this.cbSemestre.DataSource = presenter.PreencherListaSemestre();

            //Preenchimento tipo de execução pelo banco de dados memus






            foreach (var item in cbExecucao.Items)
            {
                cbListModosExecucao.Add(item.ToString());
            }

            this.cbPlataforma.SelectedIndex = 0;
            this.cbFaculdade.SelectedIndex = 0;
            this.cbFaculdade.SelectedIndex = cbFaculdade.FindStringExact(Program.login.IES.ToUpper());
            this.cbFaculdade.Enabled = true;
            this.cbSemestre.SelectedIndex = cbSemestre.Items.Count - 1;
            //cbFaculdade.SelectedIndex = cbFaculdade.FindStringExact("TODOS");
            //this.cbExecucao.SelectedIndex = 0;

            AtualizarListViewAlunos();
            AtualizarListViewLogins();
            AtualizarListViewUsuarios();
        }

        //Background worker - Importar alunos
        private void InitializeBackgroundWorker()
        {
            bwBarraProgresso.DoWork += new DoWorkEventHandler(bwBarraProgresso_DoWork);
            bwBarraProgresso.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwBarraProgresso_RunWorkerCompleted);
        }
        private void bwBarraProgresso_DoWork(object sender, DoWorkEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (VerificaDiretorio())
            {
                string filePath = txtExcel.Text;
                Dados.ImportaAlunos(filePath);
            }
            Cursor.Current = Cursors.Default;
        }
        private void bwBarraProgresso_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lbProcessando.Visible = false;
                barraProgressoImportacao.Visible = false;
                btMenu.Enabled = true;
                btnSelectPath.Enabled = true;
                SystemSounds.Exclamation.Play();
                tbBarraStatus.Visible = true;
                tbBarraStatus.BackColor = Color.Red;
                tbBarraStatus.BeginInvoke(
                   new Action(() =>
                   {
                       tbBarraStatus.Text = Convert.ToString(e.Error);
                   }
                ));
            }
            else
            {
                lbProcessando.Visible = false;
                barraProgressoImportacao.Visible = false;
                btMenu.Enabled = true;
                btnSelectPath.Enabled = true;
                AtualizarListViewAlunos();
                SystemSounds.Beep.Play();
                tbBarraStatus.Visible = true;
                int qtdAlunosProcessados = Dados.CountAluno();
                tbBarraStatus.BeginInvoke(
                   new Action(() =>
                   {
                       tbBarraStatus.Text = "Importação de " + qtdAlunosProcessados + " alunos finalizada com sucesso!";
                   }
                ));
            }
        }

        //Mostrar Data
        private void CreateData()
        {
            atualizarTransparente(labelDay, pictureBox1);
            atualizarDate(labelDay);

        }
        private void atualizarTransparente(Label label, PictureBox pictureBox)
        {
            label.BackColor = System.Drawing.Color.Transparent;
            label.Parent = pictureBox;
        }
        private void atualizarDate(Label diaMesAno)
        {
            diaMesAno.Text = DateTime.Now.ToLongDateString();
        }

        //Botões Janela Windows
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Selecionar CSV
        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            if (!Dados.VerificaQtdAlunos())
            {
                return;
            }

            AtualizarListViewAlunos();

            tbBarraStatus.Visible = false;

            ofdSelectExcel.Filter = "CSV (*.csv)|*.csv";
            if (ofdSelectExcel.ShowDialog() == DialogResult.OK)
            {
                txtExcel.Text = ofdSelectExcel.FileName;
                lbProcessando.Visible = true;
                barraProgressoImportacao.ProgressBarStyle = ProgressBarStyle.Marquee;
                barraProgressoImportacao.Visible = true;
                barraProgressoImportacao.MarqueeAnimationSpeed = 5;
                barraProgressoImportacao.Enabled = true;
                btnSelectPath.Enabled = false;
                btMenu.Enabled = false;
                bwBarraProgresso.RunWorkerAsync();
            }
        }

        //Mudar de arquivo?
        private bool VerificaDiretorio()
        {
            if (!File.Exists(txtExcel.Text))
            {
                MessageBox.Show("O diretório do Excel informado não existe.", "Erro de Diretório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //Atualizar datagrid
        private void AtualizarListViewAlunos()
        {
            var source = new BindingSource();
            if (Dados.CountAluno() == 0)
            {
                dgvAlunos.Visible = false;
            }
            else
            {
                dgvAlunos.Visible = true;
                source.DataSource = Dados.SelectAlunos();
                dgvAlunos.AutoGenerateColumns = true;
                dgvAlunos.DataSource = source;
                dgvAlunos.Columns["Cpf"].DisplayIndex = 0;
                dgvAlunos.Columns["Nome"].DisplayIndex = 1;
                dgvAlunos.Columns["Tipo"].DisplayIndex = 2;
                dgvAlunos.Columns["Conclusao"].DisplayIndex = 3;
                dgvAlunos.Columns["HorarioConclusao"].DisplayIndex = 4;
            }
        }
        public void AtualizarListViewLogins()
        {
            var source = new BindingSource();

            if (Dados.CountLogins() == 0)
            {
                dgvLogins.Visible = false;
            }
            else
            {
                dgvLogins.Visible = true;
                source.DataSource = Dados.SelectLogins();
                dgvLogins.AutoGenerateColumns = true;
                dgvLogins.DataSource = source;
                dgvLogins.Columns[dgvLogins.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        public void AtualizarListViewUsuarios()
        {
            var source = new BindingSource();
            List<TOUsuario> Usuarios = Dados.SelectUsuarioWhereIES(Program.login.IES.ToUpper());

            if (Usuarios.Count == 0)
            {
                dgvUsuarios.Visible = false;
            }
            else
            {
                dgvUsuarios.Visible = true;
                source.DataSource = Usuarios;
                dgvUsuarios.AutoGenerateColumns = true;
                dgvUsuarios.DataSource = source;
                dgvUsuarios.Columns[dgvUsuarios.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        //Mudanças de painel
        private void btnAlunos_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            panelCadastrarContent.Visible = false;
            painelUsuarios.Visible = false;
            panelLogins.Visible = false;
            panelExcel.Visible = true;
            panelPlanilha.Visible = false;
            AtualizarListViewAlunos();
        }
        private void btnLogins_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            panelSubMenu.Visible = true;
            panelLogins.Visible = true;
            panelCadastrarContent.Visible = false;
            panelPlanilha.Visible = false;
            panelLogins.BringToFront();
        }
        private void btUsuarios_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            panelLogins.Visible = false;
            painelUsuarios.Visible = true;
            panelCadastrarContent.Visible = false;
            panelPlanilha.Visible = false;
            panelExcel.Visible = false;
        }
        private void btExtrairInformacoes_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            panelCadastrarContent.Visible = true;
            painelUsuarios.Visible = false;
            panelLogins.Visible = false;
            panelExcel.Visible = false;
            panelPlanilha.Visible = false;
        }
        private void btExportar_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            panelCadastrarContent.Visible = false;
            painelUsuarios.Visible = false;
            panelLogins.Visible = false;
            panelExcel.Visible = false;
            panelPlanilha.Visible = true;
        }

        //Refatorar após acertos das classes dos FIES´s
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (cbExecucao.Text.Contains("EXPORTAR") || cbExecucao.Text.Contains("VALIDAR REPARCELAMENTO"))
            {
                ExecutarExportacoes();
                return;
            }

            if (radioBuscarStatus.Checked == true)
            {
                if (File.Exists("Tabela.csv") == true)
                {
                    File.Delete("Tabela.csv");
                }
            }
            Cursor.Current = Cursors.WaitCursor;
            if (versaoRobo == "CAE")
            {
                txtCPF.Text = txtCPF.Text.Replace(".", "");
                txtCPF.Text = txtCPF.Text.Replace("-", "");
                txtCPF.Text = txtCPF.Text.Replace(" ", "");

                if (Util.VerificaCPFValido(txtCPF.Text) == true)
                {
                    ExecutaPrograma(true);
                }
                else
                {
                    MessageBox.Show("CPF incorreto, digite novamente!");
                }
            }
            else
            {
                if (txtCPF.Text == "")
                {
                    if (radioBuscarStatus.Checked == true)
                    {
                        MessageBox.Show("Busca por status deve ser feita somente com um CPF único.");
                        return;
                    }

                    ExecutaPrograma();
                }
                else if (Util.VerificaCPFValido(txtCPF.Text) == true)
                {
                    ExecutaPrograma(true);
                }
                else
                {
                    MessageBox.Show("CPF incorreto, digite novamente!");
                }
            }
            this.Cursor = Cursors.Default;
        }
        private void ExecutarExportacoes()
        {
            string inicial = dtpDataInicial.Value.ToString("dd/MM/yyyy");
            string final = dtpDataFinal.Value.ToString("dd/MM/yyyy");

            List<TOLogin> login = Dados.SelectLogins();
            List<TOLogin> listaLoginPlat = SelecionarLoginsPorPlataforma(login, cbPlataforma.Text);
            List<TOLogin> listaLoginFacul = SelecionarLoginsPorFaculdade(listaLoginPlat, true);

            if (cbPlataforma.Text.ToUpper() == "FIES LEGADO")
            {
                login = SelecionaLogins(cbPlataforma.Text, cbFaculdade.Text);
                FiesVelhoExp.OpenFiesVelho(login, cbExecucao.Text, cbCampus.Text, cbSemestre.Text, cbSituacao.Text, cbAno.Text, cbMes.Text);
            }
            else
            {
                FiesNovoExp.OpenFiesNovo(listaLoginFacul, cbExecucao.Text, cbSemestre.Text, cbFaculdade.Text, cbAno.Text, cbMes.Text, inicial, final, cbIESRepasse.Text);
            }


        }
        private void ExecutaPrograma(bool CPFUnico = false)
        {
            try
            {
                if (CPFUnico == true) //Versão CAE, buscar apenas um aluno
                {
                    List<TOAluno> alunos = new List<TOAluno>();
                    TOAluno alunoUnico = new TOAluno();
                    alunoUnico.Cpf = txtCPF.Text;
                    alunoUnico.Tipo = cbPlataforma.Text;
                    alunos.Add(alunoUnico);
                    if (versaoRobo == "CAE")
                    {
                        Dados.DeleteTodosAlunos();
                    }
                    Dados.InsertAluno(alunoUnico);
                }
                if (Dados.CountAluno() == 0)
                {
                    MessageBox.Show("Banco de Alunos vazio. Por favor importe uma tabela.", "Nenhum aluno encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Dados.CountLogins() == 0)
                {
                    MessageBox.Show("Banco de Logins vazio. Por favor adicione os Logins necessários.", "Nenhum login encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string numSemestre = presenter.BuscarNunSemestre(cbSemestre.Text);
                List<TOAluno> listaAlunos = new List<TOAluno>();
                List<TOLogin> logins;

                string execucao = cbExecucao.Text.ToUpper();
                if (execucao.Equals("EXPORTAR INADIMPLÊNCIA") || execucao.Equals("EXPORTAR REPASSE")
                    || execucao.Equals("EXPORTAR COPARTICIPAÇÃO") || execucao.Equals("VALIDAR REPARCELAMENTO"))
                {
                    logins = Dados.SelectLoginPorIESePlataforma(cbFaculdade.Text, cbPlataforma.Text, cbCampus.Text, true);
                }
                else
                {
                    logins = Dados.SelectLoginPorIESePlataforma(cbFaculdade.Text, cbPlataforma.Text, cbCampus.Text, false);
                }

                listaAlunos = SelecionarAlunosPorPlataforma(listaAlunos, cbPlataforma.Text);
                switch (cbPlataforma.Text.ToUpper().Trim())
                {
                    case "FIES LEGADO":
                        if (cbExecucao.SelectedItem.ToString().Contains("EXTRAIR") == true)
                        {
                            FiesVelhoInf.OpenFiesVelho(logins, listaAlunos, cbCampus.Text, cbSemestre.Text, cbExecucao.Text, cbSituacao.Text);
                        }
                        else
                        {
                            FiesVelho.OpenFiesVelho(logins, listaAlunos, cbExecucao.Text, cbCampus.Text, numSemestre, cbSemestre.Text, radioBuscarStatus.Checked, cbSituacao.Text);
                        }
                        break;

                    case "FIES NOVO":
                        MetodosFiesNovo.OpenFiesNovo(logins, listaAlunos, cbExecucao.Text, cbSemestre.Text, radioBuscarStatus.Checked);
                        break;
                    default:
                        MessageBox.Show("Plataforma inválida.");
                        break;
                }

                if (radioBuscarStatus.Checked == true)
                {
                    UpdateDataGridView(cbPlataforma.Text, cbExecucao.Text);
                    MessageBox.Show("Status do aluno atualizado!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Processamento executado com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Após refatoração dos FIES´s trazer dados já filtrados
        private List<TOLogin> SelecionarLoginsPorPlataforma(List<TOLogin> listLogins, String plataforma)
        {
            List<TOLogin> loginFies = new List<TOLogin>();

            foreach (TOLogin login in listLogins)
            {
                if (login.Plataforma == plataforma)
                {
                    loginFies.Add(login);
                }
            }

            if (loginFies.Count == 0)
            {
                //throw new Exception(String.Format("Nenhum login encontrado na plataforma escolhida ({0}). Cheque se o banco de dados contém logins da plataforma que deseja realizar os aditamentos.", plataforma));
            }
            return loginFies;
        }

        //Após refatoração dos FIES´s trazer dados já filtrados
        private static List<TOLogin> SelecionaLogins(string tipoFies, string IES)
        {
            List<TOLogin> logins = Dados.SelectLogins();
            for (int i = logins.Count - 1; i >= 0; i--)
            {
                if (logins[i].Faculdade.ToUpper() != IES.ToUpper() || logins[i].Plataforma.ToUpper() != tipoFies.ToUpper())
                {
                    logins.RemoveAt(i);
                }
            }
            return logins;
        }

        //Após refatoração dos FIES´s trazer dados já filtrados
        private List<TOLogin> SelecionarLoginsPorFaculdade(List<TOLogin> logins, bool admin)
        {
            List<TOLogin> loginUsado = new List<TOLogin>();

            if (cbFaculdade.SelectedIndex > 0)
            {
                string instituicao = cbFaculdade.SelectedItem.ToString();
                foreach (TOLogin login in logins)
                {
                    if (login.Faculdade == instituicao)
                    {
                        if (admin == true)
                        {
                            if (login.Admin.ToUpper() == "SIM")
                            {
                                loginUsado.Add(login);
                            }
                        }
                        else
                        {
                            if (login.Admin.ToUpper() == "NÃO")
                            {
                                loginUsado.Add(login);
                            }
                        }
                    }
                }

                if (loginUsado.Count == 0)
                {
                    //throw new Exception(String.Format("Nenhum login encontrado da faculdade escolhida ({0}). Cheque se o banco de dados contém logins da faculdade que deseja realizar os aditamentos.", instituicao));
                }
                return loginUsado;
            }
            return logins;
        }

        //Após refatoração dos FIES´s trazer dados já filtrados
        private List<TOAluno> SelecionarAlunosPorPlataforma(List<TOAluno> alunos, string plataforma, string cpfUnico = "")
        {
            List<TOAluno> alunosFies = new List<TOAluno>();
            alunosFies = Dados.SelectAlunoWhere(plataforma);
            foreach (TOAluno aluno in alunos)
            {
                presenter.TratarDadosAluno(aluno);

                if (aluno.AproveitamentoAtual.Contains("TRANCADO") == true)
                {
                    aluno.Conclusao = "Trancado";
                    Dados.UpdateAluno(aluno);
                }

            }

            if (alunosFies.Count == 0)
            {
                throw new Exception(String.Format("Nenhum aluno encontrado na plataforma escolhida ({0}). Cheque se o banco de dados contém alunos da plataforma que deseja realizar os aditamentos.", plataforma));
            }
            return alunosFies;
        }

        //Modificar Logins
        private void btnAdicionarLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this.Location);
            loginForm.ShowDialog();
            AtualizarListViewLogins();


        }
        private void btnModificarLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this.Location, dgvLogins.CurrentRow.DataBoundItem as TOLogin);
            loginForm.ShowDialog();
            AtualizarListViewLogins();
        }
        private void btnExcluirLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir este usuário?", "Excluir usuário", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Dados.DeleteLogin(dgvLogins.CurrentRow.DataBoundItem as TOLogin);
                MessageBox.Show("Login excluido com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarListViewLogins();
            }
        }

        //Alterardo da ExportãExecel para .CSV diretamente do DataGridView, ao invés de ExportCSV (Implementado para exportar do DB)
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            Util.ExportarCSV(dgvAlunos.Rows.Count);
        }

        //Mostrar painel menu (botão logout)
        private void btMenu_Click(object sender, EventArgs e)
        {
            if (panelMenu.Visible == false)
            {

                panelMenu.Visible = true;
                panelMenu.BringToFront();


            }
            else
            {
                panelMenu.Visible = false;
            }
        }

        // Depois tirar // Botao de adicionar/remover senha do banco de dados
        private void btnSenhaBanco_Click(object sender, EventArgs e)
        {
            Util.gravaSenha();
        }

        // Verifica se o CPF possui letra | Capa os valores que não sejam inteiros - versão CAE
        private void txtCPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtCPF.Text.Length == 11 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        //Seleciona Campus disponíveis dependendo da IES selecionada
        private void cbFaculdade_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCampus.DataSource = Dados.SelectLoginTOIES(cbFaculdade.Text, cbPlataforma.Text);
        }

        //Abrir painéis extras necessários
        private void cbExecucao_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearPanel();
            ComboBox cb = (ComboBox)sender;

            // Refatorar?
            if (cb.SelectedItem.ToString() == "EXPORTAR COPARTICIPAÇÃO")
            {
                labelDataInicio.Location = labelPonto1;
                labelDataFim.Location = labelPonto2;
                dtpDataInicial.Location = objetoPonto1;
                dtpDataFinal.Location = objetoPonto2;
                labelIESRepasse.Location = labelPonto3;
                cbIESRepasse.Location = objetoPonto3;

                labelDataInicio.Visible = true;
                labelDataFim.Visible = true;
                dtpDataInicial.Visible = true;
                dtpDataFinal.Visible = true;
                labelIESRepasse.Visible = true;
                cbIESRepasse.Visible = true;
            }
            else if (cb.SelectedItem.ToString() == "EXPORTAR REPASSE" || cb.SelectedItem.ToString() == "EXPORTAR EXTRATO MENSAL DE REPASSE")
            {
                labelMes.Location = labelPonto1;
                labelAno.Location = labelPonto2;
                cbMes.Location = objetoPonto1;
                cbAno.Location = objetoPonto2;

                labelMes.Visible = true;
                labelAno.Visible = true;
                cbMes.Visible = true;
                cbAno.Visible = true;
            }
            else if (cb.SelectedItem.ToString().Contains("DRI"))
            {
                labelSituacao.Location = labelPonto1;
                cbSituacao.Location = objetoPonto1;

                labelSituacao.Visible = true;
                cbSituacao.Visible = true;
            }
        }

        //Selecionar modos de execução por plataforma
        private void cbPlataforma_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbExecucao.DataSource = presenter.PreencherListaExecucaoPorPlataforma(cbPlataforma.Text);
            cbCampus.SelectedIndex = 0;
            if (cbPlataforma.Text == "FIES Novo")
            {
                cbCampus.Enabled = false;
            }
            else
            {
                cbCampus.Enabled = true;
            }
        }

        // Verificar se isso vai ser usado - Buscar Status
        public void UpdateDataGridView(string tipoFIES, string tipoExecucao)
        {
            if (File.Exists("Tabela.csv") == false)
            {
                throw new Exception("Aluno não encontrado");
            }
            string temp;
            using (StreamReader t = new StreamReader("Tabela.csv"))
            {
                temp = t.ReadToEnd();
            }
            string[] data = temp.Split(';');
            List<string> listItems = new List<string>();
            foreach (var item in data)
            {
                listItems.Add(item);
            }

            string csvFile = System.IO.Path.Combine(Application.StartupPath, "Tabela.csv");
            List<string[]> rows = File.ReadAllLines(csvFile).Select(x => x.Split(';')).ToList();
            DataTable dataTable = new DataTable();

            if (tipoFIES == "FIES Novo")
            {
                for (int i = 0; i < 10; i++)
                {
                    dataTable.Columns.Add(rows[0][i]);
                }
            }
            else
            {
                if (tipoExecucao == "DRI")
                {
                    for (int i = 0; i < 5; i++)
                    {
                        dataTable.Columns.Add(rows[0][i]);
                    }
                }
                else
                {
                    for (int i = 0; i < 7; i++)
                    {
                        dataTable.Columns.Add(rows[0][i]);
                    }
                }

            }
            rows.RemoveAt(0);
            rows.ForEach(x => { dataTable.Rows.Add(x); });

            dataGridView1.DataSource = dataTable;
            //metroTabControl1.SelectedTab = metroTabPage2;
            if (tipoFIES == "FIES Novo")
            {
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = false;
                dataGridView1.Columns[dataGridView1.Columns.Count - 2].Visible = false;
            }
        }

        // Não mudar
        private void btLogout_Click(object sender, EventArgs e)
        {
            logout = true;
            this.Close();
            Program.formLogin.Show();
            Program.login = null;
            if (File.Exists("session.dat") == true)
            {
                File.Delete("session.dat");
            }
        }

        // Não mudar
        private void RoboForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logout == false)
            {
                Application.Exit();
            }
        }



        // Mudar? - Modificações Usuarios
        private void btAddUsuario_Click(object sender, EventArgs e)
        {
            UsuarioForm usuarioForm = new UsuarioForm(this.Location);
            usuarioForm.ShowDialog();
            AtualizarListViewUsuarios();
        }
        private void btModUsuario_Click(object sender, EventArgs e)
        {
            UsuarioForm usuarioForm = new UsuarioForm(this.Location, dgvUsuarios.CurrentRow.DataBoundItem as TOUsuario);
            usuarioForm.ShowDialog();
            AtualizarListViewUsuarios();

        }
        private void btExcUsuario_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir este usuário?", "Excluir usuário", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Dados.DeleteUsuario(dgvUsuarios.CurrentRow.DataBoundItem as TOUsuario);
                MessageBox.Show("Login excluido com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarListViewUsuarios();
            }
        }

        //Limpar painel de dados extras
        private void ClearPanel()
        {
            foreach (Control item in panelDadosDeSituacao.Controls)
            {
                item.Visible = false;
            }
        }

        //Exportar Infs
        private void btnExportarInformacoes_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = "Exportado_Robo.csv";
            bool fileError = false;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch (IOException ex)
                    {
                        fileError = true;
                        MessageBox.Show("Arquivo já existe, e está aberto em outro aplicativo" + ex.Message);
                    }
                }
                if (!fileError)
                {
                    try
                    {
                        Dados.ExportarInformacoes_CSV(sfd.FileName);
                        MessageBox.Show("Dados Exportados com Sucesso!!!", "Info");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error :" + ex.Message);
                    }
                }
            }
        }

        // Metodo que realiza a captura segurando o mouse no "Painel do menu"
        private void panelMenuBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}