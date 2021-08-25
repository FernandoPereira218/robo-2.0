using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Media;
using System.Text;
using robo.pgm;
using System.Data;
using System.Linq;
using System.Drawing;
using robo;
using System.Threading;
using System.ComponentModel;
using robo.Control.Novo;
using robo.Model.TO;

namespace Robo
{
    public partial class RoboForm : Form
    {
        public static string versaoRobo;
        //        public static string versaoRobo = "operacoesFinanceiras";
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        //public const bool finish = false;
        private bool logout = false;

        private Point labelPonto1 = new Point(2, 3);
        private Point labelPonto2 = new Point(147, 3);
        private Point labelPonto3 = new Point(2, 50);

        private Point objetoPonto1 = new Point(2, 25);
        private Point objetoPonto2 = new Point(145, 25);
        private Point objetoPonto3 = new Point(2, 72);

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
        int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private List<TOSemestre> semestre = new List<TOSemestre>();
        private List<string> cbListModosExecucao = new List<string>();
        public RoboForm()
        {
            // List<Aluno> list = Dados.SelectAlunos(typeof(TOAluno));
            // MetodosFiesNovo.OpenFiesNovo(null, list, "", "", false, typeof(TOAluno));
            string ano = DateTime.Now.ToString("yyyy");
            List<string> anos = new List<string>();
            for (int i = 2010; i <= Convert.ToInt32(ano); i++)
            {
                anos.Add(i.ToString());
            }

            versaoRobo = Program.login.Permissao;
            string verificarSemestre = Util.VerificaSemestreAtual();

            if (Dados.verficarSemestre(verificarSemestre) == 0)
            {
                TOSemestre semestre = new TOSemestre();
                semestre.Semestre = verificarSemestre;
                Dados.InsertSemestre(semestre);
            }

            InitializeComponent();
            InitializeBackgroundWorker();
            Program.formInterface = this;
            timer1.Start();
            CreateData();
            radioBaixarDocumento.Visible = true;
            radioBuscarStatus.Visible = true;
            radioBuscarStatus.Checked = true;
            metroProgressBar1.Visible = false;
            metroLabelProcessando.Visible = false;
            metroTextBox1.BackColor = Color.White;
            metroTextBox1.Visible = false;

            if (versaoRobo == "CAE")
            {
                //metroTabControl1.TabPages.Remove(metroTabPage3);
                //metroTabControl1.TabPages.Remove(metroTabPage4);
                panelMenu.BringToFront();
                btnAlunos.Visible = false;
                btnLogins.Visible = false;
                btUsuarios.Visible = false;
                btLogout.Visible = true;
                //btnCadastrar.Text = "CONSULTAR";
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
                cbExecucao.SelectedIndex = cbExecucao.FindStringExact("ADITAMENTO");
                cbExecucao.Enabled = true;

                //Tirado por não sabermos se será usado sem ser na versão CAE
                txtCPF.Visible = false;
                labelCPF.Visible = false;
                labelCPFCaracteres.Visible = false;
                //txtCPF.Enabled = false;
            }
            radioBaixarDocumento.Checked = true;
            radioBaixarDocumento.Visible = false;
            radioBuscarStatus.Visible = false;
            labelModoOperacao.Visible = false;
            //this.cbExecucao.Items.RemoveAt(0);
            semestre = Dados.SelectSemestre();
            List<String> nomeSemestre = new List<string>();

            foreach (var item in semestre)
            {
                nomeSemestre.Add(item.Semestre);
            }

            this.cbSemestre.DataSource = nomeSemestre;
            //this.cbExecucao.DataSource = Enum.GetValues(typeof(TipoExecucao));
            //this.cbExecucao.SelectedIndex = (int)TipoExecucao.DRM;

            foreach (var item in cbExecucao.Items)
            {
                cbListModosExecucao.Add(item.ToString());
            }
            //this.cbExecucao.AllowDrop = false;
            //this.cbPlataforma.DataSource = Enum.GetValues(typeof(TipoFinanciamento));

            this.cbPlataforma.SelectedIndex = 0;
            this.cbFaculdade.SelectedIndex = 0;
            this.cbFaculdade.SelectedIndex = cbFaculdade.FindStringExact(Program.login.IES.ToUpper());
            this.cbFaculdade.Enabled = true;
            this.cbSemestre.SelectedItem = verificarSemestre;
            //cbFaculdade.SelectedIndex = cbFaculdade.FindStringExact("TODOS");
            //this.cbExecucao.SelectedIndex = 0;

            AtualizarListViewAlunos();
            AtualizarListViewLogins();
            AtualizarListViewUsuarios();
        }
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            //backgroundWorker1.ProgressChanged +=
            //    new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        private void CreateData()
        {
            // Chamando 
            atualizarTransparente(label1, pictureBox1);
            atualizarTransparente(labelDay, pictureBox1);
            atualizarTransparente(dateLabel, pictureBox1);

            atualizarDate(labelDay, dateLabel);

        }
        private void atualizarTransparente(Label label, PictureBox pictureBox)
        {
            label.BackColor = System.Drawing.Color.Transparent;
            label.Parent = pictureBox;
        }

