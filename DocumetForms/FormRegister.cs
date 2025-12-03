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
    public partial class FormRegister : Form
    {
        private readonly AdminService _adminService;
        public FormRegister(AdminService adminService)
        {
            InitializeComponent();
            _adminService = adminService;
        }

        private async void buttonRegister_Click(object sender, EventArgs e)
        {
            string username = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }

            try
            {
                var admin = await _adminService.RegisterAsync(username, password);

                MessageBox.Show("Регистрация успешна!");

                // Переход на форму авторизации
                this.Hide();
                var login = new FormLogin(_adminService);
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
