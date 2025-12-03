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
    }
}
