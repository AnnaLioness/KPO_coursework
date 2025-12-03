namespace DocumetForms
{
    partial class FormMain
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
            dataGridView = new DataGridView();
            textBoxSearch = new TextBox();
            buttonAuth = new Button();
            buttonRegister = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(12, 94);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(1054, 463);
            dataGridView.TabIndex = 0;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(12, 33);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(570, 27);
            textBoxSearch.TabIndex = 1;
            // 
            // buttonAuth
            // 
            buttonAuth.Location = new Point(804, 33);
            buttonAuth.Name = "buttonAuth";
            buttonAuth.Size = new Size(109, 29);
            buttonAuth.TabIndex = 2;
            buttonAuth.Text = "Авторизация";
            buttonAuth.UseVisualStyleBackColor = true;
            buttonAuth.Click += buttonAuth_Click;
            // 
            // buttonRegister
            // 
            buttonRegister.Location = new Point(933, 33);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(106, 29);
            buttonRegister.TabIndex = 3;
            buttonRegister.Text = "Регистрация";
            buttonRegister.UseVisualStyleBackColor = true;
            buttonRegister.Click += buttonRegister_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1078, 569);
            Controls.Add(buttonRegister);
            Controls.Add(buttonAuth);
            Controls.Add(textBoxSearch);
            Controls.Add(dataGridView);
            Name = "FormMain";
            Text = "FormMain";
            Load += FormMain_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView;
        private TextBox textBoxSearch;
        private Button buttonAuth;
        private Button buttonRegister;
    }
}