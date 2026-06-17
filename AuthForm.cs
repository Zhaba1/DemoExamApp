using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DemoExamApp
{
    public partial class AuthForm : Form
    {
        private Database _db;

        public AuthForm()
        {
            InitializeComponent();

            // В режиме дизайнера не подключаемся к БД
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                _db = new Database();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                User user = _db.AuthUser(login, password);

                if (user == null)
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (user.IsBlocked)
                {
                    MessageBox.Show("Вы заблокированы. Обратитесь к администратору.", "Блокировка",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (!user.IsAdmin)
                {
                    // Для обычного пользователя показываем капчу
                    var captcha = new CaptchaForm(user);
                    captcha.Show();
                    this.Hide();
                }
                else
                {
                    // Админ сразу попадает в основное приложение
                    var main = new MainForm(user);
                    main.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
