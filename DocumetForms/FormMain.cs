using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
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
    public partial class FormMain : Form
    {
        private readonly DocumentService _documentService;
        public FormMain(DocumentService documentService)
        {
            InitializeComponent();
            _documentService = documentService;
            var linkColumn = new DataGridViewLinkColumn
            {
                HeaderText = "Ссылка",
                DataPropertyName = "CloudLink",
                Name = "CloudLink",
                TrackVisitedState = true,
                UseColumnTextForLinkValue = false
            };
            if (!dataGridView.Columns.Contains("CloudLink"))
                dataGridView.Columns.Add(linkColumn);

            // Подписываемся на событие клика по ссылке
            dataGridView.CellContentClick += dataGridView_CellContentClick;
            LoadDocumentsAsync();
        }

        private void buttonAuth_Click(object sender, EventArgs e)
        {
            var form = Program.ServiceProvider!.GetRequiredService<FormLogin>();
            form.ShowDialog();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            var form = Program.ServiceProvider!.GetRequiredService<FormRegister>();
            form.ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
        private async Task LoadDocumentsAsync()
        {
            var docs = await _documentService.GetAllAsync();
            dataGridView.DataSource = docs.ToList();
        }

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            var query = textBoxSearch.Text.Trim();
            var results = await _documentService.SearchAsync(query);
            dataGridView.DataSource = results;
        }

        private async void buttonCancel_Click(object sender, EventArgs e)
        {
            textBoxSearch.Text = "";
            await LoadDocumentsAsync();
        }
        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView.Columns[e.ColumnIndex].Name == "CloudLink")
            {
                var link = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (!string.IsNullOrEmpty(link))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = link,
                        UseShellExecute = true
                    });
                }
            }
        }
    }
}
