using Microsoft.Extensions.DependencyInjection;
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
    public partial class FormLogin : Form
    {
        private readonly AdminService _adminService;
        public FormLogin(AdminService adminService)
        {
            InitializeComponent();
            _adminService = adminService;
        }

        private async void buttonAuth_Click(object sender, EventArgs e)
        {
            string username = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            var admin = await _adminService.AuthenticateAsync(username, password);

            if (admin == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            // Авторизация успешна
            MessageBox.Show("Добро пожаловать!");
            var documentService = Program.ServiceProvider!.GetRequiredService<DocumentService>();
            

            var panel = new FormAdmin(admin, documentService, _adminService);
            panel.Show();
            this.Hide();
        }
    }
}
