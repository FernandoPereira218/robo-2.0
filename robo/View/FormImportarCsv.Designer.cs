
namespace robo.View
{
    partial class FormImportarCsv
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelExcel = new System.Windows.Forms.Panel();
            this.btnMarcarNaoFeito = new System.Windows.Forms.Button();
            this.tbBarraStatus = new MetroFramework.Controls.MetroTextBox();
            this.barraProgressoImportacao = new MetroFramework.Controls.MetroProgressBar();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.button3 = new System.Windows.Forms.Button();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.txtExcel = new System.Windows.Forms.TextBox();
            this.dgvAlunos = new System.Windows.Forms.DataGridView();
            this.lblNenhumAluno = new System.Windows.Forms.Label();
            this.panelExcel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos)).BeginInit();
            this.SuspendLayout();
            // 
            // panelExcel
            // 
            this.panelExcel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelExcel.BackColor = System.Drawing.Color.White;
            this.panelExcel.Controls.Add(this.btnMarcarNaoFeito);
            this.panelExcel.Controls.Add(this.tbBarraStatus);
            this.panelExcel.Controls.Add(this.barraProgressoImportacao);
            this.panelExcel.Controls.Add(this.metroLabel10);
            this.panelExcel.Controls.Add(this.button3);
            this.panelExcel.Controls.Add(this.btnExportarExcel);
            this.panelExcel.Controls.Add(this.btnSelectPath);
            this.panelExcel.Controls.Add(this.txtExcel);
            this.panelExcel.Controls.Add(this.dgvAlunos);
            this.panelExcel.Controls.Add(this.lblNenhumAluno);
            this.panelExcel.Location = new System.Drawing.Point(0, 0);
            this.panelExcel.Name = "panelExcel";
            this.panelExcel.Size = new System.Drawing.Size(1292, 700);
            this.panelExcel.TabIndex = 1;
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
            this.btnMarcarNaoFeito.Location = new System.Drawing.Point(835, 4);
            this.btnMarcarNaoFeito.Name = "btnMarcarNaoFeito";
            this.btnMarcarNaoFeito.Size = new System.Drawing.Size(148, 22);
            this.btnMarcarNaoFeito.TabIndex = 10;
            this.btnMarcarNaoFeito.Text = "Marcar não feito";
            this.btnMarcarNaoFeito.UseVisualStyleBackColor = false;
            // 
            // tbBarraStatus
            // 
            // 
            // 
            // 
            this.tbBarraStatus.CustomButton.Image = null;
            this.tbBarraStatus.CustomButton.Location = new System.Drawing.Point(320, 2);
            this.tbBarraStatus.CustomButton.Margin = new System.Windows.Forms.Padding(2);
            this.tbBarraStatus.CustomButton.Name = "";
            this.tbBarraStatus.CustomButton.Size = new System.Drawing.Size(19, 19);
            this.tbBarraStatus.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tbBarraStatus.CustomButton.TabIndex = 1;
            this.tbBarraStatus.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbBarraStatus.CustomButton.UseSelectable = true;
            this.tbBarraStatus.CustomButton.Visible = false;
            this.tbBarraStatus.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.tbBarraStatus.ForeColor = System.Drawing.Color.Black;
            this.tbBarraStatus.Lines = new string[] {
        "metroTextBox1"};
            this.tbBarraStatus.Location = new System.Drawing.Point(391, 18);
            this.tbBarraStatus.Margin = new System.Windows.Forms.Padding(2);
            this.tbBarraStatus.MaxLength = 32767;
            this.tbBarraStatus.Name = "tbBarraStatus";
            this.tbBarraStatus.PasswordChar = '\0';
            this.tbBarraStatus.ReadOnly = true;
            this.tbBarraStatus.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbBarraStatus.SelectedText = "";
            this.tbBarraStatus.SelectionLength = 0;
            this.tbBarraStatus.SelectionStart = 0;
            this.tbBarraStatus.ShortcutsEnabled = true;
            this.tbBarraStatus.Size = new System.Drawing.Size(342, 24);
            this.tbBarraStatus.Style = MetroFramework.MetroColorStyle.Red;
            this.tbBarraStatus.TabIndex = 9;
            this.tbBarraStatus.Text = "metroTextBox1";
            this.tbBarraStatus.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tbBarraStatus.UseSelectable = true;
            this.tbBarraStatus.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tbBarraStatus.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // barraProgressoImportacao
            // 
            this.barraProgressoImportacao.Location = new System.Drawing.Point(390, 18);
            this.barraProgressoImportacao.Margin = new System.Windows.Forms.Padding(2);
            this.barraProgressoImportacao.Name = "barraProgressoImportacao";
            this.barraProgressoImportacao.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.barraProgressoImportacao.Size = new System.Drawing.Size(343, 23);
            this.barraProgressoImportacao.Style = MetroFramework.MetroColorStyle.Green;
            this.barraProgressoImportacao.TabIndex = 7;
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.metroLabel10.Location = new System.Drawing.Point(30, 20);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(61, 19);
            this.metroLabel10.Style = MetroFramework.MetroColorStyle.White;
            this.metroLabel10.TabIndex = 6;
            this.metroLabel10.Text = "Diretório";
            this.metroLabel10.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.button3.Location = new System.Drawing.Point(1128, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(148, 22);
            this.button3.TabIndex = 5;
            this.button3.Text = "Exportar Informações";
            this.button3.UseVisualStyleBackColor = false;
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
            this.btnExportarExcel.Location = new System.Drawing.Point(980, 4);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(148, 22);
            this.btnExportarExcel.TabIndex = 5;
            this.btnExportarExcel.Text = "Exportar Excel";
            this.btnExportarExcel.UseVisualStyleBackColor = false;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.BackColor = System.Drawing.Color.Silver;
            this.btnSelectPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectPath.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSelectPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectPath.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.btnSelectPath.Location = new System.Drawing.Point(264, 19);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(121, 22);
            this.btnSelectPath.TabIndex = 2;
            this.btnSelectPath.Text = "Localizar Arquivo";
            this.btnSelectPath.UseVisualStyleBackColor = false;
            // 
            // txtExcel
            // 
            this.txtExcel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExcel.Enabled = false;
            this.txtExcel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcel.Location = new System.Drawing.Point(97, 19);
            this.txtExcel.Name = "txtExcel";
            this.txtExcel.Size = new System.Drawing.Size(161, 21);
            this.txtExcel.TabIndex = 1;
            // 
            // dgvAlunos
            // 
            this.dgvAlunos.AllowUserToAddRows = false;
            this.dgvAlunos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAlunos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAlunos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvAlunos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAlunos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAlunos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlunos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlunos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAlunos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlunos.Location = new System.Drawing.Point(-7, 46);
            this.dgvAlunos.Name = "dgvAlunos";
            this.dgvAlunos.ReadOnly = true;
            this.dgvAlunos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvAlunos.RowHeadersVisible = false;
            this.dgvAlunos.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAlunos.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAlunos.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAlunos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlunos.Size = new System.Drawing.Size(1302, 665);
            this.dgvAlunos.TabIndex = 0;
            // 
            // lblNenhumAluno
            // 
            this.lblNenhumAluno.AutoSize = true;
            this.lblNenhumAluno.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNenhumAluno.Location = new System.Drawing.Point(6, 514);
            this.lblNenhumAluno.Name = "lblNenhumAluno";
            this.lblNenhumAluno.Size = new System.Drawing.Size(252, 21);
            this.lblNenhumAluno.TabIndex = 0;
            this.lblNenhumAluno.Text = "Nenhum aluno no banco de dados.";
            // 
            // PanelImportarCsv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1290, 693);
            this.Controls.Add(this.panelExcel);
            this.Name = "PanelImportarCsv";
            this.Text = "PanelImportarCsv";
            this.panelExcel.ResumeLayout(false);
            this.panelExcel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelExcel;
        private System.Windows.Forms.Button btnMarcarNaoFeito;
        private MetroFramework.Controls.MetroTextBox tbBarraStatus;
        private MetroFramework.Controls.MetroProgressBar barraProgressoImportacao;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.TextBox txtExcel;
        private System.Windows.Forms.DataGridView dgvAlunos;
        private System.Windows.Forms.Label lblNenhumAluno;
    }
}