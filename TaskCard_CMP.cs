using System;
using System.Drawing;
using System.Windows.Forms;

namespace Inflow
{
    public partial class TaskCard_CMP : UserControl
    {
        private Panel cardPanel;
        private Label taskNameLabel;
        private Label descriptionLabel;
        private Label timeLabel;
        private FlowLayoutPanel starContainer;
        private PictureBox[] stars;
        private Button deleteButton;

        private string taskName;
        private string description;
        private string timePeriod;
        private string duration;
        private int priority;

        public event EventHandler DeleteClicked;

        public TaskCard_CMP(string name, string desc, string timeOfDay, string taskDuration, int rating)
        {
            taskName = name;
            description = desc;
            timePeriod = timeOfDay;
            duration = taskDuration;
            priority = rating;

            InitializeComponent();
            SetupCard();
            DisplayTaskInfo();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(280, 120);
            this.Margin = new Padding(5);

            cardPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5)
            };

            taskNameLabel = new Label
            {
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(5, 5),
                AutoSize = true
            };

            descriptionLabel = new Label
            {
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Location = new Point(5, 30),
                AutoSize = true,
                ForeColor = Color.Gray
            };

            timeLabel = new Label
            {
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                Location = new Point(5, 55),
                AutoSize = true,
                ForeColor = Color.DarkBlue
            };

            starContainer = new FlowLayoutPanel
            {
                Location = new Point(5, 75),
                Size = new Size(150, 25),
                FlowDirection = FlowDirection.LeftToRight
            };

            deleteButton = new Button
            {
                Text = "✗",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(245, 5),
                Size = new Size(25, 25),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Red,
                BackColor = Color.Transparent
            };
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.Click += (s, e) => DeleteClicked?.Invoke(this, EventArgs.Empty);

            stars = new PictureBox[5];
            for (int i = 0; i < 5; i++)
            {
                stars[i] = new PictureBox
                {
                    Image = Properties.Resources.Rating,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(18, 18),
                    Margin = new Padding(1)
                };
                starContainer.Controls.Add(stars[i]);
            }

            cardPanel.Controls.Add(taskNameLabel);
            cardPanel.Controls.Add(descriptionLabel);
            cardPanel.Controls.Add(timeLabel);
            cardPanel.Controls.Add(starContainer);
            cardPanel.Controls.Add(deleteButton);

            this.Controls.Add(cardPanel);
        }

        private void SetupCard()
        {
            cardPanel.BackColor = Color.FromArgb(250, 250, 250);

            // Add hover effect
            this.MouseEnter += (s, e) => cardPanel.BackColor = Color.FromArgb(240, 240, 255);
            this.MouseLeave += (s, e) => cardPanel.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void DisplayTaskInfo()
        {
            taskNameLabel.Text = taskName;
            descriptionLabel.Text = description.Length > 50 ? description.Substring(0, 47) + "..." : description;
            timeLabel.Text = $"{timePeriod} • {duration}";

            // Show stars
            for (int i = 0; i < stars.Length; i++)
            {
                if (i < priority)
                {
                    stars[i].Image = Properties.Resources.Rating;  // gold star
                    stars[i].Enabled = true;
                }
                else
                {
                    stars[i].Image = ImageHelper.CreateDimmedStar(Properties.Resources.Rating);
                    stars[i].Enabled = false;
                }
            }
        }

        public string TaskName => taskName;
        public int Priority => priority;


    }
}