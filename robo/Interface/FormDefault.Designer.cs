﻿
namespace robo.Interface
{
    partial class FormDefault
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
            this.panelCadastro = new System.Windows.Forms.Panel();
            this.panellStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.circularProgressBar1 = new CircularProgressBar.CircularProgressBar();
            this.wbHelp = new System.Windows.Forms.WebBrowser();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelCabecalho = new System.Windows.Forms.Panel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lblExecucao = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel17 = new MetroFramework.Controls.MetroLabel();
            this.panelCPF = new System.Windows.Forms.Panel();
            this.txtCPF = new System.Windows.Forms.MaskedTextBox();
            this.labelCPF = new MetroFramework.Controls.MetroLabel();
            this.labelCPFCaracteres = new MetroFramework.Controls.MetroLabel();
            this.panelImportar = new System.Windows.Forms.Panel();
            this.btnImportar = new System.Windows.Forms.Button();
            this.lblAlunosImportados = new MetroFramework.Controls.MetroLabel();
            this.panelIES = new System.Windows.Forms.Panel();
            this.cbIES = new System.Windows.Forms.ComboBox();
            this.labelIES = new MetroFramework.Controls.MetroLabel();
            this.labelAvisoIES = new MetroFramework.Controls.MetroLabel();
            this.panelCampus = new System.Windows.Forms.Panel();
            this.labelCampus = new MetroFramework.Controls.MetroLabel();
            this.cbCampus = new System.Windows.Forms.ComboBox();
            this.panelSemestre = new System.Windows.Forms.Panel();
            this.cbSemestre = new System.Windows.Forms.ComboBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.panelDataInicioEFim = new System.Windows.Forms.Panel();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.labelDataFim = new MetroFramework.Controls.MetroLabel();
            this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
            this.labelDataInicio = new MetroFramework.Controls.MetroLabel();
            this.panelAnoEMes = new System.Windows.Forms.Panel();
            this.labelAno = new MetroFramework.Controls.MetroLabel();
            this.labelMes = new MetroFramework.Controls.MetroLabel();
            this.cbMes = new System.Windows.Forms.ComboBox();
            this.cbAno = new System.Windows.Forms.ComboBox();
            this.panelSituacao = new System.Windows.Forms.Panel();
            this.labelSituacao = new MetroFramework.Controls.MetroLabel();
            this.cbSituacao = new System.Windows.Forms.ComboBox();
            this.panelIESRepasse = new System.Windows.Forms.Panel();
            this.labelIESRepasse = new MetroFramework.Controls.MetroLabel();
            this.cbIESRepasse = new System.Windows.Forms.ComboBox();
            this.panelFiesSiga = new System.Windows.Forms.Panel();
            this.labelFiesSiga = new MetroFramework.Controls.MetroLabel();
            this.cbFiesSiga = new System.Windows.Forms.ComboBox();
            this.panelTodosMesesDisponiveis = new System.Windows.Forms.Panel();
            this.checkBoxTodosMeses = new MetroFramework.Controls.MetroCheckBox();
            this.labelTodosMeses = new MetroFramework.Controls.MetroLabel();
            this.labelDay = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.tooltip = new MetroFramework.Components.MetroToolTip();
            this.panelTipoValor = new System.Windows.Forms.Panel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.cbTipoValor = new System.Windows.Forms.ComboBox();
            this.panelCadastro.SuspendLayout();
            this.panellStatus.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelCabecalho.SuspendLayout();
            this.panelCPF.SuspendLayout();
            this.panelImportar.SuspendLayout();
            this.panelIES.SuspendLayout();
            this.panelCampus.SuspendLayout();
            this.panelSemestre.SuspendLayout();
            this.panelDataInicioEFim.SuspendLayout();
            this.panelAnoEMes.SuspendLayout();
            this.panelSituacao.SuspendLayout();
            this.panelIESRepasse.SuspendLayout();
            this.panelFiesSiga.SuspendLayout();
            this.panelTodosMesesDisponiveis.SuspendLayout();
            this.panelTipoValor.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCadastro
            // 
            this.panelCadastro.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCadastro.BackColor = System.Drawing.Color.White;
            this.panelCadastro.Controls.Add(this.panellStatus);
            this.panelCadastro.Controls.Add(this.circularProgressBar1);
            this.panelCadastro.Controls.Add(this.wbHelp);
            this.panelCadastro.Controls.Add(this.flowLayoutPanel1);
            this.panelCadastro.Controls.Add(this.labelDay);
            this.panelCadastro.Controls.Add(this.btnIniciar);
            this.panelCadastro.Location = new System.Drawing.Point(0, 0);
            this.panelCadastro.Name = "panelCadastro";
            this.panelCadastro.Size = new System.Drawing.Size(1072, 700);
            this.panelCadastro.TabIndex = 39;
            // 
            // panellStatus
            // 
            this.panellStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panellStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panellStatus.Controls.Add(this.lblStatus);
            this.panellStatus.Location = new System.Drawing.Point(0, 587);
            this.panellStatus.Margin = new System.Windows.Forms.Padding(2);
            this.panellStatus.Name = "panellStatus";
            this.panellStatus.Size = new System.Drawing.Size(1032, 24);
            this.panellStatus.TabIndex = 66;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(128)))));
            this.lblStatus.Location = new System.Drawing.Point(2, 3);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 15);
            this.lblStatus.TabIndex = 67;
            this.lblStatus.Text = "Status";
            // 
            // circularProgressBar1
            // 
            this.circularProgressBar1.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.circularProgressBar1.AnimationSpeed = 500;
            this.circularProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.circularProgressBar1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.circularProgressBar1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circularProgressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.circularProgressBar1.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.circularProgressBar1.InnerMargin = 2;
            this.circularProgressBar1.InnerWidth = -1;
            this.circularProgressBar1.Location = new System.Drawing.Point(412, 203);
            this.circularProgressBar1.MarqueeAnimationSpeed = 2000;
            this.circularProgressBar1.Name = "circularProgressBar1";
            this.circularProgressBar1.OuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(62)))), ((int)(((byte)(102)))));
            this.circularProgressBar1.OuterMargin = -25;
            this.circularProgressBar1.OuterWidth = 26;
            this.circularProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(128)))));
            this.circularProgressBar1.ProgressWidth = 25;
            this.circularProgressBar1.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.circularProgressBar1.Size = new System.Drawing.Size(239, 244);
            this.circularProgressBar1.StartAngle = 270;
            this.circularProgressBar1.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.circularProgressBar1.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.circularProgressBar1.SubscriptText = "";
            this.circularProgressBar1.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.circularProgressBar1.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.circularProgressBar1.SuperscriptText = "";
            this.circularProgressBar1.TabIndex = 62;
            this.circularProgressBar1.Text = "Processando...";
            this.circularProgressBar1.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.circularProgressBar1.Value = 68;
            this.circularProgressBar1.Visible = false;
            // 
            // wbHelp
            // 
            this.wbHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbHelp.CausesValidation = false;
            this.wbHelp.Location = new System.Drawing.Point(644, 15);
            this.wbHelp.Margin = new System.Windows.Forms.Padding(0);
            this.wbHelp.Name = "wbHelp";
            this.wbHelp.Size = new System.Drawing.Size(377, 552);
            this.wbHelp.TabIndex = 63;
            this.wbHelp.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.panelCabecalho);
            this.flowLayoutPanel1.Controls.Add(this.panelCPF);
            this.flowLayoutPanel1.Controls.Add(this.panelImportar);
            this.flowLayoutPanel1.Controls.Add(this.panelIES);
            this.flowLayoutPanel1.Controls.Add(this.panelCampus);
            this.flowLayoutPanel1.Controls.Add(this.panelSemestre);
            this.flowLayoutPanel1.Controls.Add(this.panelDataInicioEFim);
            this.flowLayoutPanel1.Controls.Add(this.panelAnoEMes);
            this.flowLayoutPanel1.Controls.Add(this.panelSituacao);
            this.flowLayoutPanel1.Controls.Add(this.panelIESRepasse);
            this.flowLayoutPanel1.Controls.Add(this.panelFiesSiga);
            this.flowLayoutPanel1.Controls.Add(this.panelTodosMesesDisponiveis);
            this.flowLayoutPanel1.Controls.Add(this.panelTipoValor);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(33, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(605, 506);
            this.flowLayoutPanel1.TabIndex = 37;
            // 
            // panelCabecalho
            // 
            this.panelCabecalho.Controls.Add(this.btnHelp);
            this.panelCabecalho.Controls.Add(this.lblExecucao);
            this.panelCabecalho.Controls.Add(this.metroLabel1);
            this.panelCabecalho.Controls.Add(this.metroLabel17);
            this.panelCabecalho.Location = new System.Drawing.Point(3, 3);
            this.panelCabecalho.Name = "panelCabecalho";
            this.panelCabecalho.Size = new System.Drawing.Size(573, 100);
            this.panelCabecalho.TabIndex = 71;
            this.panelCabecalho.Tag = "";
            this.panelCabecalho.Visible = false;
            // 
            // btnHelp
            // 
            this.btnHelp.BackgroundImage = global::robo.Properties.Resources.question_mark;
            this.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Location = new System.Drawing.Point(108, 6);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(31, 23);
            this.btnHelp.TabIndex = 36;
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // lblExecucao
            // 
            this.lblExecucao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExecucao.AutoSize = true;
            this.lblExecucao.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblExecucao.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblExecucao.Location = new System.Drawing.Point(3, 6);
            this.lblExecucao.Name = "lblExecucao";
            this.lblExecucao.Size = new System.Drawing.Size(110, 25);
            this.lblExecucao.TabIndex = 33;
            this.lblExecucao.Text = "Robo Ritter";
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(3, 31);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(213, 19);
            this.metroLabel1.TabIndex = 34;
            this.metroLabel1.Text = "Digite e Selecione os dados abaixo";
            // 
            // metroLabel17
            // 
            this.metroLabel17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel17.AutoSize = true;
            this.metroLabel17.Location = new System.Drawing.Point(2, 31);
            this.metroLabel17.Name = "metroLabel17";
            this.metroLabel17.Size = new System.Drawing.Size(213, 19);
            this.metroLabel17.TabIndex = 34;
            this.metroLabel17.Text = "Digite e Selecione os dados abaixo";
            // 
            // panelCPF
            // 
            this.panelCPF.Controls.Add(this.txtCPF);
            this.panelCPF.Controls.Add(this.labelCPF);
            this.panelCPF.Controls.Add(this.labelCPFCaracteres);
            this.panelCPF.Location = new System.Drawing.Point(3, 109);
            this.panelCPF.Name = "panelCPF";
            this.panelCPF.Size = new System.Drawing.Size(573, 73);
            this.panelCPF.TabIndex = 64;
            this.panelCPF.Visible = false;
            // 
            // txtCPF
            // 
            this.txtCPF.Location = new System.Drawing.Point(13, 26);
            this.txtCPF.Name = "txtCPF";
            this.txtCPF.Size = new System.Drawing.Size(532, 20);
            this.txtCPF.TabIndex = 35;
            this.txtCPF.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtCPF_MouseClick);
            this.txtCPF.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCPF_KeyPress);
            // 
            // labelCPF
            // 
            this.labelCPF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCPF.AutoSize = true;
            this.labelCPF.Location = new System.Drawing.Point(12, 4);
            this.labelCPF.Name = "labelCPF";
            this.labelCPF.Size = new System.Drawing.Size(33, 19);
            this.labelCPF.TabIndex = 28;
            this.labelCPF.Text = "CPF";
            // 
            // labelCPFCaracteres
            // 
            this.labelCPFCaracteres.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCPFCaracteres.AutoSize = true;
            this.labelCPFCaracteres.FontSize = MetroFramework.MetroLabelSize.Small;
            this.labelCPFCaracteres.Location = new System.Drawing.Point(419, 47);
            this.labelCPFCaracteres.Name = "labelCPFCaracteres";
            this.labelCPFCaracteres.Size = new System.Drawing.Size(116, 15);
            this.labelCPFCaracteres.TabIndex = 35;
            this.labelCPFCaracteres.Text = "(11 caracteres Exatos*)";
            // 
            // panelImportar
            // 
            this.panelImportar.Controls.Add(this.btnImportar);
            this.panelImportar.Controls.Add(this.lblAlunosImportados);
            this.panelImportar.Location = new System.Drawing.Point(3, 188);
            this.panelImportar.Name = "panelImportar";
            this.panelImportar.Size = new System.Drawing.Size(573, 100);
            this.panelImportar.TabIndex = 72;
            this.panelImportar.Tag = "";
            this.panelImportar.Visible = false;
            // 
            // btnImportar
            // 
            this.btnImportar.BackColor = System.Drawing.Color.Green;
            this.btnImportar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnImportar.Image = global::robo.Properties.Resources.excel_1_;
            this.btnImportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportar.Location = new System.Drawing.Point(13, 44);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(211, 35);
            this.btnImportar.TabIndex = 37;
            this.btnImportar.Text = "Atualizar";
            this.btnImportar.UseVisualStyleBackColor = false;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // lblAlunosImportados
            // 
            this.lblAlunosImportados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlunosImportados.Location = new System.Drawing.Point(12, 23);
            this.lblAlunosImportados.Name = "lblAlunosImportados";
            this.lblAlunosImportados.Size = new System.Drawing.Size(411, 16);
            this.lblAlunosImportados.TabIndex = 34;
            this.lblAlunosImportados.Text = "Digite e Selecione os dados abaixo";
            this.lblAlunosImportados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelIES
            // 
            this.panelIES.Controls.Add(this.cbIES);
            this.panelIES.Controls.Add(this.labelIES);
            this.panelIES.Controls.Add(this.labelAvisoIES);
            this.panelIES.Location = new System.Drawing.Point(3, 294);
            this.panelIES.Name = "panelIES";
            this.panelIES.Size = new System.Drawing.Size(573, 94);
            this.panelIES.TabIndex = 62;
            this.panelIES.Tag = "";
            this.panelIES.Visible = false;
            // 
            // cbIES
            // 
            this.cbIES.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbIES.BackColor = System.Drawing.SystemColors.Control;
            this.cbIES.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbIES.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIES.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIES.FormattingEnabled = true;
            this.cbIES.Items.AddRange(new object[] {
            "TODOS",
            "FADERGS",
            "UNIRITTER",
            "IBMR",
            "UnP",
            "FPB"});
            this.cbIES.Location = new System.Drawing.Point(13, 38);
            this.cbIES.Name = "cbIES";
            this.cbIES.Size = new System.Drawing.Size(536, 25);
            this.cbIES.TabIndex = 2;
            this.cbIES.Tag = "";
            this.cbIES.SelectedIndexChanged += new System.EventHandler(this.cbIES_SelectedIndexChanged);
            // 
            // labelIES
            // 
            this.labelIES.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelIES.AutoSize = true;
            this.labelIES.Location = new System.Drawing.Point(12, 16);
            this.labelIES.Name = "labelIES";
            this.labelIES.Size = new System.Drawing.Size(26, 19);
            this.labelIES.TabIndex = 1;
            this.labelIES.Tag = "";
            this.labelIES.Text = "IES";
            // 
            // labelAvisoIES
            // 
            this.labelAvisoIES.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAvisoIES.AutoSize = true;
            this.labelAvisoIES.FontSize = MetroFramework.MetroLabelSize.Small;
            this.labelAvisoIES.Location = new System.Drawing.Point(311, 66);
            this.labelAvisoIES.Name = "labelAvisoIES";
            this.labelAvisoIES.Size = new System.Drawing.Size(242, 15);
            this.labelAvisoIES.TabIndex = 37;
            this.labelAvisoIES.Text = "(Troque de conta para poder acessar outra IES)";
            // 
            // panelCampus
            // 
            this.panelCampus.Controls.Add(this.labelCampus);
            this.panelCampus.Controls.Add(this.cbCampus);
            this.panelCampus.Location = new System.Drawing.Point(3, 394);
            this.panelCampus.Name = "panelCampus";
            this.panelCampus.Size = new System.Drawing.Size(573, 76);
            this.panelCampus.TabIndex = 63;
            this.panelCampus.Visible = false;
            // 
            // labelCampus
            // 
            this.labelCampus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCampus.AutoSize = true;
            this.labelCampus.Location = new System.Drawing.Point(9, 6);
            this.labelCampus.Name = "labelCampus";
            this.labelCampus.Size = new System.Drawing.Size(57, 19);
            this.labelCampus.TabIndex = 3;
            this.labelCampus.Tag = "";
            this.labelCampus.Text = "Campus";
            // 
            // cbCampus
            // 
            this.cbCampus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCampus.BackColor = System.Drawing.SystemColors.Control;
            this.cbCampus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbCampus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCampus.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCampus.FormattingEnabled = true;
            this.cbCampus.Location = new System.Drawing.Point(12, 28);
            this.cbCampus.Name = "cbCampus";
            this.cbCampus.Size = new System.Drawing.Size(536, 25);
            this.cbCampus.TabIndex = 4;
            this.cbCampus.Tag = "";
            // 
            // panelSemestre
            // 
            this.panelSemestre.Controls.Add(this.cbSemestre);
            this.panelSemestre.Controls.Add(this.metroLabel4);
            this.panelSemestre.Location = new System.Drawing.Point(3, 476);
            this.panelSemestre.Name = "panelSemestre";
            this.panelSemestre.Size = new System.Drawing.Size(573, 53);
            this.panelSemestre.TabIndex = 65;
            this.panelSemestre.Tag = "";
            this.panelSemestre.Visible = false;
            // 
            // cbSemestre
            // 
            this.cbSemestre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSemestre.BackColor = System.Drawing.SystemColors.Control;
            this.cbSemestre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbSemestre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSemestre.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSemestre.FormattingEnabled = true;
            this.cbSemestre.Items.AddRange(new object[] {
            "FIES Legado",
            "FIES Novo"});
            this.cbSemestre.Location = new System.Drawing.Point(9, 21);
            this.cbSemestre.Name = "cbSemestre";
            this.cbSemestre.Size = new System.Drawing.Size(536, 25);
            this.cbSemestre.TabIndex = 9;
            // 
            // metroLabel4
            // 
            this.metroLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(9, 2);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(63, 19);
            this.metroLabel4.TabIndex = 24;
            this.metroLabel4.Text = "Semestre";
            // 
            // panelDataInicioEFim
            // 
            this.panelDataInicioEFim.Controls.Add(this.dtpDataInicial);
            this.panelDataInicioEFim.Controls.Add(this.labelDataFim);
            this.panelDataInicioEFim.Controls.Add(this.dtpDataFinal);
            this.panelDataInicioEFim.Controls.Add(this.labelDataInicio);
            this.panelDataInicioEFim.Location = new System.Drawing.Point(3, 535);
            this.panelDataInicioEFim.Name = "panelDataInicioEFim";
            this.panelDataInicioEFim.Size = new System.Drawing.Size(573, 65);
            this.panelDataInicioEFim.TabIndex = 66;
            this.panelDataInicioEFim.Tag = "";
            this.panelDataInicioEFim.Visible = false;
            // 
            // dtpDataInicial
            // 
            this.dtpDataInicial.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataInicial.Location = new System.Drawing.Point(17, 28);
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Size = new System.Drawing.Size(137, 22);
            this.dtpDataInicial.TabIndex = 45;
            // 
            // labelDataFim
            // 
            this.labelDataFim.AutoSize = true;
            this.labelDataFim.Location = new System.Drawing.Point(162, 6);
            this.labelDataFim.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDataFim.Name = "labelDataFim";
            this.labelDataFim.Size = new System.Drawing.Size(62, 19);
            this.labelDataFim.TabIndex = 52;
            this.labelDataFim.Text = "Data Fim";
            // 
            // dtpDataFinal
            // 
            this.dtpDataFinal.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataFinal.Location = new System.Drawing.Point(160, 28);
            this.dtpDataFinal.Name = "dtpDataFinal";
            this.dtpDataFinal.Size = new System.Drawing.Size(128, 22);
            this.dtpDataFinal.TabIndex = 46;
            // 
            // labelDataInicio
            // 
            this.labelDataInicio.AutoSize = true;
            this.labelDataInicio.Location = new System.Drawing.Point(17, 6);
            this.labelDataInicio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDataInicio.Name = "labelDataInicio";
            this.labelDataInicio.Size = new System.Drawing.Size(70, 19);
            this.labelDataInicio.TabIndex = 51;
            this.labelDataInicio.Text = "Data Inicio";
            // 
            // panelAnoEMes
            // 
            this.panelAnoEMes.Controls.Add(this.labelAno);
            this.panelAnoEMes.Controls.Add(this.labelMes);
            this.panelAnoEMes.Controls.Add(this.cbMes);
            this.panelAnoEMes.Controls.Add(this.cbAno);
            this.panelAnoEMes.Location = new System.Drawing.Point(3, 606);
            this.panelAnoEMes.Name = "panelAnoEMes";
            this.panelAnoEMes.Size = new System.Drawing.Size(573, 63);
            this.panelAnoEMes.TabIndex = 67;
            this.panelAnoEMes.Visible = false;
            // 
            // labelAno
            // 
            this.labelAno.AutoSize = true;
            this.labelAno.Location = new System.Drawing.Point(15, 11);
            this.labelAno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAno.Name = "labelAno";
            this.labelAno.Size = new System.Drawing.Size(33, 19);
            this.labelAno.TabIndex = 54;
            this.labelAno.Text = "Ano";
            // 
            // labelMes
            // 
            this.labelMes.AutoSize = true;
            this.labelMes.Location = new System.Drawing.Point(160, 11);
            this.labelMes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMes.Name = "labelMes";
            this.labelMes.Size = new System.Drawing.Size(33, 19);
            this.labelMes.TabIndex = 53;
            this.labelMes.Text = "Mês";
            // 
            // cbMes
            // 
            this.cbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMes.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMes.FormattingEnabled = true;
            this.cbMes.Items.AddRange(new object[] {
            "Janeiro",
            "Fevereiro",
            "Março",
            "Abril",
            "Maio",
            "Junho",
            "Julho",
            "Agosto",
            "Setembro",
            "Outubro",
            "Novembro",
            "Dezembro"});
            this.cbMes.Location = new System.Drawing.Point(158, 33);
            this.cbMes.Name = "cbMes";
            this.cbMes.Size = new System.Drawing.Size(128, 25);
            this.cbMes.TabIndex = 44;
            // 
            // cbAno
            // 
            this.cbAno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAno.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAno.FormattingEnabled = true;
            this.cbAno.Items.AddRange(new object[] {
            "",
            "2010",
            "2011",
            "2012",
            "2013",
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021"});
            this.cbAno.Location = new System.Drawing.Point(15, 33);
            this.cbAno.Name = "cbAno";
            this.cbAno.Size = new System.Drawing.Size(137, 25);
            this.cbAno.TabIndex = 43;
            // 
            // panelSituacao
            // 
            this.panelSituacao.Controls.Add(this.labelSituacao);
            this.panelSituacao.Controls.Add(this.cbSituacao);
            this.panelSituacao.Location = new System.Drawing.Point(3, 675);
            this.panelSituacao.Name = "panelSituacao";
            this.panelSituacao.Size = new System.Drawing.Size(573, 71);
            this.panelSituacao.TabIndex = 68;
            this.panelSituacao.Visible = false;
            // 
            // labelSituacao
            // 
            this.labelSituacao.AutoSize = true;
            this.labelSituacao.Location = new System.Drawing.Point(14, 15);
            this.labelSituacao.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSituacao.Name = "labelSituacao";
            this.labelSituacao.Size = new System.Drawing.Size(58, 19);
            this.labelSituacao.TabIndex = 55;
            this.labelSituacao.Text = "Situação";
            // 
            // cbSituacao
            // 
            this.cbSituacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSituacao.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSituacao.FormattingEnabled = true;
            this.cbSituacao.Items.AddRange(new object[] {
            "",
            "Contratado",
            "Contrato Cancelado",
            "Contrato encerrado",
            "Contrato pendente de correcao",
            "Contrato pendente de validacao",
            "Em preenchimento pelo aluno",
            "Enviado ao banco",
            "Pendente de validacao pela CPSA",
            "Reaberto pela CPSA para correcao",
            "Recebido pelo banco",
            "Rejeitado pela CPSA",
            "Validado pela CPSA",
            "Vencido",
            "Prorrogado"});
            this.cbSituacao.Location = new System.Drawing.Point(15, 35);
            this.cbSituacao.Name = "cbSituacao";
            this.cbSituacao.Size = new System.Drawing.Size(530, 25);
            this.cbSituacao.TabIndex = 42;
            // 
            // panelIESRepasse
            // 
            this.panelIESRepasse.Controls.Add(this.labelIESRepasse);
            this.panelIESRepasse.Controls.Add(this.cbIESRepasse);
            this.panelIESRepasse.Location = new System.Drawing.Point(3, 752);
            this.panelIESRepasse.Name = "panelIESRepasse";
            this.panelIESRepasse.Size = new System.Drawing.Size(573, 68);
            this.panelIESRepasse.TabIndex = 69;
            this.panelIESRepasse.Tag = "";
            this.panelIESRepasse.Visible = false;
            // 
            // labelIESRepasse
            // 
            this.labelIESRepasse.AutoSize = true;
            this.labelIESRepasse.Location = new System.Drawing.Point(14, 11);
            this.labelIESRepasse.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelIESRepasse.Name = "labelIESRepasse";
            this.labelIESRepasse.Size = new System.Drawing.Size(77, 19);
            this.labelIESRepasse.TabIndex = 58;
            this.labelIESRepasse.Text = "IES Repasse";
            // 
            // cbIESRepasse
            // 
            this.cbIESRepasse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIESRepasse.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIESRepasse.FormattingEnabled = true;
            this.cbIESRepasse.Items.AddRange(new object[] {
            "488 - CENTRO UNIVERSITÁRIO RITTER DOS REIS",
            "5317 - FACULDADE PORTO ALEGRENSE",
            "2950 - Centro Universitário FADERGS"});
            this.cbIESRepasse.Location = new System.Drawing.Point(15, 31);
            this.cbIESRepasse.Name = "cbIESRepasse";
            this.cbIESRepasse.Size = new System.Drawing.Size(528, 25);
            this.cbIESRepasse.TabIndex = 57;
            // 
            // panelFiesSiga
            // 
            this.panelFiesSiga.Controls.Add(this.labelFiesSiga);
            this.panelFiesSiga.Controls.Add(this.cbFiesSiga);
            this.panelFiesSiga.Location = new System.Drawing.Point(3, 826);
            this.panelFiesSiga.Name = "panelFiesSiga";
            this.panelFiesSiga.Size = new System.Drawing.Size(573, 61);
            this.panelFiesSiga.TabIndex = 70;
            this.panelFiesSiga.Tag = "";
            this.panelFiesSiga.Visible = false;
            // 
            // labelFiesSiga
            // 
            this.labelFiesSiga.AutoSize = true;
            this.labelFiesSiga.Location = new System.Drawing.Point(13, 12);
            this.labelFiesSiga.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFiesSiga.Name = "labelFiesSiga";
            this.labelFiesSiga.Size = new System.Drawing.Size(131, 19);
            this.labelFiesSiga.TabIndex = 60;
            this.labelFiesSiga.Text = "LANÇAMENTO SIGA";
            // 
            // cbFiesSiga
            // 
            this.cbFiesSiga.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiesSiga.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFiesSiga.FormattingEnabled = true;
            this.cbFiesSiga.Items.AddRange(new object[] {
            "ABATIMENTO DE TROCA DE CURSO (+)",
            "ABATIMENTO DO VALOR PAGO A MAIOR (-)",
            "ABATIMENTO FACILITA (-)",
            "ABATIMENTO PRÉ-MATRICULA - Prioridade 49 (-) (-)",
            "ABATIMENTO PRÉ-MATRICULA - Prioridade 99 (-) (-)",
            "ABATIMENTOS DE CREDITOS RECEBIDOS (-)",
            "ACRÉSCIMO FACILITA (+)",
            "AJUSTE DE MENSALIDADE - Prioridade 1 (-) (-)",
            "AJUSTE DE MENSALIDADE - Prioridade 1 (+) (+)",
            "AJUSTE DE MENSALIDADE - Prioridade 99 (-) (-)",
            "AJUSTE DE MENSALIDADE - Prioridade 99 (+) (+)",
            "BOLSA ACORDO RESCISÃO (-)",
            "BOLSA CARÊNCIA (-)",
            "BOLSA COMERCIAL (TODO CURSO) - CV (-)",
            "BOLSA COMERCIAL (TODO CURSO) (-)",
            "BOLSA COMERCIAL (VALIDADE 1 ANO) (-)",
            "BOLSA DA CAPES (-)",
            "BOLSA DESEMPENHO ACADÊMICO GRADUAÇÃO (-)",
            "BOLSA DISCIPLINA ADICIONAL (-)",
            "BOLSA FAPERGS (-)",
            "BOLSA INGRESSANTE - CV (-)",
            "BOLSA INGRESSANTE (-)",
            "BOLSA MONITORIA DE ENSINO/EXTENSÃO (-)",
            "BOLSA PROUNI (-)",
            "BOLSA RETURNING (-)",
            "BOLSA REVERSÃO - CAC (-)",
            "BOLSA REVERSÃO - RETENÇÃO (-)",
            "BOLSA UNIPOA (-)",
            "CAMPANHA DE MARKETING (-)",
            "CREDIES (-)",
            "DEDUÇÃO JUDICIAL (-)",
            "DESC. COLABORADOR PÓS/MEST/DOUT - CV (-)",
            "DESC. EX-ALUNO INSTITUIÇÃO - CV (-)",
            "DESC. GRUPO DE ALUNOS - CV (-)",
            "DESCONTO CANCELAMENTO DE TURMA/TURNO (-)",
            "DESCONTO COLABORADOR (-)",
            "DESCONTO DE REMATRICULA (-)",
            "DESCONTO DEPENDENTE DE COLABORADOR/ESTAGIÁRIO (-)",
            "DESCONTO ESPECIAL (-)",
            "DESCONTO EX-ALUNO INSTITUIÇÃO (-)",
            "DESCONTO FINANCEIRO - CAC (49) (-)",
            "DESCONTO FINANCEIRO - CAC (99) (-)",
            "DESCONTO FINANCEIRO - RETENÇÃO (49) (-)",
            "DESCONTO FINANCEIRO - RETENÇÃO (99) (-)",
            "DESCONTO GRUPO DE EMPRESA CONVENIADA - CV (-)",
            "DESCONTO INDICAÇÃO POR COLABORADOR - CV (-)",
            "DESCONTO INGRESSANTE (Prior. 100) (-)",
            "DESCONTO INGRESSANTE (PRIOR. 98) (-)",
            "DESCONTO INGRESSANTE PÓS (-)",
            "DESCONTO LEP (-)",
            "DESCONTO PARCELA FIXA (-)",
            "DESCONTO PÓS-GRADUAÇÃO/MEST/DOUT (-)",
            "DESCONTO PROFESSOR REDE PÚBLICA/PRIVADA- CV (-)",
            "DESCONTO PROMOCIONAL PÓS-CV (-)",
            "DESCONTO QUERO BOLSA - CV (-)",
            "DESCONTO QUERO BOLSA (-)",
            "DESCONTO REGULAMENTO FIES (-)",
            "DESCONTO RETURNING (-)",
            "DESCONTO VAGAS REMANESCENTES (-)",
            "DEVOLUÇÃO DE EXTENSÃO (-)",
            "Devolução FIES (+)",
            "EMPRESA CONVENIADA - CV (-)",
            "EMPRESA CONVENIADA - CV (ESTORNO) (+)",
            "EMPRESA CONVENIADA BATALHAO BM (-)",
            "EMPRESA CONVENIADA (-)",
            "EMPRESA CONVENIADA HOSPITAIS (-)",
            "ESTORNO BOLSA CAPES (+)",
            "ESTORNO BOLSA PROUNI (+)",
            "ESTORNO BOLSA UNIPOA (+)",
            "ESTORNO CREDIES (+)",
            "ESTORNO DE BOLSA INDEVIDO (49) (-)",
            "ESTORNO DE BOLSA INDEVIDO (99) (-)",
            "ESTORNO DE DESCONTO INDEVIDO (49) (+)",
            "ESTORNO DE DESCONTO INDEVIDO (99) (+)",
            "ESTORNO DEDUÇÃO JUDICIAL (+)",
            "ESTORNO FIES (+)",
            "FIES (-)",
            "FIES / SOB JUDICE (-)",
            "FIES CONTRATADO (-)",
            "FIES CONTRATADO / ADITADO A MAIOR (49) (-)",
            "FIES CONTRATADO / ADITADO A MAIOR (99) (-)",
            "Financiamento PIP (-)",
            "Financiamento PIP (Estorno) (+)",
            "Financiamento Uniritter (-)",
            "Financiamento Uniritter (Estorno) (+)",
            "INCENTIVO - EVENTOS - CV (-)",
            "INCENTIVO - EVENTOS (-)",
            "INCENTIVO CURSO (-)",
            "INCENTIVO FAMILIA - CV (-)",
            "INCENTIVO FAMILIA - CV (ESTORNO) (+)",
            "INCENTIVO FAMILIA (-)",
            "INCENTIVO REINGRESSO (-)",
            "INCENTIVO ZONA SUL (-)",
            "INCENTIVO ZONA SUL MAIS CRÉDITOS (-)",
            "INTERCAMBIO - FORA DA REDE LAUREATE (-)",
            "INTERCAMBIO REDE LAUREATE (-)",
            "JUROS PRAVALER (-)",
            "MULTA CONTRATUAL (+)",
            "PROUNI/SOB JUDICE (-)",
            "Quero Educação (-)",
            "Quero Educação (Estorno) (+)",
            "RESPONSÁVEL FINANCEIRO PJ (-)",
            "REVERSÃO DE DEVOLUÇÃO (99) (+)",
            "TAXAS PRAVALER (-)",
            "VALOR PAGO A MAIOR (+)"});
            this.cbFiesSiga.Location = new System.Drawing.Point(15, 32);
            this.cbFiesSiga.Name = "cbFiesSiga";
            this.cbFiesSiga.Size = new System.Drawing.Size(528, 25);
            this.cbFiesSiga.TabIndex = 59;
            // 
            // panelTodosMesesDisponiveis
            // 
            this.panelTodosMesesDisponiveis.Controls.Add(this.checkBoxTodosMeses);
            this.panelTodosMesesDisponiveis.Controls.Add(this.labelTodosMeses);
            this.panelTodosMesesDisponiveis.Location = new System.Drawing.Point(3, 893);
            this.panelTodosMesesDisponiveis.Name = "panelTodosMesesDisponiveis";
            this.panelTodosMesesDisponiveis.Size = new System.Drawing.Size(573, 61);
            this.panelTodosMesesDisponiveis.TabIndex = 71;
            this.panelTodosMesesDisponiveis.Tag = "";
            this.panelTodosMesesDisponiveis.Visible = false;
            // 
            // checkBoxTodosMeses
            // 
            this.checkBoxTodosMeses.Location = new System.Drawing.Point(17, 34);
            this.checkBoxTodosMeses.Name = "checkBoxTodosMeses";
            this.checkBoxTodosMeses.Size = new System.Drawing.Size(231, 15);
            this.checkBoxTodosMeses.TabIndex = 61;
            this.checkBoxTodosMeses.UseSelectable = true;
            // 
            // labelTodosMeses
            // 
            this.labelTodosMeses.AutoSize = true;
            this.labelTodosMeses.Location = new System.Drawing.Point(13, 12);
            this.labelTodosMeses.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTodosMeses.Name = "labelTodosMeses";
            this.labelTodosMeses.Size = new System.Drawing.Size(248, 19);
            this.labelTodosMeses.TabIndex = 60;
            this.labelTodosMeses.Text = "BAIXAR TODOS OS MESES DISPONÍVEIS";
            // 
            // labelDay
            // 
            this.labelDay.AutoSize = true;
            this.labelDay.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDay.ForeColor = System.Drawing.Color.White;
            this.labelDay.Location = new System.Drawing.Point(44, 370);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(367, 22);
            this.labelDay.TabIndex = 31;
            this.labelDay.Text = "Quarta-feira, 11 de novembro de 2022";
            // 
            // btnIniciar
            // 
            this.btnIniciar.BackColor = System.Drawing.Color.White;
            this.btnIniciar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIniciar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciar.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIniciar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnIniciar.Location = new System.Drawing.Point(45, 540);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(216, 29);
            this.btnIniciar.TabIndex = 61;
            this.btnIniciar.Tag = "ADITAMENTO BAIXAR DRM";
            this.btnIniciar.Text = "Executar";
            this.btnIniciar.UseVisualStyleBackColor = false;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // tooltip
            // 
            this.tooltip.Style = MetroFramework.MetroColorStyle.Blue;
            this.tooltip.StyleManager = null;
            this.tooltip.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // panelTipoValor
            // 
            this.panelTipoValor.Controls.Add(this.metroLabel2);
            this.panelTipoValor.Controls.Add(this.cbTipoValor);
            this.panelTipoValor.Location = new System.Drawing.Point(3, 960);
            this.panelTipoValor.Name = "panelTipoValor";
            this.panelTipoValor.Size = new System.Drawing.Size(573, 61);
            this.panelTipoValor.TabIndex = 71;
            this.panelTipoValor.Tag = "";
            this.panelTipoValor.Visible = false;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(13, 12);
            this.metroLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(82, 19);
            this.metroLabel2.TabIndex = 60;
            this.metroLabel2.Text = "TIPO VALOR";
            // 
            // cbTipoValor
            // 
            this.cbTipoValor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoValor.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoValor.FormattingEnabled = true;
            this.cbTipoValor.Items.AddRange(new object[] {
            "Percentual",
            "Valor"});
            this.cbTipoValor.Location = new System.Drawing.Point(15, 32);
            this.cbTipoValor.Name = "cbTipoValor";
            this.cbTipoValor.Size = new System.Drawing.Size(528, 25);
            this.cbTipoValor.TabIndex = 59;
            // 
            // FormDefault
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Controls.Add(this.panelCadastro);
            this.Name = "FormDefault";
            this.Text = "FormDefault";
            this.panelCadastro.ResumeLayout(false);
            this.panelCadastro.PerformLayout();
            this.panellStatus.ResumeLayout(false);
            this.panellStatus.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelCabecalho.ResumeLayout(false);
            this.panelCabecalho.PerformLayout();
            this.panelCPF.ResumeLayout(false);
            this.panelCPF.PerformLayout();
            this.panelImportar.ResumeLayout(false);
            this.panelIES.ResumeLayout(false);
            this.panelIES.PerformLayout();
            this.panelCampus.ResumeLayout(false);
            this.panelCampus.PerformLayout();
            this.panelSemestre.ResumeLayout(false);
            this.panelSemestre.PerformLayout();
            this.panelDataInicioEFim.ResumeLayout(false);
            this.panelDataInicioEFim.PerformLayout();
            this.panelAnoEMes.ResumeLayout(false);
            this.panelAnoEMes.PerformLayout();
            this.panelSituacao.ResumeLayout(false);
            this.panelSituacao.PerformLayout();
            this.panelIESRepasse.ResumeLayout(false);
            this.panelIESRepasse.PerformLayout();
            this.panelFiesSiga.ResumeLayout(false);
            this.panelFiesSiga.PerformLayout();
            this.panelTodosMesesDisponiveis.ResumeLayout(false);
            this.panelTodosMesesDisponiveis.PerformLayout();
            this.panelTipoValor.ResumeLayout(false);
            this.panelTipoValor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCadastro;
        private MetroFramework.Controls.MetroLabel labelFiesSiga;
        private System.Windows.Forms.ComboBox cbFiesSiga;
        private MetroFramework.Controls.MetroLabel labelDataInicio;
        private System.Windows.Forms.ComboBox cbSituacao;
        private System.Windows.Forms.ComboBox cbAno;
        private MetroFramework.Controls.MetroLabel labelIESRepasse;
        private System.Windows.Forms.ComboBox cbMes;
        private System.Windows.Forms.ComboBox cbIESRepasse;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
        private MetroFramework.Controls.MetroLabel labelSituacao;
        private MetroFramework.Controls.MetroLabel labelDataFim;
        private MetroFramework.Controls.MetroLabel labelAno;
        private MetroFramework.Controls.MetroLabel labelMes;
        private MetroFramework.Controls.MetroLabel labelAvisoIES;
        private MetroFramework.Controls.MetroLabel labelCPFCaracteres;
        private MetroFramework.Controls.MetroLabel metroLabel17;
        private MetroFramework.Controls.MetroLabel lblExecucao;
        private System.Windows.Forms.Label labelDay;
        private MetroFramework.Controls.MetroLabel labelCPF;
        private MetroFramework.Controls.MetroLabel labelCampus;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel labelIES;
        private System.Windows.Forms.ComboBox cbSemestre;
        private System.Windows.Forms.ComboBox cbCampus;
        private System.Windows.Forms.ComboBox cbIES;
        private System.Windows.Forms.Panel panelIES;
        private System.Windows.Forms.Panel panelCampus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panelCabecalho;
        private System.Windows.Forms.Panel panelCPF;
        private System.Windows.Forms.Panel panelSemestre;
        private System.Windows.Forms.Panel panelDataInicioEFim;
        private System.Windows.Forms.Panel panelAnoEMes;
        private System.Windows.Forms.Panel panelSituacao;
        private System.Windows.Forms.Panel panelIESRepasse;
        private System.Windows.Forms.Panel panelFiesSiga;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Panel panelImportar;
        private System.Windows.Forms.Button btnImportar;
        private MetroFramework.Controls.MetroLabel lblAlunosImportados;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.MaskedTextBox txtCPF;
        public CircularProgressBar.CircularProgressBar circularProgressBar1;
        public System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.WebBrowser wbHelp;
        private MetroFramework.Components.MetroToolTip tooltip;
        private System.Windows.Forms.Panel panellStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelTodosMesesDisponiveis;
        private MetroFramework.Controls.MetroCheckBox checkBoxTodosMeses;
        private MetroFramework.Controls.MetroLabel labelTodosMeses;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Panel panelTipoValor;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.ComboBox cbTipoValor;
    }
}