using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoExamApp
{
    public partial class ValidationForm : Form
    {
        private HttpClient _httpClient;

        public ValidationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При загрузке формы подключаемся к API эмулятора.
        /// </summary>
        private void ValidationForm_Load(object sender, EventArgs e)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:4444/TransferSimulator/")
            };
        }

        /// <summary>
        /// Кнопка "Получить данные" — получает значение от эмулятора и сразу проверяет его.
        /// По умолчанию используется ФИО. Чтобы переключиться на другой тип данных,
        /// раскомментируй нужный блок ниже и закомментируй текущий.
        /// </summary>
        private async void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                btnGetData.Enabled = false;

                // ============================================================
                // БЛОК 1: ФИО (fullName)
                // ============================================================
                //string value = await GetValueAsync("fullName");
                //lblFullName.Text = value;
                //string result = ValidateFullName(value);
                //lblResult.Text = $"Результат: {result}";

                // ============================================================
                // БЛОК 2: СНИЛС (snils)
                // ============================================================
                 string value = await GetValueAsync("snils");
                 lblFullName.Text = value;
                 string result = ValidateSnils(value);
                 lblResult.Text = $"Результат: {result}";

                // ============================================================
                // БЛОК 3: ИНН (inn)
                // ============================================================
                // string value = await GetValueAsync("inn");
                // lblFullName.Text = value;
                // string result = ValidateInn(value);
                // lblResult.Text = $"Результат: {result}";

                // ============================================================
                // БЛОК 4: Email (email)
                // ============================================================
                // string value = await GetValueAsync("email");
                // lblFullName.Text = value;
                // string result = ValidateEmail(value);
                // lblResult.Text = $"Результат: {result}";

                // ============================================================
                // БЛОК 5: Удостоверение личности (identityCard)
                // ============================================================
                // string value = await GetValueAsync("identityCard");
                // lblFullName.Text = value;
                // string result = ValidateIdentityCard(value);
                // lblResult.Text = $"Результат: {result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения данных:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGetData.Enabled = true;
            }
        }

        /// <summary>
        /// Выполняет GET-запрос к указанному endpoint эмулятора и возвращает значение.
        /// </summary>
        private async Task<string> GetValueAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Message>();
            return result?.value ?? string.Empty;
        }

        // ============================================================
        // ВАЛИДАЦИЯ ФИО
        // ============================================================
        private string ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "ФИО не заполнено";

            if (Regex.IsMatch(fullName, @"[^а-яА-Яa-zA-Z\s-]"))
                return "ФИО содержит запрещенные символы";

            return "ФИО не содержит запрещенных символов";
        }

        // ============================================================
        // ВАЛИДАЦИЯ СНИЛС
        // ============================================================
        private string ValidateSnils(string snils)
        {
            if (string.IsNullOrWhiteSpace(snils))
                return "СНИЛС не заполнен";

            // СНИЛС: 11 цифр, допустимы разделители "-" и " "
            string digitsOnly = Regex.Replace(snils, @"[^0-9]", "");
            if (digitsOnly.Length != 11)
                return "СНИЛС должен содержать 11 цифр";

            return "СНИЛС заполнен корректно";
        }

        // ============================================================
        // ВАЛИДАЦИЯ ИНН
        // ============================================================
        private string ValidateInn(string inn)
        {
            if (string.IsNullOrWhiteSpace(inn))
                return "ИНН не заполнен";

            // ИНН физлица — 12 цифр, организации — 10 цифр
            string digitsOnly = Regex.Replace(inn, @"[^0-9]", "");
            if (digitsOnly.Length != 10 && digitsOnly.Length != 12)
                return "ИНН должен содержать 10 или 12 цифр";

            return "ИНН заполнен корректно";
        }

        // ============================================================
        // ВАЛИДАЦИЯ EMAIL
        // ============================================================
        private string ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return "Email не заполнен";

            // Простая проверка формата email
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, pattern))
                return "Email имеет некорректный формат";

            return "Email заполнен корректно";
        }

        // ============================================================
        // ВАЛИДАЦИЯ УДОСТОВЕРЕНИЯ ЛИЧНОСТИ
        // ============================================================
        private string ValidateIdentityCard(string identityCard)
        {
            if (string.IsNullOrWhiteSpace(identityCard))
                return "Удостоверение личности не заполнено";

            // Паспорт РФ: серия 4 цифры + номер 6 цифр (допустимы пробелы/тире)
            string digitsOnly = Regex.Replace(identityCard, @"[^0-9]", "");
            if (digitsOnly.Length != 10)
                return "Удостоверение личности должно содержать 10 цифр";

            return "Удостоверение личности заполнено корректно";
        }
    }

    public class Message
    {
        public string value { get; set; }
    }
}
