
namespace robo.View
{
    partial class FormInterface2
    {
        /// <summary>     
        /// Required designer variable.     
        /// </summary>     
        private System.ComponentModel.IContainer components = null;

        /// <summary>     
        /// Clean up any resources being used.     
        /// </summary>     
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>     
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code     

        /// <summary>     
        /// Required method for Designer support - do not modify     
        /// the contents of this method with the code editor.     
        /// </summary>     
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMenuBar = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelExcel = new System.Windows.Forms.Panel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.lblStatusQuantidadeAlunos = new MetroFramework.Controls.MetroLabel();
            this.dgvAlunos = new System.Windows.Forms.DataGridView();
            this.btnMarcarNaoFeito = new System.Windows.Forms.Button();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.ofdSelectExcel = new System.Windows.Forms.OpenFileDialog();
            this.panelCadastrarContent = new System.Windows.Forms.Panel();
            this.panelMenuExecucao = new System.Windows.Forms.Panel();
            this.lblExecucao = new MetroFramework.Controls.MetroLabel();
            this.btnPlanilha = new System.Windows.Forms.Button();
            this.panelExecucao = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTipoFies = new MetroFramework.Controls.MetroLabel();
            this.flpModosDeExecucao = new System.Windows.Forms.FlowLayoutPanel();
            this.panelCadastro = new System.Windows.Forms.Panel();
            this.panelErroNenhumAluno = new System.Windows.Forms.Panel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bwBarraProgresso = new System.ComponentModel.BackgroundWorker();
            this.panelSubMenu = new System.Windows.Forms.Panel();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnSiga = new System.Windows.Forms.Button();
            this.btnFiesLegado = new System.Windows.Forms.Button();
            this.btnFiesNovo = new System.Windows.Forms.Button();
            this.btnLogins = new System.Windows.Forms.Button();
            this.btExtrairInformacoes = new System.Windows.Forms.Button();
            this.panelMenuBar.SuspendLayout();
            this.panelExcel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos)).BeginInit();
            this.panelCadastrarContent.SuspendLayout();
            this.panelMenuExecucao.SuspendLayout();
            this.panelExecucao.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelErroNenhumAluno.SuspendLayout();
            this.panelSubMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenuBar
            // 
            this.panelMenuBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMenuBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelMenuBar.Controls.Add(this.label6);
            this.panelMenuBar.Controls.Add(this.btnMinimize);
            this.panelMenuBar.Controls.Add(this.btnClose);
            this.panelMenuBar.Location = new System.Drawing.Point(0, 0);
            this.panelMenuBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMenuBar.Name = "panelMenuBar";
            this.panelMenuBar.Size = new System.Drawing.Size(1821, 53);
            this.panelMenuBar.TabIndex = 1;
            this.panelMenuBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMenuBar_MouseDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(9, 17);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "Anima Educação";
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new System.Drawing.Point(1763, 0);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(29, 31);
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.Text = "_";
            this.btnMinimize.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1791, -1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 32);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelExcel
            // 
            this.panelExcel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelExcel.BackColor = System.Drawing.Color.White;
            this.panelExcel.Controls.Add(this.btnVoltar);
            this.panelExcel.Controls.Add(this.lblStatusQuantidadeAlunos);
            this.panelExcel.Controls.Add(this.dgvAlunos);
            this.panelExcel.Controls.Add(this.btnMarcarNaoFeito);
            this.panelExcel.Controls.Add(this.btnExportarExcel);
            this.panelExcel.Controls.Add(this.btnSelectPath);
            this.panelExcel.Location = new System.Drawing.Point(307, 75);
            this.panelExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelExcel.Name = "panelExcel";
            this.panelExcel.Size = new System.Drawing.Size(1429, 820);
            this.panelExcel.TabIndex = 0;
            // 
            // btnVoltar
            // 
            this.btnVoltar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnVoltar.BackColor = System.Drawing.Color.White;
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoltar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnVoltar.Location = new System.Drawing.Point(29, -10);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(88, 90);
            this.btnVoltar.TabIndex = 11;
            this.btnVoltar.Text = "←";
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // lblStatusQuantidadeAlunos
            // 
            this.lblStatusQuantidadeAlunos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusQuantidadeAlunos.AutoSize = true;
            this.lblStatusQuantidadeAlunos.Location = new System.Drawing.Point(227, 30);
            this.lblStatusQuantidadeAlunos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatusQuantidadeAlunos.Name = "lblStatusQuantidadeAlunos";
            this.lblStatusQuantidadeAlunos.Size = new System.Drawing.Size(223, 20);
            this.lblStatusQuantidadeAlunos.TabIndex = 35;
            this.lblStatusQuantidadeAlunos.Text = "Digite e Selecione os dados abaixo";
            this.lblStatusQuantidadeAlunos.Visible = false;
            // 
            // dgvAlunos
            // 
            this.dgvAlunos.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvAlunos.AllowUserToAddRows = false;
            this.dgvAlunos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAlunos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAlunos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvAlunos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAlunos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAlunos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlunos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlunos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAlunos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlunos.Location = new System.Drawing.Point(29, 80);
            this.dgvAlunos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvAlunos.Name = "dgvAlunos";
            this.dgvAlunos.ReadOnly = true;
            this.dgvAlunos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvAlunos.RowHeadersVisible = false;
            this.dgvAlunos.RowHeadersWidth = 51;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAlunos.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAlunos.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAlunos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlunos.Size = new System.Drawing.Size(1400, 740);
            this.dgvAlunos.TabIndex = 0;
            // 
            // btnMarcarNaoFeito
            // 
            this.btnMarcarNaoFeito.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMarcarNaoFeito.BackColor = System.Drawing.Color.White;
            this.btnMarcarNaoFeito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMarcarNaoFeito.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnMarcarNaoFeito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarcarNaoFeito.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMarcarNaoFeito.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnMarcarNaoFeito.Location = new System.Drawing.Point(1099, 15);
            this.btnMarcarNaoFeito.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMarcarNaoFeito.Name = "btnMarcarNaoFeito";
            this.btnMarcarNaoFeito.Size = new System.Drawing.Size(152, 27);
            this.btnMarcarNaoFeito.TabIndex = 10;
            this.btnMarcarNaoFeito.Text = "Marcar não feito";
            this.btnMarcarNaoFeito.UseVisualStyleBackColor = false;
            this.btnMarcarNaoFeito.Click += new System.EventHandler(this.btnMarcarNaoFeito_Click);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExportarExcel.BackColor = System.Drawing.Color.White;
            this.btnExportarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportarExcel.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarExcel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnExportarExcel.Location = new System.Drawing.Point(1247, 15);
            this.btnExportarExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(152, 27);
            this.btnExportarExcel.TabIndex = 5;
            this.btnExportarExcel.Text = "Exportar Excel";
            this.btnExportarExcel.UseVisualStyleBackColor = false;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.BackColor = System.Drawing.Color.Silver;
            this.btnSelectPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectPath.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSelectPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectPath.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnSelectPath.Location = new System.Drawing.Point(935, 15);
            this.btnSelectPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(164, 27);
            this.btnSelectPath.TabIndex = 2;
            this.btnSelectPath.Text = "Localizar Arquivo";
            this.btnSelectPath.UseVisualStyleBackColor = false;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // ofdSelectExcel
            // 
            this.ofdSelectExcel.FileName = "openFileDialog1";
            // 
            // panelCadastrarContent
            // 
            this.panelCadastrarContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCadastrarContent.AutoScroll = true;
            this.panelCadastrarContent.AutoSize = true;
            this.panelCadastrarContent.BackColor = System.Drawing.Color.White;
            this.panelCadastrarContent.Controls.Add(this.panelMenuExecucao);
            this.panelCadastrarContent.Controls.Add(this.panelExecucao);
            this.panelCadastrarContent.Controls.Add(this.panelCadastro);
            this.panelCadastrarContent.Controls.Add(this.panelErroNenhumAluno);
            this.panelCadastrarContent.Controls.Add(this.panelExcel);
            this.panelCadastrarContent.Location = new System.Drawing.Point(85, 49);
            this.panelCadastrarContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelCadastrarContent.MaximumSize = new System.Drawing.Size(2560, 1329);
            this.panelCadastrarContent.MinimumSize = new System.Drawing.Size(1821, 945);
            this.panelCadastrarContent.Name = "panelCadastrarContent";
            this.panelCadastrarContent.Size = new System.Drawing.Size(2171, 1329);
            this.panelCadastrarContent.TabIndex = 3;
            // 
            // panelMenuExecucao
            // 
            this.panelMenuExecucao.BackColor = System.Drawing.Color.White;
            this.panelMenuExecucao.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMenuExecucao.Controls.Add(this.lblExecucao);
            this.panelMenuExecucao.Controls.Add(this.btnPlanilha);
            this.panelMenuExecucao.Location = new System.Drawing.Point(0, -1);
            this.panelMenuExecucao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMenuExecucao.Name = "panelMenuExecucao";
            this.panelMenuExecucao.Size = new System.Drawing.Size(1736, 77);
            this.panelMenuExecucao.TabIndex = 15;
            // 
            // lblExecucao
            // 
            this.lblExecucao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExecucao.AutoSize = true;
            this.lblExecucao.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblExecucao.Location = new System.Drawing.Point(13, 21);
            this.lblExecucao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExecucao.Name = "lblExecucao";
            this.lblExecucao.Size = new System.Drawing.Size(112, 25);
            this.lblExecucao.TabIndex = 64;
            this.lblExecucao.Text = "Robo Anima";
            // 
            // btnPlanilha
            // 
            this.btnPlanilha.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPlanilha.BackColor = System.Drawing.Color.White;
            this.btnPlanilha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlanilha.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnPlanilha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlanilha.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlanilha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnPlanilha.Location = new System.Drawing.Point(1599, 20);
            this.btnPlanilha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPlanilha.Name = "btnPlanilha";
            this.btnPlanilha.Size = new System.Drawing.Size(128, 36);
            this.btnPlanilha.TabIndex = 63;
            this.btnPlanilha.Text = "Planilha";
            this.btnPlanilha.UseVisualStyleBackColor = false;
            this.btnPlanilha.Visible = false;
            this.btnPlanilha.Click += new System.EventHandler(this.btnPlanilha_Click);
            // 
            // panelExecucao
            // 
            this.panelExecucao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelExecucao.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelExecucao.Controls.Add(this.panel1);
            this.panelExecucao.Controls.Add(this.flpModosDeExecucao);
            this.panelExecucao.Location = new System.Drawing.Point(0, 74);
            this.panelExecucao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelExecucao.Name = "panelExecucao";
            this.panelExecucao.Size = new System.Drawing.Size(307, 825);
            this.panelExecucao.TabIndex = 61;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.labelTipoFies);
            this.panel1.Location = new System.Drawing.Point(-9, -17);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 95);
            this.panel1.TabIndex = 66;
            // 
            // labelTipoFies
            // 
            this.labelTipoFies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTipoFies.BackColor = System.Drawing.Color.White;
            this.labelTipoFies.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelTipoFies.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelTipoFies.Location = new System.Drawing.Point(-20, 14);
            this.labelTipoFies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTipoFies.Name = "labelTipoFies";
            this.labelTipoFies.Size = new System.Drawing.Size(335, 82);
            this.labelTipoFies.TabIndex = 65;
            this.labelTipoFies.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTipoFies.UseCustomBackColor = true;
            this.labelTipoFies.UseCustomForeColor = true;
            this.labelTipoFies.UseStyleColors = true;
            // 
            // flpModosDeExecucao
            // 
            this.flpModosDeExecucao.AutoScroll = true;
            this.flpModosDeExecucao.BackColor = System.Drawing.Color.White;
            this.flpModosDeExecucao.ForeColor = System.Drawing.Color.Gray;
            this.flpModosDeExecucao.Location = new System.Drawing.Point(-3, 74);
            this.flpModosDeExecucao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flpModosDeExecucao.Name = "flpModosDeExecucao";
            this.flpModosDeExecucao.Size = new System.Drawing.Size(308, 746);
            this.flpModosDeExecucao.TabIndex = 0;
            // 
            // panelCadastro
            // 
            this.panelCadastro.BackColor = System.Drawing.Color.White;
            this.panelCadastro.Location = new System.Drawing.Point(307, 75);
            this.panelCadastro.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelCadastro.Name = "panelCadastro";
            this.panelCadastro.Size = new System.Drawing.Size(1429, 825);
            this.panelCadastro.TabIndex = 38;
            // 
            // panelErroNenhumAluno
            // 
            this.panelErroNenhumAluno.BackColor = System.Drawing.Color.White;
            this.panelErroNenhumAluno.Controls.Add(this.btnImportar);
            this.panelErroNenhumAluno.Controls.Add(this.metroLabel2);
            this.panelErroNenhumAluno.Controls.Add(this.metroLabel1);
            this.panelErroNenhumAluno.Location = new System.Drawing.Point(307, 75);
            this.panelErroNenhumAluno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelErroNenhumAluno.Name = "panelErroNenhumAluno";
            this.panelErroNenhumAluno.Size = new System.Drawing.Size(1429, 821);
            this.panelErroNenhumAluno.TabIndex = 62;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(561, 347);
            this.metroLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(435, 25);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Importe uma Planilha para fazer a Execução desejada";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(795, 316);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(57, 25);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Ops...";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // bwBarraProgresso
            // 
            this.bwBarraProgresso.WorkerReportsProgress = true;
            this.bwBarraProgresso.WorkerSupportsCancellation = true;
            // 
            // panelSubMenu
            // 
            this.panelSubMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelSubMenu.Controls.Add(this.btnSiga);
            this.panelSubMenu.Controls.Add(this.btnFiesLegado);
            this.panelSubMenu.Controls.Add(this.btnFiesNovo);
            this.panelSubMenu.Controls.Add(this.btnLogins);
            this.panelSubMenu.Controls.Add(this.btExtrairInformacoes);
            this.panelSubMenu.Location = new System.Drawing.Point(0, 49);
            this.panelSubMenu.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSubMenu.Name = "panelSubMenu";
            this.panelSubMenu.Size = new System.Drawing.Size(93, 896);
            this.panelSubMenu.TabIndex = 38;
            // 
            // btnImportar
            // 
            this.btnImportar.BackColor = System.Drawing.Color.Green;
            this.btnImportar.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnImportar.Image = global::robo.Properties.Resources.excel_1_;
            this.btnImportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportar.Location = new System.Drawing.Point(711, 393);
            this.btnImportar.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(231, 59);
            this.btnImportar.TabIndex = 38;
            this.btnImportar.Text = "Importar";
            this.btnImportar.UseVisualStyleBackColor = false;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnSiga
            // 
            this.btnSiga.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSiga.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSiga.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiga.FlatAppearance.BorderSize = 0;
            this.btnSiga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiga.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiga.ForeColor = System.Drawing.Color.Silver;
            this.btnSiga.Image = global::robo.Properties.Resources.admin;
            this.btnSiga.Location = new System.Drawing.Point(1, 731);
            this.btnSiga.Margin = new System.Windows.Forms.Padding(0);
            this.btnSiga.Name = "btnSiga";
            this.btnSiga.Size = new System.Drawing.Size(92, 82);
            this.btnSiga.TabIndex = 8;
            this.btnSiga.Text = "SIGA";
            this.btnSiga.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSiga.UseVisualStyleBackColor = true;
            this.btnSiga.Click += new System.EventHandler(this.btnSiga_Click);
            // 
            // btnFiesLegado
            // 
            this.btnFiesLegado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFiesLegado.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFiesLegado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiesLegado.FlatAppearance.BorderSize = 0;
            this.btnFiesLegado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiesLegado.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiesLegado.ForeColor = System.Drawing.Color.Silver;
            this.btnFiesLegado.Image = global::robo.Properties.Resources.admin;
            this.btnFiesLegado.Location = new System.Drawing.Point(1, 567);
            this.btnFiesLegado.Margin = new System.Windows.Forms.Padding(0);
            this.btnFiesLegado.Name = "btnFiesLegado";
            this.btnFiesLegado.Size = new System.Drawing.Size(92, 82);
            this.btnFiesLegado.TabIndex = 7;
            this.btnFiesLegado.Text = "FIES Legado";
            this.btnFiesLegado.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFiesLegado.UseVisualStyleBackColor = true;
            this.btnFiesLegado.Click += new System.EventHandler(this.btnFiesLegado_Click);
            // 
            // btnFiesNovo
            // 
            this.btnFiesNovo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFiesNovo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFiesNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiesNovo.FlatAppearance.BorderSize = 0;
            this.btnFiesNovo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiesNovo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiesNovo.ForeColor = System.Drawing.Color.Silver;
            this.btnFiesNovo.Image = global::robo.Properties.Resources.admin;
            this.btnFiesNovo.Location = new System.Drawing.Point(0, 649);
            this.btnFiesNovo.Margin = new System.Windows.Forms.Padding(0);
            this.btnFiesNovo.Name = "btnFiesNovo";
            this.btnFiesNovo.Size = new System.Drawing.Size(92, 82);
            this.btnFiesNovo.TabIndex = 6;
            this.btnFiesNovo.Text = "FIES Novo";
            this.btnFiesNovo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFiesNovo.UseVisualStyleBackColor = true;
            this.btnFiesNovo.Click += new System.EventHandler(this.btnFiesNovo_Click);
            // 
            // btnLogins
            // 
            this.btnLogins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogins.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLogins.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogins.FlatAppearance.BorderSize = 0;
            this.btnLogins.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogins.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogins.ForeColor = System.Drawing.Color.Silver;
            this.btnLogins.Image = global::robo.Properties.Resources.admin;
            this.btnLogins.Location = new System.Drawing.Point(0, 813);
            this.btnLogins.Margin = new System.Windows.Forms.Padding(0);
            this.btnLogins.Name = "btnLogins";
            this.btnLogins.Size = new System.Drawing.Size(92, 82);
            this.btnLogins.TabIndex = 3;
            this.btnLogins.Text = "Configurações";
            this.btnLogins.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLogins.UseVisualStyleBackColor = true;
            this.btnLogins.Click += new System.EventHandler(this.btnConfiguracoes);
            // 
            // btExtrairInformacoes
            // 
            this.btExtrairInformacoes.FlatAppearance.BorderSize = 0;
            this.btExtrairInformacoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExtrairInformacoes.ForeColor = System.Drawing.Color.Silver;
            this.btExtrairInformacoes.Image = global::robo.Properties.Resources.silhueta_negra_de_casa_sem_porta;
            this.btExtrairInformacoes.Location = new System.Drawing.Point(4, 499);
            this.btExtrairInformacoes.Margin = new System.Windows.Forms.Padding(4);
            this.btExtrairInformacoes.Name = "btExtrairInformacoes";
            this.btExtrairInformacoes.Size = new System.Drawing.Size(85, 77);
            this.btExtrairInformacoes.TabIndex = 3;
            this.btExtrairInformacoes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btExtrairInformacoes.UseVisualStyleBackColor = true;
            // 
            // FormInterface2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1821, 945);
            this.Controls.Add(this.panelSubMenu);
            this.Controls.Add(this.panelCadastrarContent);
            this.Controls.Add(this.panelMenuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormInterface2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panelMenuBar.ResumeLayout(false);
            this.panelMenuBar.PerformLayout();
            this.panelExcel.ResumeLayout(false);
            this.panelExcel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos)).EndInit();
            this.panelCadastrarContent.ResumeLayout(false);
            this.panelMenuExecucao.ResumeLayout(false);
            this.panelMenuExecucao.PerformLayout();
            this.panelExecucao.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelErroNenhumAluno.ResumeLayout(false);
            this.panelErroNenhumAluno.PerformLayout();
            this.panelSubMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelMenuBar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Panel panelExcel;
        public System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.DataGridView dgvAlunos;
        private System.Windows.Forms.OpenFileDialog ofdSelectExcel;
        private System.Windows.Forms.Panel panelCadastrarContent;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnLogins;
        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.BackgroundWorker bwBarraProgresso;
        private System.Windows.Forms.Panel panelSubMenu;
        private System.Windows.Forms.Button btExtrairInformacoes;
        private System.Windows.Forms.Button btnMarcarNaoFeito;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>


        #endregion
        private System.Windows.Forms.Panel panelMenuExecucao;
        private MetroFramework.Controls.MetroLabel lblExecucao;
        private System.Windows.Forms.Button btnPlanilha;
        private System.Windows.Forms.Panel panelExecucao;
        private System.Windows.Forms.Button btnFiesLegado;
        private System.Windows.Forms.Button btnFiesNovo;
        private System.Windows.Forms.FlowLayoutPanel flpModosDeExecucao;
        private MetroFramework.Controls.MetroLabel labelTipoFies;
        private System.Windows.Forms.Panel panelCadastro;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSiga;
        private System.Windows.Forms.Panel panelErroNenhumAluno;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnVoltar;
        private MetroFramework.Controls.MetroLabel lblStatusQuantidadeAlunos;
    }
}