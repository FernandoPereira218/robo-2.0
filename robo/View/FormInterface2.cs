using Robo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robo.View
{
    public partial class FormInterface2 : Form
    {
        FormDefault formulario;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public static string versaoRobo;
        private bool logout = false;
        private bool verificacaoMAX = false;
        public FormInterface2()
        {
            versaoRobo = Program.login.Permissao;
            InitializeComponent();
            
            lblUsuario.Text = Program.login.Usuario;
            VerificarVersaoCAE();
        }

        private void VerificarVersaoCAE()
        {
            if (versaoRobo == "CAE")
            {
                btnSiga.Visible = false;
                btnConfiguracoes.Visible = false;
            }
        }

        private void btnFiesNovo_Click(object sender, EventArgs e)
        {
            panelCadastrarContent.BringToFront();
            LimparModosDeExecucao();
            SelecionarMenusPorTipoFies("FIES NOVO");
        }
        private void SelecionarMenusPorTipoFies(string tipoFies)
        {
            List<TOMenus> menusFIESNovo = Dados.SelectMenuWhereLite(tipoFies);
            labelTipoFies.Text = tipoFies;

            foreach (var menu in menusFIESNovo)
            {
                Button btn = new Button();
                btn.Text = menu.Item;
                btn.Font = new Font("Century Gothic ", 9.75f);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Size = new Size(flpModosDeExecucao.Width - 23, 40);

                //btn_1.Size = new Size(width, height);
                btn.Tag = menu;
                btn.Click += ExecutarAlgumaCoisa;
                flpModosDeExecucao.Controls.Add(btn);
            }
            //flpModosDeExecucao.Controls[1].Visible = false;
            //flpModosDeExecucao.Controls[2].Visible = false;
        }

        private void ExecutarAlgumaCoisa(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            StartForm((TOMenus)btn.Tag, labelTipoFies.Text.ToUpper());
        }

        private void StartForm(TOMenus menu, string tipoFies)
        {
            int cont = Dados.Count<TOAluno>();
            //panelCadastro.Controls.Clear();
            if (formulario == null)
            {
                
                formulario = new FormDefault(this);
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelCadastro.Controls.Add(formulario);
                formulario.Show();
                formulario.BringToFront();
            }
            lblExecucao.Text = "SISTEMA DE " + menu.Item;
            if (menu.Planilha == true)
            {
                btnPlanilha.Visible = true;
                formulario.setText(cont);
            }
            else
            {
                btnPlanilha.Visible = false;
            }
            formulario.Visible = true;
            formulario.UpdateForm(menu, tipoFies);
            panelCadastro.BringToFront();
            if (cont == 0 && menu.Planilha == true)
            {
                btnPlanilha.Visible = false;
                panelErroNenhumAluno.BringToFront();
            }
        }

        private void LimparModosDeExecucao()
        {
            foreach (System.Windows.Forms.Control item in panelCadastro.Controls)
            {
                if (item.GetType() == typeof(FormDefault))
                {
                    item.Visible = false;
                    break;
                }
            }
            if (flpModosDeExecucao.Controls.Count > 0)
            {
                flpModosDeExecucao.Controls.Clear();
            }
        }

        private void btnFiesLegado_Click(object sender, EventArgs e)
        {
            panelCadastrarContent.BringToFront();
            LimparModosDeExecucao();
            SelecionarMenusPorTipoFies("FIES LEGADO");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (logout == false)
            {
                Application.Exit();
            }
        }

        private void btnSiga_Click(object sender, EventArgs e)
        {
            panelCadastrarContent.BringToFront();
            LimparModosDeExecucao();
            SelecionarMenusPorTipoFies("SIGA");
        }

        private void btnPlanilha_Click(object sender, EventArgs e)
        {
            AtualizarListViewAlunos();
            panelExcel.BringToFront();
        }

        private void btnExecucao_Click(object sender, EventArgs e)
        {
            formulario.Visible = true;
            panelCadastro.BringToFront();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            btnSelectPath.PerformClick();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            panelCadastro.BringToFront();
        }

        private void panelMenuBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnMarcarNaoFeito_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Você realmente deseja marcar todas as conclusões como não feito", "Confirmação", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.No)
            {
                return;
            }
            Dados.UpdateConclusaoAluno("Não Feito");
            AtualizarListViewAlunos();
        }
        private void AtualizarListViewAlunos()
        {
            var source = new BindingSource();
            if (Dados.Count<TOAluno>() == 0)
            {
                dgvAlunos.Visible = false;
            }
            else
            {
                dgvAlunos.Visible = true;
                source.DataSource = Dados.SelectAll<TOAluno>();
                dgvAlunos.AutoGenerateColumns = true;
                dgvAlunos.DataSource = source;
                dgvAlunos.Columns["Cpf"].DisplayIndex = 0;
                dgvAlunos.Columns["Nome"].DisplayIndex = 1;
                dgvAlunos.Columns["Tipo"].DisplayIndex = 2;
                dgvAlunos.Columns["Conclusao"].DisplayIndex = 3;
                dgvAlunos.Columns["HorarioConclusao"].DisplayIndex = 4;


                foreach (DataGridViewColumn item in dgvAlunos.Columns)
                {
                    if (Convert.ToString(dgvAlunos.Rows[0].Cells[item.Name].Value) == "")
                    {
                        dgvAlunos.Columns[item.Name].Visible = false;
                    }
                    else
                    {
                        dgvAlunos.Columns[item.Name].Visible = true;
                    }
                }


            }
        }
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (lblExecucao.Text.Contains("INFORMAÇÕES"))
            {
                Util.ExportarCSV(dgvAlunos.Rows.Count, "Informações");
            }
            else if (lblExecucao.Text.Contains("STATUS ALUNO"))
            {
                Util.ExportarCSV(dgvAlunos.Rows.Count, "Status Aluno");
            }
            else
            {
                Util.ExportarCSV(dgvAlunos.Rows.Count, "Alunos");
            }
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            if (!Dados.VerificaQtdAlunos())
            {
                return;
            }


            AtualizarListViewAlunos();

            lblStatusQuantidadeAlunos.Visible = false;
            ofdSelectExcel.Filter = "CSV (*.csv)|*.csv";

            if (ofdSelectExcel.ShowDialog() == DialogResult.OK)
            {
                if (ofdSelectExcel.FileName != "")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Dados.ImportaAlunos(ofdSelectExcel.FileName, labelTipoFies.Text);
                    Cursor.Current = Cursors.Default;
                    AtualizarListViewAlunos();
                    SystemSounds.Beep.Play();
                    lblStatusQuantidadeAlunos.Visible = true;
                    int qtdAlunosProcessados = Dados.Count<TOAluno>();
                    lblStatusQuantidadeAlunos.Text = "Importação de " + qtdAlunosProcessados + " alunos finalizada com sucesso!";
                    panelExcel.BringToFront();
                    btnPlanilha.Visible = true;
                }
            }

        }

        private void btnConfiguracoes_Click(object sender, EventArgs e)
        {
            if (FormConfiguracoes.verificacao == true)
            {
                return;
            }
            FormConfiguracoes Config = new FormConfiguracoes();
            Config.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            panelHome.BringToFront();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnLogout_Click(object sender, EventArgs e)
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

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (verificacaoMAX == false)
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
                verificacaoMAX = true;
                return;
            }
            if(verificacaoMAX == true)
            {
                this.WindowState = FormWindowState.Normal;
                verificacaoMAX = false;
            }
        }
    }
}

