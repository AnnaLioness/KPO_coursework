namespace DocumetForms
{
    partial class FormAdmin
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
            dataGridViewDocs = new DataGridView();
            textBoxTitle = new TextBox();
            textBoxAuthor = new TextBox();
            textBoxLink = new TextBox();
            textBoxYear = new TextBox();
            buttonCreate = new Button();
            buttonUpd = new Button();
            buttonDel = new Button();
            buttonExit = new Button();
            buttonDelAcc = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            buttonLoad = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDocs).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewDocs
            // 
            dataGridViewDocs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDocs.Location = new Point(12, 12);
            dataGridViewDocs.Name = "dataGridViewDocs";
            dataGridViewDocs.RowHeadersWidth = 51;
            dataGridViewDocs.Size = new Size(666, 549);
            dataGridViewDocs.TabIndex = 0;
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new Point(684, 84);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(524, 27);
            textBoxTitle.TabIndex = 1;
            // 
            // textBoxAuthor
            // 
            textBoxAuthor.Location = new Point(684, 148);
            textBoxAuthor.Name = "textBoxAuthor";
            textBoxAuthor.Size = new Size(524, 27);
            textBoxAuthor.TabIndex = 2;
            // 
            // textBoxLink
            // 
            textBoxLink.Location = new Point(684, 291);
            textBoxLink.Name = "textBoxLink";
            textBoxLink.Size = new Size(504, 27);
            textBoxLink.TabIndex = 3;
            // 
            // textBoxYear
            // 
            textBoxYear.Location = new Point(684, 221);
            textBoxYear.Name = "textBoxYear";
            textBoxYear.Size = new Size(179, 27);
            textBoxYear.TabIndex = 4;
            // 
            // buttonCreate
            // 
            buttonCreate.Location = new Point(684, 528);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(94, 29);
            buttonCreate.TabIndex = 5;
            buttonCreate.Text = "Сохранить";
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // buttonUpd
            // 
            buttonUpd.Location = new Point(814, 528);
            buttonUpd.Name = "buttonUpd";
            buttonUpd.Size = new Size(94, 29);
            buttonUpd.TabIndex = 6;
            buttonUpd.Text = "Обновить";
            buttonUpd.UseVisualStyleBackColor = true;
            buttonUpd.Click += buttonUpd_Click;
            // 
            // buttonDel
            // 
            buttonDel.Location = new Point(946, 528);
            buttonDel.Name = "buttonDel";
            buttonDel.Size = new Size(94, 29);
            buttonDel.TabIndex = 7;
            buttonDel.Text = "Удалить";
            buttonDel.UseVisualStyleBackColor = true;
            buttonDel.Click += buttonDel_Click;
            // 
            // buttonExit
            // 
            buttonExit.Location = new Point(946, 12);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(94, 29);
            buttonExit.TabIndex = 8;
            buttonExit.Text = "Выйти";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // buttonDelAcc
            // 
            buttonDelAcc.Location = new Point(1061, 12);
            buttonDelAcc.Name = "buttonDelAcc";
            buttonDelAcc.Size = new Size(147, 29);
            buttonDelAcc.TabIndex = 9;
            buttonDelAcc.Text = "Удалить аккаунт";
            buttonDelAcc.UseVisualStyleBackColor = true;
            buttonDelAcc.Click += buttonDelAcc_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(684, 61);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 10;
            label1.Text = "Название:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(684, 125);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 11;
            label2.Text = "Автор:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(684, 198);
            label3.Name = "label3";
            label3.Size = new Size(98, 20);
            label3.TabIndex = 12;
            label3.Text = "Год издания:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(684, 268);
            label4.Name = "label4";
            label4.Size = new Size(154, 20);
            label4.TabIndex = 13;
            label4.Text = "Ссылка на оригинал:";
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(684, 344);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(94, 29);
            buttonLoad.TabIndex = 14;
            buttonLoad.Text = "Загрузить";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // FormAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1220, 573);
            Controls.Add(buttonLoad);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonDelAcc);
            Controls.Add(buttonExit);
            Controls.Add(buttonDel);
            Controls.Add(buttonUpd);
            Controls.Add(buttonCreate);
            Controls.Add(textBoxYear);
            Controls.Add(textBoxLink);
            Controls.Add(textBoxAuthor);
            Controls.Add(textBoxTitle);
            Controls.Add(dataGridViewDocs);
            Name = "FormAdmin";
            Text = "FormAdmin";
            ((System.ComponentModel.ISupportInitialize)dataGridViewDocs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewDocs;
        private TextBox textBoxTitle;
        private TextBox textBoxAuthor;
        private TextBox textBoxLink;
        private TextBox textBoxYear;
        private Button buttonCreate;
        private Button buttonUpd;
        private Button buttonDel;
        private Button buttonExit;
        private Button buttonDelAcc;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button buttonLoad;
    }
}