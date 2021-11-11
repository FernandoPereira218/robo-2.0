﻿using org.apache.commons.lang.time;
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
    public partial class FormInterface : Form
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
        public FormInterface()
        {
            versaoRobo = Program.login.Permissao;
            InitializeComponent();

            lblUsuario.Text = Program.login.Usuario;
            if (Program.login.Usuario != "Admin")
            {
                btnSiga.Visible = false;
            }
            VerificarVersaoCAE();

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            labelDescricaoHome.Text =
                "Um projeto com a têndencia de melhorar e trazer ganhos de tempo \n" +
                "e eficiência para times dos setores, com um sistema de automatização\n" +
                "que atenda de forma clara e precisa nos processos executados.";

            tooltip.SetToolTip(btnFiesLegado, "Operações realizadas no site do MEC");
            tooltip.SetToolTip(btnFiesNovo, "Operações realizadas no site da Caixa");
            tooltip.SetToolTip(btnSiga, "Operações realizadas no site do SIGA");
        }

        private void CriarBotoesModosDeExecucao(string tipoFies)
        {
            List<TOMenus> menusFIESNovo = Dados.SelectMenuWhereLite(tipoFies);
            labelTipoFies.Text = tipoFies;

            foreach (var menu in menusFIESNovo)
            {
                Button btn = new Button();
                btn.Anchor = AnchorStyles.Top;
                btn.Anchor = AnchorStyles.Left;
                btn.Anchor = AnchorStyles.Right;
                btn.Anchor = AnchorStyles.Bottom;
                btn.Text = menu.Item;
                btn.Font = new Font("Century Gothic ", 9.75f);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Size = new Size(flpModosDeExecucao.Width - 23, 40);

                btn.Tag = menu;
                btn.Click += ClickBotoesModoDeExecucao;
                flpModosDeExecucao.Controls.Add(btn);
            }
        }
        private void ClickBotoesModoDeExecucao(object sender, EventArgs e)
        {
            ResetarBotaoEPainel();
            Button btn = (Button)sender;
            StartForm((TOMenus)btn.Tag, labelTipoFies.Text.ToUpper());
        }

        private void ResetarBotaoEPainel()
        {
            panelCadastro.BringToFront();

            btnVoltar.Visible = false;
            btnPlanilha.Visible = false;
        }

        private void StartForm(TOMenus menu, string tipoFies)
        {
            int cont = Dados.Count<TOAluno>();
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
        private void VerificarVersaoCAE()
        {
            if (versaoRobo == "CAE")
            {
                btnSiga.Visible = false;
                btnConfiguracoes.Visible = false;
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
        private void AtualizarListViewAlunos()
        {
            if (Dados.Count<TOAluno>() == 0)
            {
                dgvAlunos.Visible = false;
            }
            else
            {
                dgvAlunos.Visible = true;
                List<TOAluno> alunos = Dados.SelectAll<TOAluno>();
                dgvAlunos.AutoGenerateColumns = true;
                dgvAlunos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvAlunos.DataSource = alunos;

                foreach (DataGridViewColumn item in dgvAlunos.Columns)
                {
                    if (Convert.ToString(dgvAlunos.Rows[0].Cells[item.Name].Value) == "" || item.Name == "Id")
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
        private void OpenPanelCadastro()
        {
            panelCadastrarContent.Visible = true;
            panelHome.Visible = false;
        }

        //Eventos de click
        private void btnFiesNovo_Click(object sender, EventArgs e)
        {
            OpenPanelCadastro();
            LimparModosDeExecucao();
            CriarBotoesModosDeExecucao("FIES NOVO");
            ResetarBotaoEPainel();


        }
        private void btnFiesLegado_Click(object sender, EventArgs e)
        {
            OpenPanelCadastro();
            LimparModosDeExecucao();
            CriarBotoesModosDeExecucao("FIES LEGADO");
            ResetarBotaoEPainel();
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
            OpenPanelCadastro();
            LimparModosDeExecucao();
            CriarBotoesModosDeExecucao("SIGA");
            ResetarBotaoEPainel();
        }
        private void btnPlanilha_Click(object sender, EventArgs e)
        {
            VerificarStatusAluno();
            AtualizarListViewAlunos();

            panelExcel.BringToFront();

            btnVoltar.Visible = true;
            btnPlanilha.Visible = false;
            btnVoltar.Location = new Point(lblExecucao.Size.Width + 40, lblExecucao.Location.Y);

        }
        private void btnExecucao_Click(object sender, EventArgs e)
        {
            formulario.Visible = true;
            panelCadastro.BringToFront();
        }
        private void btnImportar_Click(object sender, EventArgs e)
        {
            btnSelectPath.PerformClick();
            panelExcel.BringToFront();
        }
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            panelCadastro.BringToFront();

            btnVoltar.Visible = false;
            btnPlanilha.Visible = true;
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
                    VerificarStatusAluno();
                    panelExcel.Visible = true;
                    btnPlanilha.Visible = true;
                    btnPlanilha.PerformClick();
                }
            }

        }

        private void VerificarStatusAluno()
        {
            int qtdAlunosProcessados = Dados.Count<TOAluno>();
            if (qtdAlunosProcessados == 0)
            {
                lblStatusQuantidadeAlunos.Text = "Nenhum aluno Exportado no Banco";
            }
            else
            {
                lblStatusQuantidadeAlunos.Text = qtdAlunosProcessados + " alunos importados no Banco de Dados";
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
            panelHome.Visible = true;
            panelCadastrarContent.Visible = false;
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
            if (this.WindowState == FormWindowState.Normal)
            {
                if (this.MaximizedBounds != Screen.FromHandle(this.Handle).WorkingArea)
                {
                    this.WindowState = FormWindowState.Maximized;
                    return;
                }
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
                return;
            }
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        //Eventos de Form
        private void FormInterface2_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnClose.PerformClick();
        }
        private void panelMenuBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

                if (this.Location.Y <= 0)
                {
                    btnMaximize.PerformClick();
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                }
            }
        }
        private void btnChangeColorEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.UseVisualStyleBackColor = false;
            button.ForeColor = SystemColors.Control;
            button.BackColor = Color.Purple;
        }
        private void btnChangeColorLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.UseVisualStyleBackColor = true;
            button.ForeColor = Color.FromArgb(0, 45, 45, 45);
            button.BackColor = SystemColors.Control;
        }
    }
}