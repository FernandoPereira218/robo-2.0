
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle94 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle95 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle96 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMenuBar = new System.Windows.Forms.Panel();
            this.btnMaximize = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
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
            this.btnImportar = new System.Windows.Forms.Button();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bwBarraProgresso = new System.ComponentModel.BackgroundWorker();
            this.panelSubMenu = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnSiga = new System.Windows.Forms.Button();
            this.btnFiesLegado = new System.Windows.Forms.Button();
            this.btnFiesNovo = new System.Windows.Forms.Button();
            this.btnConfiguracoes = new System.Windows.Forms.Button();
            this.panelHome = new System.Windows.Forms.Panel();
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
            this.panelMenuBar.BackColor = System.Drawing.Color.Purple;
            this.panelMenuBar.Controls.Add(this.btnMaximize);
            this.panelMenuBar.Controls.Add(this.lblUsuario);
            this.panelMenuBar.Controls.Add(this.label6);
            this.panelMenuBar.Controls.Add(this.btnMinimize);
            this.panelMenuBar.Controls.Add(this.btnClose);
            this.panelMenuBar.ForeColor = System.Drawing.Color.White;
            this.panelMenuBar.Location = new System.Drawing.Point(0, 0);
            this.panelMenuBar.Margin = new System.Windows.Forms.Padding(2);
            this.panelMenuBar.Name = "panelMenuBar";
            this.panelMenuBar.Size = new System.Drawing.Size(1280, 40);
            this.panelMenuBar.TabIndex = 1;
            this.panelMenuBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMenuBar_MouseDown);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.ForeColor = System.Drawing.Color.White;
            this.btnMaximize.Location = new System.Drawing.Point(1190, 0);
            this.btnMaximize.Margin = new System.Windows.Forms.Padding(2);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(45, 40);
            this.btnMaximize.TabIndex = 13;
            this.btnMaximize.Text = "_";
            this.btnMaximize.UseVisualStyleBackColor = true;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblUsuario.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(1104, 16);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(42, 16);
            this.lblUsuario.TabIndex = 12;
            this.lblUsuario.Text = "Admin";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Purple;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(12, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 16);
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
            this.btnMinimize.Location = new System.Drawing.Point(1151, 0);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(45, 40);
            this.btnMinimize.TabIndex = 2;
            this.btnMinimize.Text = "_";
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1235, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 40);
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
            this.panelExcel.Location = new System.Drawing.Point(231, 41);
            this.panelExcel.Name = "panelExcel";
            this.panelExcel.Size = new System.Drawing.Size(974, 667);
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
            this.btnVoltar.Location = new System.Drawing.Point(-27, -8);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(66, 73);
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
            this.lblStatusQuantidadeAlunos.Location = new System.Drawing.Point(170, 24);
            this.lblStatusQuantidadeAlunos.Name = "lblStatusQuantidadeAlunos";
            this.lblStatusQuantidadeAlunos.Size = new System.Drawing.Size(213, 19);
            this.lblStatusQuantidadeAlunos.TabIndex = 35;
            this.lblStatusQuantidadeAlunos.Text = "Digite e Selecione os dados abaixo";
            this.lblStatusQuantidadeAlunos.Visible = false;
            // 
            // dgvAlunos
            // 
            this.dgvAlunos.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvAlunos.AllowUserToAddRows = false;
            this.dgvAlunos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle94.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAlunos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle94;
            this.dgvAlunos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvAlunos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAlunos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAlunos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlunos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle95.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle95.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle95.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle95.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle95.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle95.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle95.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlunos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle95;
            this.dgvAlunos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlunos.Location = new System.Drawing.Point(-27, 65);
            this.dgvAlunos.Name = "dgvAlunos";
            this.dgvAlunos.ReadOnly = true;
            this.dgvAlunos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvAlunos.RowHeadersVisible = false;
            this.dgvAlunos.RowHeadersWidth = 51;
            dataGridViewCellStyle96.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAlunos.RowsDefaultCellStyle = dataGridViewCellStyle96;
            this.dgvAlunos.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAlunos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlunos.Size = new System.Drawing.Size(1050, 601);
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
            this.btnMarcarNaoFeito.Location = new System.Drawing.Point(775, 12);
            this.btnMarcarNaoFeito.Name = "btnMarcarNaoFeito";
            this.btnMarcarNaoFeito.Size = new System.Drawing.Size(114, 22);
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
            this.btnExportarExcel.Location = new System.Drawing.Point(886, 12);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(114, 22);
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
            this.btnSelectPath.Location = new System.Drawing.Point(701, 12);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(123, 22);
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
            this.panelCadastrarContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelCadastrarContent.BackColor = System.Drawing.Color.White;
            this.panelCadastrarContent.Controls.Add(this.panelMenuExecucao);
            this.panelCadastrarContent.Controls.Add(this.panelExecucao);
            this.panelCadastrarContent.Controls.Add(this.panelCadastro);
            this.panelCadastrarContent.Controls.Add(this.panelErroNenhumAluno);
            this.panelCadastrarContent.Controls.Add(this.panelExcel);
            this.panelCadastrarContent.Location = new System.Drawing.Point(76, 40);
            this.panelCadastrarContent.Name = "panelCadastrarContent";
            this.panelCadastrarContent.Size = new System.Drawing.Size(1207, 686);
            this.panelCadastrarContent.TabIndex = 3;
            // 
            // panelMenuExecucao
            // 
            this.panelMenuExecucao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMenuExecucao.BackColor = System.Drawing.SystemColors.Control;
            this.panelMenuExecucao.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMenuExecucao.Controls.Add(this.lblExecucao);
            this.panelMenuExecucao.Controls.Add(this.btnPlanilha);
            this.panelMenuExecucao.ForeColor = System.Drawing.SystemColors.Control;
            this.panelMenuExecucao.Location = new System.Drawing.Point(0, -5);
            this.panelMenuExecucao.Name = "panelMenuExecucao";
            this.panelMenuExecucao.Size = new System.Drawing.Size(1205, 67);
            this.panelMenuExecucao.TabIndex = 15;
            // 
            // lblExecucao
            // 
            this.lblExecucao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExecucao.AutoSize = true;
            this.lblExecucao.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblExecucao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.lblExecucao.Location = new System.Drawing.Point(15, 21);
            this.lblExecucao.Name = "lblExecucao";
            this.lblExecucao.Size = new System.Drawing.Size(106, 25);
            this.lblExecucao.TabIndex = 64;
            this.lblExecucao.Text = "Robo Anima";
            this.lblExecucao.UseCustomBackColor = true;
            this.lblExecucao.UseCustomForeColor = true;
            this.lblExecucao.UseStyleColors = true;
            // 
            // btnPlanilha
            // 
            this.btnPlanilha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlanilha.BackColor = System.Drawing.Color.White;
            this.btnPlanilha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlanilha.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnPlanilha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlanilha.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlanilha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnPlanilha.Location = new System.Drawing.Point(1094, 17);
            this.btnPlanilha.Name = "btnPlanilha";
            this.btnPlanilha.Size = new System.Drawing.Size(96, 29);
            this.btnPlanilha.TabIndex = 63;
            this.btnPlanilha.Text = "Planilha";
            this.btnPlanilha.UseVisualStyleBackColor = false;
            this.btnPlanilha.Visible = false;
            this.btnPlanilha.Click += new System.EventHandler(this.btnPlanilha_Click);
            // 
            // panelExecucao
            // 
            this.panelExecucao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelExecucao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelExecucao.Controls.Add(this.panel1);
            this.panelExecucao.Controls.Add(this.flpModosDeExecucao);
            this.panelExecucao.Location = new System.Drawing.Point(-1, 60);
            this.panelExecucao.Name = "panelExecucao";
            this.panelExecucao.Size = new System.Drawing.Size(232, 620);
            this.panelExecucao.TabIndex = 61;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.labelTipoFies);
            this.panel1.Location = new System.Drawing.Point(0, -14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 86);
            this.panel1.TabIndex = 66;
            // 
            // labelTipoFies
            // 
            this.labelTipoFies.BackColor = System.Drawing.SystemColors.Control;
            this.labelTipoFies.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelTipoFies.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelTipoFies.Location = new System.Drawing.Point(2, 11);
            this.labelTipoFies.Name = "labelTipoFies";
            this.labelTipoFies.Size = new System.Drawing.Size(232, 75);
            this.labelTipoFies.TabIndex = 65;
            this.labelTipoFies.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTipoFies.UseCustomBackColor = true;
            this.labelTipoFies.UseCustomForeColor = true;
            this.labelTipoFies.UseStyleColors = true;
            // 
            // flpModosDeExecucao
            // 
            this.flpModosDeExecucao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.flpModosDeExecucao.AutoScroll = true;
            this.flpModosDeExecucao.BackColor = System.Drawing.SystemColors.Control;
            this.flpModosDeExecucao.ForeColor = System.Drawing.Color.Gray;
            this.flpModosDeExecucao.Location = new System.Drawing.Point(1, 68);
            this.flpModosDeExecucao.MaximumSize = new System.Drawing.Size(231, 1080);
            this.flpModosDeExecucao.MinimumSize = new System.Drawing.Size(231, 500);
            this.flpModosDeExecucao.Name = "flpModosDeExecucao";
            this.flpModosDeExecucao.Size = new System.Drawing.Size(231, 554);
            this.flpModosDeExecucao.TabIndex = 0;
            // 
            // panelCadastro
            // 
            this.panelCadastro.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCadastro.BackColor = System.Drawing.Color.White;
            this.panelCadastro.Location = new System.Drawing.Point(230, 61);
            this.panelCadastro.Name = "panelCadastro";
            this.panelCadastro.Size = new System.Drawing.Size(977, 619);
            this.panelCadastro.TabIndex = 38;
            // 
            // panelErroNenhumAluno
            // 
            this.panelErroNenhumAluno.BackColor = System.Drawing.Color.White;
            this.panelErroNenhumAluno.Controls.Add(this.btnImportar);
            this.panelErroNenhumAluno.Controls.Add(this.metroLabel2);
            this.panelErroNenhumAluno.Controls.Add(this.metroLabel1);
            this.panelErroNenhumAluno.Location = new System.Drawing.Point(230, 61);
            this.panelErroNenhumAluno.Name = "panelErroNenhumAluno";
            this.panelErroNenhumAluno.Size = new System.Drawing.Size(970, 667);
            this.panelErroNenhumAluno.TabIndex = 62;
            // 
            // btnImportar
            // 
            this.btnImportar.BackColor = System.Drawing.Color.Green;
            this.btnImportar.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnImportar.Image = global::robo.Properties.Resources.excel_1_;
            this.btnImportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportar.Location = new System.Drawing.Point(533, 319);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(173, 48);
            this.btnImportar.TabIndex = 38;
            this.btnImportar.Text = "Importar";
            this.btnImportar.UseVisualStyleBackColor = false;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(421, 282);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(419, 25);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Importe uma Planilha para fazer a Execução desejada";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(596, 257);
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
            this.panelSubMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelSubMenu.BackColor = System.Drawing.SystemColors.Control;
            this.panelSubMenu.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSubMenu.Controls.Add(this.btnLogout);
            this.panelSubMenu.Controls.Add(this.btnHome);
            this.panelSubMenu.Controls.Add(this.btnSiga);
            this.panelSubMenu.Controls.Add(this.btnFiesLegado);
            this.panelSubMenu.Controls.Add(this.btnFiesNovo);
            this.panelSubMenu.Controls.Add(this.btnConfiguracoes);
            this.panelSubMenu.ForeColor = System.Drawing.Color.White;
            this.panelSubMenu.Location = new System.Drawing.Point(-2, 35);
            this.panelSubMenu.Name = "panelSubMenu";
            this.panelSubMenu.Size = new System.Drawing.Size(83, 733);
            this.panelSubMenu.TabIndex = 38;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnLogout.Location = new System.Drawing.Point(1, 544);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(74, 67);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnHome
            // 
            this.btnHome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHome.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnHome.Image = global::robo.Properties.Resources.contorno_da_casa;
            this.btnHome.Location = new System.Drawing.Point(1, 474);
            this.btnHome.Margin = new System.Windows.Forms.Padding(0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(77, 67);
            this.btnHome.TabIndex = 9;
            this.btnHome.Text = "Home";
            this.btnHome.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnSiga
            // 
            this.btnSiga.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSiga.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiga.FlatAppearance.BorderSize = 0;
            this.btnSiga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiga.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiga.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnSiga.Image = global::robo.Properties.Resources.rotunda;
            this.btnSiga.Location = new System.Drawing.Point(1, 132);
            this.btnSiga.Margin = new System.Windows.Forms.Padding(0);
            this.btnSiga.Name = "btnSiga";
            this.btnSiga.Size = new System.Drawing.Size(77, 67);
            this.btnSiga.TabIndex = 8;
            this.btnSiga.Text = "SIGA";
            this.btnSiga.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSiga.UseVisualStyleBackColor = true;
            this.btnSiga.Click += new System.EventHandler(this.btnSiga_Click);
            // 
            // btnFiesLegado
            // 
            this.btnFiesLegado.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFiesLegado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiesLegado.FlatAppearance.BorderSize = 0;
            this.btnFiesLegado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiesLegado.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiesLegado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnFiesLegado.Image = global::robo.Properties.Resources.old_tv_3_;
            this.btnFiesLegado.Location = new System.Drawing.Point(1, 1);
            this.btnFiesLegado.Margin = new System.Windows.Forms.Padding(0);
            this.btnFiesLegado.Name = "btnFiesLegado";
            this.btnFiesLegado.Size = new System.Drawing.Size(77, 67);
            this.btnFiesLegado.TabIndex = 7;
            this.btnFiesLegado.Text = "Legado";
            this.btnFiesLegado.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFiesLegado.UseVisualStyleBackColor = true;
            this.btnFiesLegado.Click += new System.EventHandler(this.btnFiesLegado_Click);
            // 
            // btnFiesNovo
            // 
            this.btnFiesNovo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFiesNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiesNovo.FlatAppearance.BorderSize = 0;
            this.btnFiesNovo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiesNovo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiesNovo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnFiesNovo.Image = global::robo.Properties.Resources.tv;
            this.btnFiesNovo.Location = new System.Drawing.Point(0, 65);
            this.btnFiesNovo.Margin = new System.Windows.Forms.Padding(0);
            this.btnFiesNovo.Name = "btnFiesNovo";
            this.btnFiesNovo.Size = new System.Drawing.Size(77, 67);
            this.btnFiesNovo.TabIndex = 6;
            this.btnFiesNovo.Text = "FIES Novo";
            this.btnFiesNovo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFiesNovo.UseVisualStyleBackColor = true;
            this.btnFiesNovo.Click += new System.EventHandler(this.btnFiesNovo_Click);
            // 
            // btnConfiguracoes
            // 
            this.btnConfiguracoes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfiguracoes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConfiguracoes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfiguracoes.FlatAppearance.BorderSize = 0;
            this.btnConfiguracoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguracoes.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfiguracoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnConfiguracoes.Image = global::robo.Properties.Resources.configuracoes;
            this.btnConfiguracoes.Location = new System.Drawing.Point(1, 614);
            this.btnConfiguracoes.Margin = new System.Windows.Forms.Padding(0);
            this.btnConfiguracoes.Name = "btnConfiguracoes";
            this.btnConfiguracoes.Size = new System.Drawing.Size(77, 67);
            this.btnConfiguracoes.TabIndex = 3;
            this.btnConfiguracoes.Text = "Ajustes";
            this.btnConfiguracoes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnConfiguracoes.UseVisualStyleBackColor = true;
            this.btnConfiguracoes.Click += new System.EventHandler(this.btnConfiguracoes_Click);
            // 
            // panelHome
            // 
            this.panelHome.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHome.Location = new System.Drawing.Point(76, 40);
            this.panelHome.Name = "panelHome";
            this.panelHome.Size = new System.Drawing.Size(1204, 686);
            this.panelHome.TabIndex = 0;
            // 
            // FormInterface2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelMenuBar);
            this.Controls.Add(this.panelSubMenu);
            this.Controls.Add(this.panelCadastrarContent);
            this.Controls.Add(this.panelHome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Button btnConfiguracoes;
        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.BackgroundWorker bwBarraProgresso;
        private System.Windows.Forms.Panel panelSubMenu;
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
        public System.Windows.Forms.FlowLayoutPanel flpModosDeExecucao;
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
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Panel panelHome;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnMaximize;
    }
}