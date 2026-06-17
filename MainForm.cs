using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DemoExamApp
{
    public partial class MainForm : Form
    {
        private readonly User _currentUser;
        private Database _db;
        private DataTable _currentDataTable;

        // Конструктор для дизайнера Visual Studio
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(User user)
        {
            _currentUser = user;
            InitializeComponent();
            SetupUIByRole();
            _db = new Database();
            LoadTables();
        }

        private void SetupUIByRole()
        {
            this.Text = $"Главное окно - {_currentUser.RoleText} ({_currentUser.Login})";

            if (!_currentUser.IsAdmin)
            {
                // Обычный пользователь только просматривает
                dataGridView.ReadOnly = true;
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                btnSave.Visible = false;
                btnValidation.Visible = false;
            }
            else
            {
                // Администратор может редактировать и открывать валидацию
                dataGridView.ReadOnly = false;
                btnAdd.Visible = true;
                btnDelete.Visible = true;
                btnSave.Visible = true;
                btnValidation.Visible = true;
            }
        }

        private void LoadTables()
        {
            try
            {
                cmbTables.Items.Clear();
                var tables = _db.GetTables();

                foreach (var table in tables)
                {
                    // Обычному пользователю не показываем таблицу Users
                    if (!_currentUser.IsAdmin && table.Equals("Users", StringComparison.OrdinalIgnoreCase))
                        continue;

                    cmbTables.Items.Add(table);
                }

                if (cmbTables.Items.Count > 0)
                    cmbTables.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки таблиц: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTableData();
        }

        private void LoadTableData()
        {
            if (cmbTables.SelectedItem == null) return;

            string tableName = cmbTables.SelectedItem.ToString();
            try
            {
                _currentDataTable = _db.GetTableData(tableName);
                dataGridView.DataSource = _currentDataTable;
                FormatBooleanColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatBooleanColumns()
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Name == "IsAdmin" || column.Name == "IsBlocked")
                {
                    if (!(column is DataGridViewCheckBoxColumn))
                    {
                        var checkBoxColumn = new DataGridViewCheckBoxColumn
                        {
                            Name = column.Name,
                            DataPropertyName = column.DataPropertyName,
                            HeaderText = column.HeaderText,
                            DisplayIndex = column.DisplayIndex,
                            TrueValue = 1,
                            FalseValue = 0,
                            ThreeState = false
                        };

                        dataGridView.Columns.Remove(column);
                        dataGridView.Columns.Insert(column.DisplayIndex, checkBoxColumn);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTableData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_currentDataTable == null) return;

            var newRow = _currentDataTable.NewRow();
            _currentDataTable.Rows.Add(newRow);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0) return;

            var result = MessageBox.Show("Удалить выбранные строки?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    if (!row.IsNewRow)
                        dataGridView.Rows.Remove(row);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_currentDataTable == null || cmbTables.SelectedItem == null) return;

            string tableName = cmbTables.SelectedItem.ToString();
            try
            {
                dataGridView.EndEdit();
                int updatedRows = _db.SaveTableData(tableName, _currentDataTable);
                MessageBox.Show($"Изменения сохранены. Затронуто строк: {updatedRows}", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTableData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnValidation_Click(object sender, EventArgs e)
        {
            using (var form = new ValidationForm())
            {
                form.ShowDialog(this);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
