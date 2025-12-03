using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumetForms.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Models.Models;
using Services.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumetForms
{
    public partial class FormAdmin : Form
    {
        private readonly DocumentService _documentService;
        private readonly AdminService _adminService;

        private readonly AdminModel _currentAdmin;
        private string? _loadedDocumentContent; // Текст документа из файла
        public FormAdmin(AdminModel admin, DocumentService docService, AdminService adminService)
        {
            InitializeComponent();
            _documentService = docService;
            _adminService = adminService;
            _currentAdmin = admin;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Выберите документ";
            dialog.Filter = "Word (*.docx)|*.docx|Текст (*.txt)|*.txt";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                string ext = Path.GetExtension(path).ToLower();

                try
                {
                    if (ext == ".txt")
                    {
                        _loadedDocumentContent = File.ReadAllText(path);
                    }
                    else if (ext == ".docx")
                    {
                        _loadedDocumentContent = WordHelper.ReadWordDocument(path);
                    }
                    else
                    {
                        MessageBox.Show("Поддерживаются только .txt и .docx файлы.");
                        return;
                    }

                    MessageBox.Show("Документ загружен успешно!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке: " + ex.Message);
                }
            }
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            if (_loadedDocumentContent == null)
            {
                MessageBox.Show("Сначала загрузите документ!");
                return;
            }


            var doc = new DocumentModel
            {
                Title = textBoxTitle.Text,
                Author = textBoxAuthor.Text,
                Year = textBoxYear.Text,
                CloudLink = textBoxLink.Text,
                Content = _loadedDocumentContent,
                AdminId = _currentAdmin.Id
            };

            await _documentService.AddAsync(doc);
            LoadDocumentsAsync();
            MessageBox.Show("Документ создан.");
        }

        private async void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewDocs.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите документ.");
                return;
            }

            int id = (int)dataGridViewDocs.SelectedRows[0].Cells["Id"].Value;

            var doc = await _documentService.GetByIdAsync(id);

            if (doc == null)
            {
                MessageBox.Show("Документ не найден.");
                return;
            }

            // редактируем
            doc.Title = textBoxTitle.Text;
            doc.Author = textBoxAuthor.Text;
            doc.CloudLink = textBoxLink.Text;
            doc.Year = textBoxYear.Text;

            if (_loadedDocumentContent != null)
                doc.Content = _loadedDocumentContent;

            await _documentService.UpdateAsync(doc);
            LoadDocumentsAsync();
            MessageBox.Show("Документ обновлён.");
        }

        private async void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewDocs.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите документ.");
                return;
            }

            int id = (int)dataGridViewDocs.SelectedRows[0].Cells["Id"].Value;
            var doc = await _documentService.GetByIdAsync(id);

            await _documentService.DeleteAsync(doc);
            LoadDocumentsAsync();

            MessageBox.Show("Документ удалён.");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            var main = Program.ServiceProvider!.GetRequiredService<FormMain>();
            main.Show();
        }

        private async void buttonDelAcc_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Вы действительно хотите удалить аккаунт?\nВсе ваши документы будут потеряны.",
                "Подтверждение",
                MessageBoxButtons.YesNo);

            if (confirm == DialogResult.No)
                return;

            await _adminService.DeleteAdminAsync(_currentAdmin.Id);

            MessageBox.Show("Аккаунт удалён.");

            this.Hide();
            var main = Program.ServiceProvider!.GetRequiredService<FormMain>();
            main.Show();
        }
        private async Task LoadDocumentsAsync()
        {
            var docs = await _documentService.GetDocumentsByAdminAsync(_currentAdmin.Id);
            dataGridViewDocs.DataSource = docs.ToList();
            ClearInputs();
        }
        private void ClearInputs()
        {
            textBoxTitle.Clear();
            textBoxAuthor.Clear();
            textBoxYear.Clear();
            textBoxLink.Clear();
        }

        private async void dataGridViewDocs_SelectionChanged(object sender, EventArgs e)
        {
            // Проверяем, есть ли выбранная строка
            if (dataGridViewDocs.CurrentRow == null) return;

            int index = dataGridViewDocs.CurrentRow.Index;

            // Проверяем, что индекс не выходит за границы строк DataGridView
            if (index < 0 || index >= dataGridViewDocs.Rows.Count) return;

            // Безопасно получаем Id документа из выбранной строки
            if (!int.TryParse(dataGridViewDocs.CurrentRow.Cells["Id"].Value?.ToString(), out int id))
                return;

            // Получаем документ по Id
            var doc = await _documentService.GetByIdAsync(id);
            if (doc == null)
            {
                MessageBox.Show("Документ не найден.");
                return;
            }

            // Заполняем текстбоксы данными документа
            textBoxTitle.Text = doc.Title;
            textBoxAuthor.Text = doc.Author;
            textBoxYear.Text = doc.Year.ToString();
            textBoxLink.Text = doc.CloudLink;
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            LoadDocumentsAsync();
        }
    }
}
