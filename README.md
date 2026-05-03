![inflow](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/2.png?raw=true)
<br/>

![Header1](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/3.png?raw=true)
<div align = center>Welcome to InFlow! Keep it in the flow with the desktop task and performance tracking application based in native C# and Windows Forms structure that enables users to create and plan schedules for different tasks throughout the day. 
Inflow helps users to accomplish tasks -- menial or important -- with the system's time tracker and rapid task flow so you won't miss a task everyday! By incentivising users to keep completing tasks before the time limit, InFlow transforms to-do lists as an efficient, automated routine where finishing tasks can get your Flow Streak to level up.
</div>

![Header2](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/4.png?raw=true)
![UML](https://raw.githubusercontent.com/DaedanAlcantara/Inflow/7e59f16dc46265f3b4cea28a44860b7145a66d03/AOOP%20UML.png)
![Header3](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/5.png?raw=true)
### Dashboard
InFlow boasts a **comprehensive and functional dashboard** that has everything you need in one go! The dashboard tracks the user's completed and dropped tasks, as well as your **Flow Streak** and tasks to be done. Additionaly, the dashboard has a **real-time integrated time and date display** so that users can quickly track of time during tasks.
The Dashboard's Flow Streak is the app's **incentive system** where once a user is able to finish three tasks consecutively, the system counts that as one Flow Streak. After emptying the planner, the streak is saved until the next deployment. However, if the user is to break the flow by dropping a task, the Streak drops back to zero.

### Planner
Planner lets users customize their to-do lists to further optimize and improve their performance. The InFlow Planner uses a **5-star priority rating system** to emphasize importance of each task in the moment as it rolls. **User-friendly interface** for creating tasks are included to maximize **user comfort**. Lastly, the Planner has a **smart auto-sort function** that neatly sorts your tasks according to their priority rating as defined in each task card. In turn, you don't have to worry about thinking which tasks to go first!
### Nitro Flow
Nitro Flow is the star of InFlow's task management. If users want to be in their Flow State while working -- undisturbed and in the zone -- they can use Nitro Flow which is the app's **"Focus Mode"**. 
In NitroFlow, tasks are automatically timed according to their specifications and if the user is not able to declare tasks as completed before the timer runs out, the task is automatically tagged as dropped and moves onto the next task in the list.
Furthermore, users are rewarded in Nitro Flow in that completing two tasks consecutively would gain a Flow Streak.
### Custom Title Bar

### Clean UI Design
![Header4](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/6.png?raw=true)


![Header5](https://github.com/DaedanAlcantara/Inflow/blob/master/InFlow%20MD%20Headers/7.png?raw=true)
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

|Name|  Role|  Github |
|--|--|--|
| Rheman Pasia |  Back-End Developer / Researcher| https://github.com/riri-cpp |
| Daedan Alcantara| UI/UX Designer |https://github.com/DaedanAlcantara |
| Nicole Hepuller | Developer  |https://github.com/katenicolehepuller |