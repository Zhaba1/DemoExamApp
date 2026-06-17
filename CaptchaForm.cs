using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DemoExamApp
{
    public partial class CaptchaForm : Form
    {
        private readonly User _currentUser;
        private Database _db;
        private readonly List<PictureBox> _zones = new List<PictureBox>();
        private readonly List<PictureBox> _pieces = new List<PictureBox>();
        private readonly Random _random = new Random();
        private int _attemptsRemaining = 3;

        // Конструктор для дизайнера Visual Studio
        public CaptchaForm()
        {
            InitializeComponent();
            InitializeZones();
        }

        public CaptchaForm(User user)
        {
            _currentUser = user;
            InitializeComponent();
            InitializeZones();
            _db = new Database();
            InitializeCaptcha();
        }

        private void InitializeZones()
        {
            _zones.Add(zone1);
            _zones.Add(zone2);
            _zones.Add(zone3);
            _zones.Add(zone4);

            // Зоны — только цели, не перетаскиваются
            foreach (var zone in _zones)
            {
                zone.AllowDrop = true;
                zone.DragEnter += Zone_DragEnter;
                zone.DragDrop += Zone_DragDrop;
            }
        }

        /// <summary>
        /// Загружает 4 готовых куска капчи из папки Images.
        /// 1.png — верхняя левая, 2.png — верхняя правая,
        /// 3.png — нижняя левая, 4.png — нижняя правая.
        /// </summary>
        private List<CaptchaPiece> LoadCaptchaPieces()
        {
            string imagesPath = Path.Combine(Application.StartupPath, "Images");
            var pieces = new List<CaptchaPiece>();

            for (int i = 0; i < 4; i++)
            {
                string fileName = $"{i + 1}.png";
                string filePath = Path.Combine(imagesPath, fileName);

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Не найден файл капчи: {filePath}");

                pieces.Add(new CaptchaPiece
                {
                    CorrectIndex = i,
                    Image = new Bitmap(filePath)
                });
            }

            return pieces;
        }

        private void InitializeCaptcha()
        {
            // Удаляем старые кусочки, если есть
            foreach (var piece in _pieces)
            {
                this.Controls.Remove(piece);
                piece.Dispose();
            }
            _pieces.Clear();

            // Очищаем зоны
            foreach (var zone in _zones)
            {
                zone.Image = null;
                zone.Tag = null;
            }

            var pieces = LoadCaptchaPieces();

            // Перемешиваем кусочки
            var shuffled = pieces.OrderBy(x => _random.Next()).ToList();

            // Создаём перетаскиваемые кусочки в случайных местах
            for (int i = 0; i < shuffled.Count; i++)
            {
                var pieceBox = new PictureBox
                {
                    Size = new Size(120, 90),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    Image = shuffled[i].Image,
                    Tag = shuffled[i],
                    Location = GetRandomPiecePosition()
                };

                pieceBox.MouseDown += Piece_MouseDown;
                this.Controls.Add(pieceBox);
                _pieces.Add(pieceBox);
            }

            UpdateAttemptsLabel();
        }

        /// <summary>
        /// Возвращает случайную позицию для кусочка капчи.
        /// Кусочки размещаются справа от зон или ниже них.
        /// </summary>
        private Point GetRandomPiecePosition()
        {
            // Область для случайного размещения: справа от зон
            int minX = 320;
            int maxX = this.ClientSize.Width - 130;
            int minY = 30;
            int maxY = 220;

            int x = _random.Next(minX, maxX);
            int y = _random.Next(minY, maxY);

            return new Point(x, y);
        }

        private void UpdateAttemptsLabel()
        {
            lblAttempts.Text = $"Осталось попыток: {_attemptsRemaining}";
        }

        private void Piece_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var piece = sender as PictureBox;
                if (piece != null)
                    piece.DoDragDrop(piece, DragDropEffects.Move);
            }
        }

        private void Zone_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Zone_DragDrop(object sender, DragEventArgs e)
        {
            var target = sender as PictureBox;
            var source = e.Data.GetData(typeof(PictureBox)) as PictureBox;

            if (target == null || source == null || !_zones.Contains(target)) return;

            // Переносим изображение и тег из кусочка в зону
            target.Image = source.Image;
            target.Tag = source.Tag;

            // Удаляем кусочек с формы
            this.Controls.Remove(source);
            source.Dispose();
            _pieces.Remove(source);

            target.Refresh();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            bool isCorrect = true;

            for (int i = 0; i < _zones.Count; i++)
            {
                var piece = _zones[i].Tag as CaptchaPiece;
                if (piece == null || piece.CorrectIndex != i)
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                MessageBox.Show("Капча успешно пройдена!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                var main = new MainForm(_currentUser);
                main.Show();
                this.Close();
            }
            else
            {
                _attemptsRemaining--;
                UpdateAttemptsLabel();

                if (_attemptsRemaining > 0)
                {
                    MessageBox.Show(
                        $"Капча собрана неправильно. Осталось {_attemptsRemaining} попыток.",
                        "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    InitializeCaptcha();
                }
                else
                {
                    try
                    {
                        _db.BlockUser(_currentUser.Id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка блокировки: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show(
                        "Вы были заблокированы. Для разблокировки обратитесь к администратору.",
                        "Блокировка",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    Application.Exit();
                }
            }
        }

        private void CaptchaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Если форма закрыта без успешной капчи, закрываем приложение
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }
    }

    /// <summary>
    /// Кусочек капчи с информацией о правильном положении.
    /// </summary>
    public class CaptchaPiece
    {
        public int CorrectIndex { get; set; }
        public Bitmap Image { get; set; }
    }
}