        private void atualizarDate(Label horaMinutosSegundos, Label diaMesAno)
        {
            horaMinutosSegundos.Text = DateTime.Now.ToString("HH.mm.ss");
            diaMesAno.Text = DateTime.Now.ToLongDateString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            timerTransition.Start();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            int countAlunoTO = Dados.CountAluno(typeof(TOAluno));
            if (countAlunoTO > 0)
            {
                string mensagem = "Tem certeza que deseja excluir o banco de dados?";
                mensagem += "\n\nCertifique-se de já ter exportado antes para que nenhuma informação seja perdida!";

                if (MessageBox.Show(mensagem, "Limpar Banco de Dados", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Dados.DeleteTodosAlunos();
                    AtualizarListViewAlunos();
                }
                else
                {
                    return;
                }
            }
            metroTextBox1.Visible = false;
            if (ofdSelectExcel.ShowDialog() == DialogResult.OK)
            {
                txtExcel.Text = ofdSelectExcel.FileName;
                metroLabelProcessando.Visible = true;
                metroProgressBar1.ProgressBarStyle = ProgressBarStyle.Marquee;
                metroProgressBar1.Visible = true;
                metroProgressBar1.MarqueeAnimationSpeed = 5;
                metroProgressBar1.Enabled = true;
                btnSelectPath.Enabled = false;
                btMenu.Enabled = false;
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //            try
            //            {
            if (AreRnsOk())
            {
                string filePath = txtExcel.Text;
                ImportaAlunos(filePath);
                //Importa Alunos INF (Feito pela Trigger no DB)
                //ImportaAlunosInf(filePath);
            }
            //            }
            //            catch (Exception exception)
            //            {
            //                MessageBox.Show(exception.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            finally
            //            {
            Cursor.Current = Cursors.Default;
            //            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                metroLabelProcessando.Visible = false;
                metroProgressBar1.Visible = false;
                btMenu.Enabled = true;
                btnSelectPath.Enabled = true;
                SystemSounds.Exclamation.Play();
                metroTextBox1.Visible = true;
                metroTextBox1.BackColor = Color.Red;
                metroTextBox1.BeginInvoke(
                   new Action(() =>
                   {
                       metroTextBox1.Text = Convert.ToString(e.Error);
                   }
                ));
            }
            else
            {
                metroLabelProcessando.Visible = false;
                metroProgressBar1.Visible = false;
                btMenu.Enabled = true;
                btnSelectPath.Enabled = true;
                AtualizarListViewAlunos();
                SystemSounds.Beep.Play();
                metroTextBox1.Visible = true;
                int qtdAlunosProcessados = Dados.CountAluno(typeof(TOAluno));
                metroTextBox1.BeginInvoke(
                   new Action(() =>
                   {
                       metroTextBox1.Text = "Importação de " + qtdAlunosProcessados + " alunos finalizada com sucesso!";
                   }
                ));
            }
        }

        private void ImportaAlunos(string filePath)
        {
            metroLabelProcessando.BeginInvoke(
                new Action(() =>
                {
                    metroLabelProcessando.Text = "Importando .CSV...";
                }
             ));
            List<TOAluno> alunos = new List<TOAluno>();
            alunos = Dados.BuscarListaAlunos(filePath);
            metroLabelProcessando.BeginInvoke(
                new Action(() =>
                {
                    metroLabelProcessando.Text = "Atualizando BD...";
                }
             ));
            Dados.AtualizarAlunosBD(alunos);
        }

        private bool AreRnsOk()
        {
            if (!File.Exists(txtExcel.Text))
            {
                MessageBox.Show("O diretório do Excel informado não existe.", "Erro de Diretório", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void AtualizarListViewAlunos()
        {
            var source = new BindingSource();
            List<TOAluno> alunos = Dados.SelectAlunos();
            if (alunos.Count == 0)
            {
                dgvAlunos.Visible = false;
                //panelNenhumAluno.Visible = true;
                //tlpAlunos.RowStyles[1].SizeType = SizeType.Absolute;
                //tlpAlunos.RowStyles[1].Height = 30;
            }
            else
            {
                dgvAlunos.Visible = true;
                //panelNenhumAluno.Visible = false;Z
                //tlpAlunos.RowStyles[1].SizeType = SizeType.Absolute;
                //tlpAlunos.RowStyles[1].Height = 0;
                source.DataSource = alunos;
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
            List<TOLogin> logins = Dados.SelectLogins();
            if (logins.Count == 0)
            {
                dgvLogins.Visible = false;
            }
            else
            {
                dgvLogins.Visible = true;
                source.DataSource = logins;
                dgvLogins.AutoGenerateColumns = true;
                dgvLogins.DataSource = source;
                dgvLogins.Columns[dgvLogins.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        public void AtualizarListViewUsuarios()
        {
            var source = new BindingSource();
            List<TOUsuario> Usuarios = Dados.SelectUsuarios();
            for (int i = Usuarios.Count - 1; i >= 0; i--)
            {
                if (Usuarios[i].IES.ToUpper() != Program.login.IES.ToUpper())
                {
                    Usuarios.RemoveAt(i);
                }
            }
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

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //TabPage tabPage = metroTabPage1;
            //metroTabControl1.SelectTab(tabPage);
            panelMenu.Visible = false;
            panelCadastrarContent.Visible = true;
            //panelAlunosContent.Visible = false;
            panelLogins.Visible = false;
            panelCadastrarContent.BringToFront();
            panelSubMenu.Visible = true;
        }

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

                if (Util.IsCpf(txtCPF.Text) == true)
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
                else if (Util.IsCpf(txtCPF.Text) == true)
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

            List<TOLogin> login = SelecionaLogins(cbPlataforma.Text, cbFaculdade.Text);

            if (cbPlataforma.Text.ToUpper() == "FIES LEGADO")
            {
                FiesVelhoExp.OpenFiesVelho(login, cbExecucao.Text, cbCampus.Text, cbSemestre.Text, cbSituacao.Text, cbAno.Text, cbMes.Text);
            }
            else
            {
                FiesNovoExp.OpenFiesNovo(login, cbExecucao.Text, cbSemestre.Text, cbFaculdade.Text, cbAno.Text, cbMes.Text, inicial, final, cbIESRepasse.Text);
            }


        }

        private void ExecutaPrograma(bool CPFUnico = false)
        {
            try
            {
                List<TOLogin> listLogins = Dados.SelectLogins();
                List<TOAluno> alunos = new List<TOAluno>();
                if (CPFUnico == true)
                {
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
                else
                {
                    alunos = Dados.SelectAlunos();
                }
                if (alunos.Count == 0)
                {
                    MessageBox.Show("Banco de Alunos vazio. Por favor importe uma tabela.", "Nenhum aluno encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (listLogins.Count == 0)
                {
                    MessageBox.Show("Banco de Logins vazio. Por favor adicione os Logins necessários.", "Nenhum login encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string numSemestre = string.Empty;
                List<TOLogin> listaLoginPlat;
                List<TOLogin> listaLoginFacul;
                List<TOAluno> listaAlunos;
                foreach (var item in semestre)
                {
                    if (cbSemestre.Text == item.Semestre)
                    {
                        numSemestre = item.numSemestre;
                    }
                }

                String tipoFinanciamento;
                TipoExecucao tipoExecucao;
                Enum.TryParse<TipoExecucao>(cbExecucao.SelectedIndex.ToString(), out tipoExecucao);
                switch (cbPlataforma.SelectedIndex)
                {
                    case (int)TipoFinanciamento.Antigo:
                        //tipoFinanciamento = TipoFinanciamento.Antigo.ToString();
                        tipoFinanciamento = "FIES Legado";
                        listaLoginPlat = SelecionarLoginsPorPlataforma(listLogins, tipoFinanciamento);
                        listaLoginFacul = SelecionarLoginsPorFaculdade(listaLoginPlat);
                        if (CPFUnico == true && versaoRobo == "operacoesFinanceiras")
                        {
                            listaAlunos = SelecionarAlunosPorPlataforma(alunos, tipoFinanciamento, txtCPF.Text);
                        }
                        else
                        {
                            listaAlunos = SelecionarAlunosPorPlataforma(alunos, tipoFinanciamento);
                        }

                        if (cbExecucao.SelectedItem.ToString().Contains("EXTRAIR") == true)
                        {
                            List<TOAluno> alunoInf = new List<TOAluno>();
                            alunoInf = Database.Acess.SelectAll<TOAluno>("ALUNO");
                            FiesVelhoInf.OpenFiesVelho(listaLoginFacul, alunoInf, cbCampus.Text, cbSemestre.Text, cbExecucao.Text, cbSituacao.Text);
                        }
                        else
                        {
                            FiesVelho.OpenFiesVelho(listaLoginFacul, listaAlunos, cbExecucao.Text, cbCampus.Text, numSemestre, cbSemestre.Text, radioBuscarStatus.Checked, cbSituacao.Text);
                        }
                        break;
                    case (int)TipoFinanciamento.Novo:
                        //tipoFinanciamento = TipoFinanciamento.Novo.ToString();
                        tipoFinanciamento = "FIES Novo";
                        listaLoginPlat = SelecionarLoginsPorPlataforma(listLogins, tipoFinanciamento);
                        listaLoginFacul = SelecionarLoginsPorFaculdade(listaLoginPlat);
                        //listaAlunos = SelecionarAlunosPorPlataforma(alunos, tipoFinanciamento);
                        List<TOAluno> teste = new List<TOAluno>();
                        //if (cbExecucao.Text == "EXTRAIR INFORMAÇÕES DRM")
                        //{
                        //    teste = SelecionarAlunosPorPlataforma(teste, tipoFinanciamento);
                        //}
                        //else
                        //{
                        //}
                        teste = SelecionarAlunosPorPlataforma(teste, tipoFinanciamento);

                        //FiesNovo.OpenFiesNovo(listaLoginFacul, listaAlunos, cbExecucao.Text, cbSemestre.Text, radioBuscarStatus.Checked, cbFaculdade.Text);
                        //List<Aluno> teste = listaAlunos.ConvertAll(x => (Aluno)x);
                        MetodosFiesNovo.OpenFiesNovo(listaLoginFacul, teste, cbExecucao.Text, cbSemestre.Text, radioBuscarStatus.Checked);
                        break;
                    default:
                        MessageBox.Show("Plataforma não esperada. Por favor escolha uma plataforma que já tenha sido desenvolvida.", "Plataforma não reconhecida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }

                if (radioBuscarStatus.Checked == true)
                {
                    UpdateDataGridView(cbPlataforma.Text, cbExecucao.Text);
                    MessageBox.Show("Status do aluno atualizado!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Todos alunos executados com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //this.Close();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw exception;
            }
        }

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
                throw new Exception(String.Format("Nenhum login encontrado na plataforma escolhida ({0}). Cheque se o banco de dados contém logins da plataforma que deseja realizar os aditamentos.", plataforma));
            }
            return loginFies;
        }


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
        private List<TOAluno> SelecionarAlunosPorPlataforma(List<TOAluno> alunos, String plataforma, string cpfUnico = "")
        {
            List<TOAluno> alunosFies = new List<TOAluno>();
            alunosFies = Dados.SelectAlunos();
            foreach (TOAluno aluno in alunos)
            {
                Dados.TratarCpf(aluno);
                Dados.TratarTextoReceitas(aluno);
                Dados.TratarPorcentagemReceita(aluno);
                Dados.TratarCampusAluno(aluno);
                Dados.TratarTipoFIES(aluno);

                if (plataforma.Equals(aluno.Tipo))
                {
                    if (aluno.AproveitamentoAtual.Contains("TRANCADO") == true)
                    {
                        aluno.Conclusao = "Trancado";
                        Dados.UpdateAluno(aluno);
                    }
                    else if ("NÃO FEITO".Equals(aluno.Conclusao.ToUpper()) || "Pendente".Equals(aluno.Conclusao) || "DRI não encontrada".Equals(aluno.Conclusao))
                    {
                        if (cpfUnico == "" || cpfUnico == aluno.Cpf)
                        {
                            alunosFies.Add(aluno);
                        }
                    }
                }
            }

            if (alunosFies.Count == 0)
            {
                throw new Exception(String.Format("Nenhum aluno encontrado na plataforma escolhida ({0}). Cheque se o banco de dados contém alunos da plataforma que deseja realizar os aditamentos.", plataforma));
            }
            return alunosFies;
        }

        private List<TOLogin> SelecionarLoginsPorFaculdade(List<TOLogin> logins)
        {
            List<TOLogin> loginUsado = new List<TOLogin>();

            if (cbFaculdade.SelectedIndex > 0)
            {
                string instituicao = cbFaculdade.SelectedItem.ToString();
                foreach (TOLogin login in logins)
                {
                    if (login.Faculdade == instituicao)
                    {
                        loginUsado.Add(login);
                    }
                }

                if (loginUsado.Count == 0)
                {
                    throw new Exception(String.Format("Nenhum login encontrado da faculdade escolhida ({0}). Cheque se o banco de dados contém logins da faculdade que deseja realizar os aditamentos.", instituicao));
                }
                return loginUsado;
            }
            return logins;
        }
        /*
                private void btnLimpar_Click(object sender, EventArgs e)
                {
                    string mensagem = "Tem certeza que deseja excluir o banco de dados?";
                    mensagem += "\n\nCertifique-se de já ter exportado antes para que nenhuma informação seja perdida!";

                    if (MessageBox.Show(mensagem, "Limpar Banco de Dados", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Dados.DeleteTodosAlunos();
                        AtualizarListViewAlunos();
                    }
                }
        */
        private void btnAdicionarLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(this.Location);
            loginForm.ShowDialog();
            AtualizarListViewLogins();
        }

        private void btnModificarLogin_Click(object sender, EventArgs e)
        {
            ModificarLogin();
        }

        private void ModificarLogin()
        {
            LoginForm loginForm = new LoginForm(this.Location, dgvLogins.CurrentRow.DataBoundItem as TOLogin);
            loginForm.ShowDialog();
            AtualizarListViewLogins();
        }

        private void btnExcluirLogin_Click(object sender, EventArgs e)
        {
            ExcluirLogin();
        }

        private void ExcluirLogin()
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
            if (dgvAlunos.Rows.Count > 0)
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
                            ExportarAlunosParaCSV(sfd.FileName);
                            MessageBox.Show("Dados Exportados com Sucesso!!!", "Info");
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }


                    }
                }
            }
            else
            {
                MessageBox.Show("Sem Registro para Exportar!!!", "Info");
            }
        }

        private void ExportarAlunosParaCSV(string fileName)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Cpf", "Cpf");
            dic.Add("Nome", "Nome");
            dic.Add("Tipo", "Tipo");
            dic.Add("Conclusao", "Conclusao");
            dic.Add("HorarioConclusao", "HorarioConclusao");
            dic.Add("Campus", "Campus");
            dic.Add("AproveitamentoAtual", "AproveitamentoAtual");
            dic.Add("HistoricoAproveitamento", "HistoricoAproveitamento");
            dic.Add("ReceitaBruta", "ReceitaBruta");
            dic.Add("ReceitaLiquida", "ReceitaLiquida");
            dic.Add("ReceitaFies", "ReceitaFies");
            dic.Add("CampusAditado", "CampusAditado");
            dic.Add("ValorAditado", "ValorAditado");
            dic.Add("ValorAditadoComDesconto", "ValorAditadoComDesconto");
            dic.Add("ValorAditadoFinanciamento", "ValorAditadoFinanciamento");
            dic.Add("ValorPagoRecursoEstudante", "ValorPagoRecursoEstudante");
            dic.Add("DescontoLiberalidade", "DescontoLiberalidade");
            dic.Add("Extraido", "Extraido");
            dic.Add("Justificativa", "Justificativa");


            List<TOAluno> alunos = Database.Acess.SelectAll<TOAluno>("ALUNO");
            CSVManager.CSVManager.ExportCSV<TOAluno>(fileName, dic, alunos);
        }

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            Util.gravaSenha();
        }

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

        private void cbFaculdade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            List<TOLogin> temp = Dados.SelectLogins();
            ComboBox campusTemp;
            if (cb.Name == "cbFaculdade")
            {
                cbCampus.Items.Clear();
                cbCampus.Items.Add("");
            }
            campusTemp = cbCampus;
            foreach (var login in temp)
            {
                if (login.Plataforma == cbPlataforma.Text)
                {
                    if (cb.Text == "TODOS")
                    {
                        campusTemp.Items.Add(login.Campus);
                        continue;
                    }

                    if (login.Faculdade.ToUpper() == cb.Text.ToUpper())
                    {
                        campusTemp.Items.Add(login.Campus);
                    }
                }
            }
        }

        private void cbExecucao_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearPanel();
            ComboBox cb = (ComboBox)sender;

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

        private void cbPlataforma_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbExecucao.Items.Clear();
            if (cbPlataforma.Text == "FIES Legado")
            {
                //cbExecucao.Items = cbListModosExecucao;
                foreach (var item in cbListModosExecucao)
                {
                    if (item != "HISTÓRICO COPARTICIPAÇÃO" && item != "STATUS ALUNO")
                    {
                        cbExecucao.Items.Add(item);
                    }
                }
                cbCampus.Enabled = true;
            }
            else
            {
                foreach (var item in cbListModosExecucao)
                {
                    if (item != "DRI" && item != "DRD")
                    {
                        cbExecucao.Items.Add(item);
                    }
                }
                cbCampus.SelectedItem = "";
                cbCampus.Enabled = false;
            }
            if (versaoRobo == "operacoesFinanceiras")
            {
                cbExecucao.SelectedIndex = cbExecucao.FindStringExact("DRM");
            }
        }

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToString("HH:mm:ss");
            labelDay.Text = DateTime.Now.ToLongDateString();
        }

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

        private void RoboForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logout == false)
            {
                Application.Exit();
            }
        }

        private void btUsuarios_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            panelLogins.Visible = false;
            painelUsuarios.Visible = true;
            panelCadastrarContent.Visible = false;
            panelPlanilha.Visible = false;
            panelExcel.Visible = false;
            //panelAlunosContent.Visible = false;
        }

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

        private void timerTransition_Tick(object sender, EventArgs e)
        {
            if (Opacity > 0.0)
            {
                this.Opacity -= 0.5;
            }
            else
            {
                timerTransition.Stop();
                this.Close();
            }

        }

        private void ClearPanel()
        {
            foreach (Control item in panelDadosDeSituacao.Controls)
            {
                item.Visible = false;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
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
                        Dados.Exportar_CSV(sfd.FileName);
                        MessageBox.Show("Dados Exportados com Sucesso!!!", "Info");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error :" + ex.Message);
                    }
                }
            }
        }

        private void btCadastro_Click(object sender, EventArgs e)
        {
            panelCadastro.Visible = false;
            panelPlanilha.Visible = false;
        }

        private void btPlanilha_Click(object sender, EventArgs e)
        {
            panelCadastro.Visible = false;
            panelPlanilha.Visible = false;
        }

        private void btExportar_Click(object sender, EventArgs e)
        {
            /*panelCadastro.Visible = false;*/
            panelMenu.Visible = false;
            panelCadastrarContent.Visible = false;
            painelUsuarios.Visible = false;
            panelLogins.Visible = false;
            panelExcel.Visible = false;
            panelPlanilha.Visible = true;
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
    }
}