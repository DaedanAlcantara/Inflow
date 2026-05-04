![inflow](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/2.png?raw=true)
<br/>

![Header1](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/3.png?raw=true)
---
<div align = center>Welcome to InFlow! Keep it in the flow with the desktop task and performance tracking application based in native C# and Windows Forms structure that enables users to create and plan schedules for different tasks throughout the day. 
Inflow helps users to accomplish tasks -- menial or important -- with the system's time tracker and rapid task flow so you won't miss a task everyday! By incentivising users to keep completing tasks before the time limit, InFlow transforms to-do lists as an efficient, automated routine where finishing tasks can get your Flow Streak to level up.
</div>


![Header2](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/4.png?raw=true)
---
![UML](https://raw.githubusercontent.com/DaedanAlcantara/Inflow/7e59f16dc46265f3b4cea28a44860b7145a66d03/AOOP%20UML.png)


![Header3](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/5.png?raw=true)
---
### Dashboard
 InFlow boasts a **comprehensive and functional dashboard** that has everything you need in one go! The dashboard tracks the user's completed and dropped tasks, as well as your **Flow Streak** and tasks to be done. Additionaly, the dashboard has a **real-time integrated time and date display** so that users can quickly track of time during tasks.
The Dashboard's Flow Streak is the app's **incentive system** where once a user is able to finish three tasks consecutively, the system counts that as one Flow Streak. After emptying the planner, the streak is saved until the next deployment. However, if the user is to break the flow by dropping a task, the Streak drops back to zero.

### Planner
Planner lets users customize their to-do lists to further optimize and improve their performance. The InFlow Planner uses a **5-star priority rating system** to emphasize importance of each task in the moment as it rolls. **User-friendly interface** for creating tasks are included to maximize **user comfort**. Lastly, the Planner has a **smart auto-sort function** that neatly sorts your tasks according to their priority rating as defined in each task card. In turn, you don't have to worry about thinking which tasks to go first!

### Nitro Flow
Nitro Flow is the star of InFlow's task management. If users want to be in their Flow State while working -- undisturbed and in the zone -- they can use Nitro Flow which is the app's **"Focus Mode"**. 
In NitroFlow, tasks are automatically timed according to their specifications and if the user is not able to declare tasks as completed before the timer runs out, the task is automatically tagged as dropped and moves onto the next task in the list.
Furthermore, users are rewarded in Nitro Flow in that completing two tasks consecutively would gain a Flow Streak.


### Custom UI Design
Custom and personalized title bars are included in the app to further enhance the user experience. The title bar is designed to be **minimalistic and clean** to fit the overall design of the app. The title bar also includes a **customized close, minimize, and maximize buttons** that are designed to be intuitive and easy to use. The title bar also includes a **drag-and-drop feature** that allows users to easily move the app around their desktop.
Designed with a **modern and sleek aesthetic**, controls and other assets are designed to be **intuitive and user-friendly**. 

![Header4](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/6.png?raw=true)
---

Every controls present in the app are custom designed using native C# and Windows Forms structure. The app's custom controls are designed to be **intuitive and user-friendly** to enhance the user experience. The custom controls include **Rounded Panels, Rounded Text Boxes, Task Cards, User Schedule Boxes, and User Boxes**. Each control is designed to fit the overall design of the app and to enhance the user experience.
```
public class GradientPanel_CMP : Panel
{

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color ColorTop { get; set; } = Color.Blue;
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color ColorBottom { get; set; } = Color.LightBlue;

    public GradientPanel_CMP()
    {
        this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                      ControlStyles.UserPaint |
                      ControlStyles.DoubleBuffer |
                      ControlStyles.OptimizedDoubleBuffer, true);
        this.UpdateStyles();
        this.DoubleBuffered = true;
        this.ResizeRedraw = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using (LinearGradientBrush brush = new LinearGradientBrush(
            this.ClientRectangle,
            this.ColorTop,
            this.ColorBottom,
            LinearGradientMode.Vertical))
        {
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }
    }
}
```
```
public partial class TaskCard_CMP : UserControl
    {
        private Panel cardPanel;
        private Label taskNameLabel;
        private Label descriptionLabel;
        private Label timeLabel;
        private FlowLayoutPanel starContainer;
        private PictureBox[] stars;
        private PictureBox deleteButton;
        private Color taskColor;
        private string taskName;
        private string description;
        private string timePeriod;
        private string duration;
        private int priority;

        public event EventHandler DeleteClicked;

        internal TaskCard_CMP(Task_BX task)
        {
            taskName = task.Name;
            description = task.Description;
            timePeriod = (task.TimePreference == TimePreference_BX.Morning) ? "Morning" : "Afternoon";
            duration = $"{task.Duration.Hours:D2}:{task.Duration.Minutes:D2}";
            priority = task.Priority;
            taskColor = task.CardColor; 

            InitializeComponent();
            SetupCard();
            DisplayTaskInfo();
        }

     
        [Obsolete("Use TaskCard_CMP(Task_BX) constructor instead for permanent colors.")]
        public TaskCard_CMP(string name, string desc, string timeOfDay, string taskDuration, int rating)
        {
            taskName = name;
            description = desc;
            timePeriod = timeOfDay;
            duration = taskDuration;
            priority = rating;
            taskColor = GenerateRandomColor();  

            InitializeComponent();
            SetupCard();
            DisplayTaskInfo();
        }
```
The application's skeleton structure is divided into three main components: the **Dashboard**, **Planner**, and **Nitro Flow**. Each component is designed to be intuitive and user-friendly to enhance the user experience. The Dashboard is designed to be comprehensive and functional, while the Planner is designed to be customizable and efficient. Nitro Flow is designed to be a focus mode that helps users to stay in their flow state while working on tasks.

```
public partial class Dashboard_FX : Form
    {
        private UserSchedule_BX userSchedule;
        private int flowStreak;
        public Dashboard_FX(UserSchedule_BX schedule)
        {
            InitializeComponent();
            userSchedule = schedule;
            flowStreak = 0; 
            UpdateDashboard();
        }
        public void UpdateDashboard()
        {
            completedTasksLabel.Text = $"Completed Tasks: {userSchedule.CompletedTasks.Count}";
            droppedTasksLabel.Text = $"Dropped Tasks: {userSchedule.DroppedTasks.Count}";
            flowStreakLabel.Text = $"Flow Streak: {flowStreak}";
            DateTime now = DateTime.Now;
            dateTimeLabel.Text = now.ToString("MMMM dd, yyyy - HH:mm:ss");
        }
        public void IncrementFlowStreak()
        {
            flowStreak++;
            UpdateDashboard();
        }
        public void ResetFlowStreak()
        {
            flowStreak = 0;
            UpdateDashboard();
        }
    }
```
```
public partial class Planner_FX : Form
    {
        private UserSchedule_BX userSchedule;
        private Dashboard_FX dashboard;
        public Planner_FX(UserSchedule_BX schedule, Dashboard_FX dash)
        {
            InitializeComponent();
            userSchedule = schedule;
            dashboard = dash;
            RefreshTaskList();
        }
        private void RefreshTaskList()
        {
            tasksFlowPanel.Controls.Clear();
            foreach (var task in userSchedule.Tasks)
            {
                TaskCard_CMP taskCard = new TaskCard_CMP(task);
                taskCard.DeleteClicked += TaskCard_DeleteClicked;
                tasksFlowPanel.Controls.Add(taskCard);
            }
        }
        private void TaskCard_DeleteClicked(object sender, EventArgs e)
        {
            if (sender is TaskCard_CMP taskCard)
            {
                int index = tasksFlowPanel.Controls.IndexOf(taskCard);
                if (index >= 0 && index < userSchedule.Tasks.Count)
                {
                    userSchedule.Tasks.RemoveAt(index);
                    RefreshTaskList();
                    dashboard.UpdateDashboard();
                }
            }
        }
    }
```
```
public partial class Nitro_FX : Form
    {
        private UserSchedule_BX userSchedule;
        private Dashboard_FX dashboard;
        private int currentTaskIndex;
        private Timer taskTimer;
        public Nitro_FX(UserSchedule_BX schedule, Dashboard_FX dash)
        {
            InitializeComponent();
            userSchedule = schedule;
            dashboard = dash;
            currentTaskIndex = 0;
            taskTimer = new Timer();
            taskTimer.Interval = 1000; 
            taskTimer.Tick += TaskTimer_Tick;
            StartNextTask();
        }
        private void StartNextTask()
        {
            if (currentTaskIndex < userSchedule.Tasks.Count)
            {
                Task_BX currentTask = userSchedule.Tasks[currentTaskIndex];
                timeRemainingLabel.Text = $"Time Remaining: {currentTask.Duration.Hours:D2}:{currentTask.Duration.Minutes:D2}";
                taskTimer.Start();
            }
            else
            {
                MessageBox.Show("All tasks completed!");
                this.Close();
            }
        }
        private void TaskTimer_Tick(object sender, EventArgs e)
        {
            if (currentTaskIndex < userSchedule.Tasks.Count)
            {
                Task_BX currentTask = userSchedule.Tasks[currentTaskIndex];
                TimeSpan remainingTime = TimeSpan.Parse(timeRemainingLabel.Text.Replace("Time Remaining: ", ""));
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
                if (remainingTime.TotalSeconds <= 0)
                {
                    userSchedule.DroppedTasks.Add(currentTask);
                    dashboard.ResetFlowStreak();
                    currentTaskIndex++;
                    StartNextTask();
                }
                else
                {
                    timeRemainingLabel.Text = $"Time Remaining: {remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
                }
            }
        }
    }
```

Every action and navigation in the app is designed to be seamless and efficient. Program files are categorized as `FX`, `BX`, and `CMP`. `FX` files are the main forms of the app that users interact with. `BX` files are the business logic files that handle the data and operations of the app. `CMP` files are the custom controls that are used in the `FX` files to enhance the user experience. Each file is designed to be intuitive and user-friendly to enhance the overall design of the app.

![Header5](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/7.png?raw=true)
--

### Getting the Repository
To be able to run the application, please clone or download the given repository and its assets and files. Make sure there are no build errors present before kickstarting. Open the `Inflow.cs` file and run the code to launch the app.
```
📂Inflow/
├── 📂Inflow/
│   ├── 📂Assets/
│   ├── 📂bin/
│   ├── 📂Fonts/
│   ├── 📂obj/
│   ├── 📂Properties/
│   ├── 📂Resources/
│   ├── #️⃣AppState.cs
│   ├── #️⃣Dashboard_FX.cs
│   ├── #️⃣Dashboard_FX.Designer.cs
│   ├── #️⃣GettingStartedForm_FX.cs
│   ├── #️⃣GettingStartedForm_FX.Designer.cs
│   ├── #️⃣GradientPanel_CMP.cs
│   ├── #️⃣GrandmaWindow_FX.cs
│   ├── #️⃣ImageHelper.cs
│   ├── #️⃣InFlow.cs
│   ├── #️⃣InFlow.Designer.cs
│   ├── #️⃣MainWIndowMother_FX.cs
│   ├── #️⃣MotherWindowFX.cs
│   ├── #️⃣Nitro_FX.cs
│   ├── #️⃣Nitro_FX.Designer.cs
│   ├── #️⃣Planner_FX.cs
│   ├── #️⃣Program.cs
│   ├── #️⃣RoundedPanel_CMP.cs
│   ├── #️⃣RoundedTextBox_CMP.cs
│   ├── #️⃣Task_BX.cs
│   ├── #️⃣TaskCard_CMP.cs
│   ├── #️⃣User_BX.cs
│   └── #️⃣UserSchedule_BX.cs
├── 📄LICENSE
└── 📄README.md
```

### Installing Assets
The InFlow app utlize on custom files as its graphic resources and assets. Some assets such as images are already embedded onto the resource (`.resx`) file of the application. However, fonts used in the application are not native to the default system of a computer. Therefore it is recommended to download the files in the Font directory to fully enjoy the application.
```
📂Inflow/
├── 📂Inflow/
│   ├── 📂Fonts/
│   │   ├── 📄Inter-Black.ttf
│   │   ├── 📄Inter-BlackItalic.ttf
│   │   ├── 📄Inter-Bold.ttf
│   │   ├── 📄Inter-BoldItalic.ttf
│   │   ├── 📄InterDisplay-Black.ttf
│   │   ├── 📄InterDisplay-BlackItalic.ttf
│   │   ├── 📄InterDisplay-Bold.ttf
│   │   ├── 📄InterDisplay-BoldItalic.ttf
│   │   ├── 📄InterDisplay-ExtraLight.ttf
│   │   ├── 📄InterDisplay-ExtraLightItalic.ttf
│   │   ├── 📄InterDisplay-Italic.ttf
│   │   ├── 📄InterDisplay-Light.ttf
│   │   ├── 📄InterDisplay-LightItalic.ttf
│   │   ├── 📄InterDisplay-Medium.ttf
│   │   ├── 📄InterDisplay-MediumItalic.ttf
│   │   ├── 📄InterDisplay-Regular.ttf
│   │   ├── 📄InterDisplay-SemiBold.ttf
│   │   ├── 📄InterDisplay-SemIBoldItalic.ttf
│   │   ├── 📄InterDisplay-Thin.ttf
│   │   ├── 📄InterDisplay-ThinItalic.ttf
│   │   ├── 📄Inter-ExtraBold.ttf
│   │   ├── 📄Inter-ExtraBoldItalic.ttf
│   │   ├── 📄Inter-ExtraLight.ttf
│   │   ├── 📄Inter-ExtraLightItalic.ttf
│   │   ├── 📄Inter-Italic.ttf
│   │   ├── 📄Inter-Light.ttf
│   │   ├── 📄Inter-LightItalic.ttf
│   │   ├── 📄Inter-Medium.ttf
│   │   ├── 📄Inter-MediumItalic.ttf
│   │   ├── 📄Inter-Regular.ttf
│   │   ├── 📄Inter-SemiBold.ttf
│   │   ├── 📄Inter-SemiBoldItalic.ttf
│   │   ├── 📄Inter-Thin.ttf
│   │   └── 📄Inter-ThinItalic.ttf
├── 📄LICENSE
└── 📄README.md
```

![Header6](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/8.png?raw=true)
---

|Name|  Role|  Github |
|--|--|--|
| Rheman Pasia |  Back-End Developer / Researcher| https://github.com/riri-cpp |
| Daedan Alcantara| UI/UX Designer | https://github.com/DaedanAlcantara  |
| Nicole Hepuller | Developer  | https://github.com/katenicolehepuller |