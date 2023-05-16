using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace OS_Project_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnRR.Enabled = false;
        }

        //Reset Scheduler 
        private void Reset_Scheduler()
        {
            SchedulerTable.Controls.Clear();
            AvgWaiting.Text = "0ms";
            AvgTurnaround.Text = "0ms";
            IdealInformation.Text = "Information about CPU Ideal Time";
        }

        private void RemoveAndPushProcess(int[,] processes)
        {
            int numRows = processes.GetLength(0);
            int numCols = processes.GetLength(1);

            int processName = processes[0, 0];
            int processArrival = processes[0, 1];
            int processBrust = processes[0, 2];

            // Shift the rows up by one position
            for (int i = 1; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    processes[i - 1, j] = processes[i, j];
                }
            }

            // Assign the values of the first row to the last row
            //for (int j = 0; j < numCols; j++)
            //{
            processes[numRows - 1, 0] = processName;
            processes[numRows - 1, 1] = processArrival;
            processes[numRows - 1, 2] = processBrust;
            //}
        }

        private int[,] ProcessNameToEqualMinus1(int[,]processes, int rowIndex)
        {
            for (int x = 0; x < processes.GetLength(0); x++)
            {
                if (processes[x, 0] == rowIndex)
                {
                    processes[x, 0] = -1;
                }
            }
            return processes;
        }

        private int[,] SubtractOneFromProcess(int[,]processes, int rowIndex)
        {
            if (processes[rowIndex, 0] == -1)
            {
                return processes;
            }
            else
            {
                for (int x = 0; x < processes.GetLength(0); x++)
                {
                    if (processes[x, 0] == rowIndex)
                    {
                        processes[x, 2] -= 1;
                    }
                }
                return processes;
            }
        }
        private int[,] RemoveProcess(int[,] processes, int rowIndex)
        {
            int index = 0;
            int[,] newProcesses = new int[processes.GetLength(0) - 1, processes.GetLength(1)];
            for (int x = 0; x < processes.GetLength(0); x++)
            {
                if (processes[x, 0] != rowIndex)
                {
                    for (int j = 0; j < processes.GetLength(1); j++)
                    {
                        newProcesses[index, j] = processes[x, j];
                    }
                    index++;
                }
            }
            return newProcesses;
        }

        // افتراض المصفوفة مرتبة بالفعل بناءً على Burst Time
        private void SortByBurstTime(int[,] processes)
        {
            int n = processes.GetLength(0);

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (processes[j, 2] < processes[minIndex, 2])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    // تبادل الصفوف
                    int tempName = processes[minIndex, 0];
                    int tempArrival = processes[minIndex, 1];
                    int tempBurst = processes[minIndex, 2];

                    processes[minIndex, 0] = processes[i, 0];
                    processes[minIndex, 1] = processes[i, 1];
                    processes[minIndex, 2] = processes[i, 2];

                    processes[i, 0] = tempName;
                    processes[i, 1] = tempArrival;
                    processes[i, 2] = tempBurst;
                }
            }
        }

        private int[,] SortByBurstTime_CurrentTime_SJFp(int[,] processes, int currentTime)
        {
            int[,] arrivedProcesses = new int[processes.GetLength(0), 3];
            int n = processes.GetLength(0);
            int z = 0;

            for (int i = 0; i < n; i++)
            {
                if (processes[i, 1] <= currentTime && processes[i, 0] != -1)
                {
                    arrivedProcesses[z, 0] = processes[i, 0];
                    arrivedProcesses[z, 1] = processes[i, 1];
                    arrivedProcesses[z, 2] = processes[i, 2];
                    z++;
                }

            }

            if (arrivedProcesses[0, 2] != 0)
            {
                int[,] updated_arrivedProcesses = new int[z, 3];
                for (int i = 0; i < z; i++)
                {
                    updated_arrivedProcesses[i, 0] = arrivedProcesses[i, 0]; //i + counter;
                    updated_arrivedProcesses[i, 1] = arrivedProcesses[i, 1];
                    updated_arrivedProcesses[i, 2] = arrivedProcesses[i, 2];
                }

                SortByBurstTime(updated_arrivedProcesses);
                int[,] last_updated_arrivedProcesses = new int[1, 3];
                last_updated_arrivedProcesses[0, 0] = updated_arrivedProcesses[0, 0];
                last_updated_arrivedProcesses[0, 1] = updated_arrivedProcesses[0, 1];
                last_updated_arrivedProcesses[0, 2] = updated_arrivedProcesses[0, 2];

                return last_updated_arrivedProcesses;
            }

            else
            {
                return arrivedProcesses;
            }
        }

        private int[,] SortByBurstTime_CurrentTime_SJFnp(int[,] processes, int currentTime)
        {
            int[,] arrivedProcesses = new int[processes.GetLength(0), 3];
            int n = processes.GetLength(0);
            int z = 0;

            for (int i = 0; i < n; i++)
            {
                if (processes[i, 1] <= currentTime)
                {
                    arrivedProcesses[z, 0] = processes[i, 0];
                    arrivedProcesses[z, 1] = processes[i, 1];
                    arrivedProcesses[z, 2] = processes[i, 2];
                    z++;
                }
                
            }

            if (arrivedProcesses[0, 2] != 0)
            {
                int[,] updated_arrivedProcesses = new int[z, 3];
                for (int i = 0; i < z; i++)
                {
                    updated_arrivedProcesses[i, 0] = arrivedProcesses[i, 0]; //i + counter;
                    updated_arrivedProcesses[i, 1] = arrivedProcesses[i, 1];
                    updated_arrivedProcesses[i, 2] = arrivedProcesses[i, 2];
                }

                SortByBurstTime(updated_arrivedProcesses);

                int[,] last_updated_arrivedProcesses = new int[1, 3];
                last_updated_arrivedProcesses[0, 0] = updated_arrivedProcesses[0, 0];
                last_updated_arrivedProcesses[0, 1] = updated_arrivedProcesses[0, 1];
                last_updated_arrivedProcesses[0, 2] = updated_arrivedProcesses[0, 2];

                return last_updated_arrivedProcesses;
            }

            else
            {
                return arrivedProcesses;
            }
        }

        // افتراض المصفوفة مرتبة بالفعل بناءً على Arrival Time
        private void SortByArrivalTime(int[,] processes)
        {
            int n = processes.GetLength(0);

            for (int i = 0; i < n - 1; i++)
            {
                if (processes[i, 2] != 0)
                {
                    int minIndex = i;

                    for (int j = i + 1; j < n; j++)
                    {
                        if (processes[j, 1] < processes[minIndex, 1])
                        {
                            minIndex = j;
                        }
                    }

                    if (minIndex != i)
                    {
                        // تبادل الصفوف
                        int tempName = processes[minIndex, 0];
                        int tempArrival = processes[minIndex, 1];
                        int tempBurst = processes[minIndex, 2];

                        processes[minIndex, 0] = processes[i, 0];
                        processes[minIndex, 1] = processes[i, 1];
                        processes[minIndex, 2] = processes[i, 2];

                        processes[i, 0] = tempName;
                        processes[i, 1] = tempArrival;
                        processes[i, 2] = tempBurst;
                    }
                }
            }
        }

        private void ProcessNum_TextChanged(object sender, EventArgs e)
        {
            int rowCount;

            // حاول تحويل قيمة TextBox إلى عدد صحيح، إذا فشل التحويل استخدم قيمة الـ "RowCount" الافتراضية
            if (!int.TryParse(ProcessNum.Text, out rowCount))
            {
                rowCount = ProcessesData.RowCount;
            }

            else
            {
                if (rowCount <= 10 && rowCount != 0)
                {
                    // حدد عدد الصفوف في DataGridView
                    ProcessesData.RowCount = rowCount;
                    //عايز اخلى العمود الاول كله يتملى لوحده من 1 ل العدد الى هو دخله
                    for (int i = 0; i < rowCount; i++)
                    {
                        ProcessesData.Rows[i].Cells[0].Value = i;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter number between 1 and 10!!", "Error"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            //Clear the WaititngTime and TurnaroundTime and IdealInformation
            AvgWaiting.Text = "0ms";
            AvgTurnaround.Text = "0ms";
            IdealInformation.Text = "Information about CPU Ideal Time";

            //Clear the Scheduler
            SchedulerTable.Controls.Clear();

            //Clear the QuantumValue
            txtQuantum.Text = "EnterQuantumValue";

            // Clear the processes matrix
            Array.Clear(processes,0, processes.Length);

            // Clear the text in the text boxes
            ProcessNum.Clear();
            ProcessesData.Rows.Clear();

            // Reset the column headers
            ProcessesData.Columns.Clear();
            ProcessesData.Columns.Add("Process Name", "Process Name");
            ProcessesData.Columns.Add("Arrival Time", "Arrival Time");
            ProcessesData.Columns.Add("Burst Time", "Burst Time");
        }

        private void btnFCFS_Click(object sender, EventArgs e)
        {
            Reset_Scheduler();

            int[,] FCFS_copy_processes = processes;
            SortByArrivalTime(FCFS_copy_processes);

            // تحديد عدد العمليات
            int processCount = FCFS_copy_processes.GetLength(0);

            // إنشاء مصفوفة لحفظ أسماء العمليات في الترتيب الجديد
            string[] processNames = new string[processCount];

            // حفظ أسماء العمليات في الترتيب الجديد
            for (int i = 0; i < processCount; i++)
            {
                processNames[i] = "Process " + FCFS_copy_processes[i, 0].ToString();
            }
            
            SchedulerTable.Controls.Clear();

            SchedulerTable.ColumnCount = processCount;
            SchedulerTable.RowCount = 1;

            int numberOfIdealTime = 0;


            if (FCFS_copy_processes[0, 1] != 0)
            {
                numberOfIdealTime++;
                IdealInformation.Text += "\n 1 => Before P0";
            }

            // إضافة الـ Labels للـ TableLayoutPanel على أساس الترتيب الجديد
            for (int i = 0; i < processCount; i++)
            {
                Label label = new Label();
                label.Text = processNames[i];
                //label.Dock = DockStyle.Fill;
                //label.TextAlign = ContentAlignment.MiddleCenter;
                SchedulerTable.Controls.Add(label, i, 0);
            }

            int n = FCFS_copy_processes.GetLength(0);
            int[] waitingTime = new int[n];
            int[] finishTime = new int[n];
            int[] turnAroundTime = new int[n];

            // حساب waitingTime و finishTime لكل عملية
            waitingTime[0] = 0;
            finishTime[0] = FCFS_copy_processes[0, 1] + FCFS_copy_processes[0, 2];
            turnAroundTime[0] = finishTime[0] - FCFS_copy_processes[0, 1];
            int counter = 0;

            for (int i = 1; i < n; i++)
            {
                if ((finishTime[i - 1] + counter) >= FCFS_copy_processes[i, 1])
                {
                    waitingTime[i] = (finishTime[i - 1] + counter) - FCFS_copy_processes[i, 1];
                    finishTime[i] = (finishTime[i - 1] + counter) + FCFS_copy_processes[i, 2];
                    turnAroundTime[i] = finishTime[i] - FCFS_copy_processes[i, 1];
                    counter = 0;
                }
                else
                {
                    counter += FCFS_copy_processes[i, 1] - finishTime[i - 1];
                    i--;
                    numberOfIdealTime++;
                    IdealInformation.Text += $"\n {numberOfIdealTime} => Between P{FCFS_copy_processes[i, 0]} and P{FCFS_copy_processes[i + 1, 0]}";
                }
            }

            // حساب متوسط ​​وقت الانتظار لجميع العمليات
            double avgWaitingTime = waitingTime.Average();
            double avgTurnaroundTime=turnAroundTime.Average();

            AvgWaiting.Text = avgWaitingTime.ToString() + "ms";
            AvgTurnaround.Text = avgTurnaroundTime.ToString() + "ms";
            if (numberOfIdealTime != 0)
            { 
                IdealInformation.Text += $"\n Total Ideal Times = {numberOfIdealTime.ToString()}"; 
            }
            else
            {
                IdealInformation.Text += "\n There Is No Ideal Times!!";
            }
        }

        private void btnSJFp_Click(object sender, EventArgs e)
        {
            Reset_Scheduler();
            int currentTime = processes[0, 1];
            int[,] SJFp_copy_Processes = processes;
            int[,] SortedProcesses; 
            int[,] RealSortedProcesses; 
            int n = SJFp_copy_Processes.GetLength(0);
            int numberOfIdealTime = 0;
            int ProcessToRemove;
            int ProcessToSubtract;
            int FirstArrivalTime = SJFp_copy_Processes[0, 1];
            int LastProcessesNumber = 0;
            bool allArrivalSame = true;
            bool ProcessesFinished;

            for (int i = 1; i < n; i++)
            {
                if (FirstArrivalTime == SJFp_copy_Processes[i, 1])
                {
                    continue;
                }
                else
                {
                    allArrivalSame = false;
                    break;
                }
            }

            if (allArrivalSame)
            {
                SortedProcesses = new int[processes.GetLength(0), processes.GetLength(1)];
                for (int i = 0; i < n; i++)
                {
                    int[,] arrivedProcesses = SortByBurstTime_CurrentTime_SJFnp(SJFp_copy_Processes, currentTime);

                    if (arrivedProcesses[0, 2] != 0)
                    {
                        for (int j = 0; j < arrivedProcesses.GetLength(0); j++)
                        {
                            SortedProcesses[j + i, 0] = arrivedProcesses[j, 0];
                            SortedProcesses[j + i, 1] = arrivedProcesses[j, 1];
                            SortedProcesses[j + i, 2] = arrivedProcesses[j, 2];
                            currentTime += SortedProcesses[j + i, 2];     //arrivedProcesses[j, 2];
                        }
                    }

                    if (SJFp_copy_Processes.GetLength(0) != 1)
                    {
                        ProcessToRemove = SortedProcesses[i, 0];
                        //هشيل الصف الى دخل الSortedProcesses دلوقتى بس من المصفوفة الاساسية ال SJF بحيث المرة الجاية اشتغل من غيرها
                        SJFp_copy_Processes = RemoveProcess(SJFp_copy_Processes, ProcessToRemove);
                    }

                    if (arrivedProcesses[0, 2] == 0)
                    {
                        currentTime += SJFp_copy_Processes[0, 1] - currentTime;
                        numberOfIdealTime++;
                        i--;
                    }

                }

                int ProcessCount = SortedProcesses.GetLength(0);

                // إنشاء مصفوفة لحفظ أسماء العمليات في الترتيب الجديد
                string[] ProcessNames = new string[ProcessCount];

                // حفظ أسماء العمليات في الترتيب الجديد
                for (int i = 0; i < ProcessCount; i++)
                {
                    ProcessNames[i] = "Process " + SortedProcesses[i, 0].ToString(); //$"{i}";
                }

                SchedulerTable.Controls.Clear();

                SchedulerTable.ColumnCount = ProcessCount;
                SchedulerTable.RowCount = 1;

                if (SortedProcesses[0, 1] != 0)
                {
                    numberOfIdealTime++;
                    IdealInformation.Text += "\n 1 => Before P0";
                }

                // إضافة الـ Labels للـ TableLayoutPanel على أساس الترتيب الجديد
                for (int i = 0; i < ProcessCount; i++)
                {
                    Label label = new Label();
                    label.Text = ProcessNames[i];
                    //label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    SchedulerTable.Controls.Add(label, i, 0);
                }

                // حساب waitingTime و finishTime و turnAround لكل عملية
                int N_Sorted = SortedProcesses.GetLength(0);
                int[] WaitingTime = new int[n];
                int[] FinishTime = new int[n];
                int[] TurnAroundTime = new int[n];

                // حساب waitingTime و finishTime لكل عملية
                WaitingTime[0] = 0;
                FinishTime[0] = SortedProcesses[0, 1] + SortedProcesses[0, 2];
                TurnAroundTime[0] = FinishTime[0] - SortedProcesses[0, 1];
                int Counter_Sorted = 0;

                for (int i = 1; i < N_Sorted; i++)
                {
                    if ((FinishTime[i - 1] + Counter_Sorted) >= SortedProcesses[i, 1])
                    {
                        WaitingTime[i] = (FinishTime[i - 1] + Counter_Sorted) - SortedProcesses[i, 1];
                        FinishTime[i] = (FinishTime[i - 1] + Counter_Sorted) + SortedProcesses[i, 2];
                        TurnAroundTime[i] = FinishTime[i] - SortedProcesses[i, 1];
                        Counter_Sorted = 0;
                    }
                    else
                    {
                        Counter_Sorted += SortedProcesses[i, 1] - FinishTime[i - 1];
                        i--;
                        //numberOfIdealTime++;
                        IdealInformation.Text += $"\n {numberOfIdealTime} => Between P{SortedProcesses[i, 0]} and P{SortedProcesses[i + 1, 0]}";
                    }
                }

                // حساب متوسط ​​وقت الانتظار لجميع العمليات
                double AvgWaitingTime = WaitingTime.Average();
                double AvgTurnaroundTime = TurnAroundTime.Average();

                AvgWaiting.Text = AvgWaitingTime.ToString() + "ms";
                AvgTurnaround.Text = AvgTurnaroundTime.ToString() + "ms";
                if (numberOfIdealTime != 0)
                {
                    IdealInformation.Text += $"\n Total Ideal Times = {numberOfIdealTime.ToString()}";
                }
                else
                {
                    IdealInformation.Text += "\n There Is No Ideal Times!!";
                }

                return;
            }

            else
            {
                SortedProcesses = new int[processes.GetLength(0)+20, processes.GetLength(1)]; //+20 احتياطى للعمليات الى هتطلع وتدخل
                int i = 0;
                while ( true )   
                {
                    int[,] arrivedProcesses = SortByBurstTime_CurrentTime_SJFp(SJFp_copy_Processes, currentTime);
                    if (arrivedProcesses[0, 2] != 0)
                    {

                        for (int j = 0; j < arrivedProcesses.GetLength(0); j++)
                        {

                            if (i == 0 && currentTime == SJFp_copy_Processes[0,1])
                            {
                                SortedProcesses[j + i, 0] = arrivedProcesses[j, 0];
                                SortedProcesses[j + i, 1] = arrivedProcesses[j, 1];
                                SortedProcesses[j + i, 2] = arrivedProcesses[j, 2];
                                currentTime += 1;     
                            }
                            else
                            {
                                if (SortedProcesses[j + i, 0] != arrivedProcesses[j, 0])
                                {
                                    i++;
                                    SortedProcesses[j + i, 0] = arrivedProcesses[j, 0];
                                    SortedProcesses[j + i, 1] = arrivedProcesses[j, 1];
                                    SortedProcesses[j + i, 2] = arrivedProcesses[j, 2];
                                    currentTime += 1;     
                                }
                                else
                                {
                                    currentTime += 1;
                                    break;
                                }
                            }
                        }

                        ProcessToRemove = SortedProcesses[i, 0];
                        ProcessToSubtract = SortedProcesses[i, 0];
                        if (SJFp_copy_Processes.GetLength(0) != 1 && SJFp_copy_Processes[ProcessToRemove, 2] == 1)
                        {

                            //هشيل الصف الى دخل الSortedProcesses دلوقتى بس من المصفوفة الاساسية ال SJF بحيث المرة الجاية اشتغل من غيرها
                            SJFp_copy_Processes = ProcessNameToEqualMinus1(SJFp_copy_Processes, ProcessToRemove);
                        }
                        if (SJFp_copy_Processes.GetLength(0) != 1 && SortedProcesses[i, 2] >= 1
                            && SJFp_copy_Processes[ProcessToSubtract, 2] >= 1)
                        {

                            SJFp_copy_Processes = SubtractOneFromProcess(SJFp_copy_Processes, ProcessToSubtract);
                            SortedProcesses[i, 2] = currentTime;
                        }

                    }
                    
                    if (arrivedProcesses[0, 2] == 0)
                    {
                        int processesNotEqualMinus1 = 0;

                        for (int x = 0; x < n; x++)
                        {
                            if (SJFp_copy_Processes[x, 0] != -1)
                            {
                                processesNotEqualMinus1++;
                            }
                        }

                        int[,] ArrivedProcessesAfterIdeal = new int[processesNotEqualMinus1, 3];

                        for (int x = 0; x < n; x++)
                        {
                            if (SJFp_copy_Processes[x, 0] != -1)
                            {
                                ArrivedProcessesAfterIdeal[0, 0] = SJFp_copy_Processes[x, 0];
                                ArrivedProcessesAfterIdeal[0, 1] = SJFp_copy_Processes[x, 1];
                                ArrivedProcessesAfterIdeal[0, 2] = SJFp_copy_Processes[x, 2];
                            }
                        }
                        SortByArrivalTime(ArrivedProcessesAfterIdeal);

                        currentTime += ArrivedProcessesAfterIdeal[0, 1] - currentTime; //SJFnp_copy_Processes[0, 1] - currentTime;
                        numberOfIdealTime++;
                        //i--;
                        continue;
                        
                    }

                    

                    ProcessesFinished = true;
                    for (int j = 0; j < n; j++)
                    {
                        if (SJFp_copy_Processes[j, 0] == -1)
                        {
                            continue;
                        }
                        else
                        {
                            ProcessesFinished = false;
                            break;
                        }
                    }

                    if(ProcessesFinished)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }

                }
            }

            for (int i = 0; i < SortedProcesses.GetLength(0); i++)
            {
                if (SortedProcesses[i, 2] != 0)
                {
                    LastProcessesNumber++;
                }
            }

            RealSortedProcesses = new int[LastProcessesNumber, 3];

            for (int i = 0; i < RealSortedProcesses.GetLength(0); i++)
            {
                if (SortedProcesses[i, 2] != 0)
                {
                    RealSortedProcesses[i, 0] = SortedProcesses[i, 0];
                    RealSortedProcesses[i, 1] = SortedProcesses[i, 1];
                    RealSortedProcesses[i, 2] = SortedProcesses[i, 2];
                }
            }

            int processCount = RealSortedProcesses.GetLength(0);

            // إنشاء مصفوفة لحفظ أسماء العمليات في الترتيب الجديد
            string[] processNames = new string[processCount];

            // حفظ أسماء العمليات في الترتيب الجديد
            for (int i = 0; i < processCount; i++)
            {
                processNames[i] = "Process "+ RealSortedProcesses[i, 0].ToString(); //$"{i}";
            }

            SchedulerTable.Controls.Clear();

            SchedulerTable.ColumnCount = processCount;
            SchedulerTable.RowCount = 1;

            if (SortedProcesses[0, 1] != 0)
            {
                numberOfIdealTime++;
                IdealInformation.Text += "\n 1 => Before P0";
            }

            // إضافة الـ Labels للـ TableLayoutPanel على أساس الترتيب الجديد
            for (int i = 0; i < processCount; i++)
            {
                Label label = new Label();
                label.Text = processNames[i];
                //label.Dock = DockStyle.Fill;
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.AutoSize = false;
                
                SchedulerTable.Controls.Add(label, i, 0);
            }

            
            // حساب waitingTime و finishTime و turnAround لكل عملية
            int n_Sorted = RealSortedProcesses.GetLength(0);
            int[] waitingTime = new int[n];
            int[] finishTime = new int[n];
            int[] turnAroundTime = new int[n];
            int[] ProcessCounter = new int[n];
            int counter_Sorted = 0;

            waitingTime[0] = 0;
            finishTime[0] = RealSortedProcesses[1, 1];
            ProcessCounter[0]++;

            for (int i = 1; i < n_Sorted; i++)
            {

                if ((finishTime[RealSortedProcesses[i - 1, 0]] + counter_Sorted) >= RealSortedProcesses[i, 1])
                {
                    if(counter_Sorted!=0)
                    {
                        if (ProcessCounter[RealSortedProcesses[i, 0]] == 0)
                        {
                            waitingTime[RealSortedProcesses[i, 0]] = 
                                (finishTime[RealSortedProcesses[i - 1, 0]] + counter_Sorted) -
                                SJFp_copy_Processes[RealSortedProcesses[i, 0], 1];
                            finishTime[RealSortedProcesses[i, 0]] = RealSortedProcesses[i, 2];
                            ProcessCounter[RealSortedProcesses[i, 0]]++;
                            counter_Sorted = 0;
                        }
                        else if (ProcessCounter[RealSortedProcesses[i, 0]] > 0)
                        {
                            waitingTime[RealSortedProcesses[i, 0]] += finishTime[RealSortedProcesses[i - 1, 0]] - finishTime[RealSortedProcesses[i, 0]];
                            finishTime[RealSortedProcesses[i, 0]] = RealSortedProcesses[i, 2];
                            ProcessCounter[RealSortedProcesses[i, 0]]++;
                            counter_Sorted = 0;
                        }
                        continue;
                    }
                    if (ProcessCounter[RealSortedProcesses[i, 0]] == 0)
                    {
                        waitingTime[RealSortedProcesses[i, 0]] = finishTime[RealSortedProcesses[i-1, 0]] -
                            SJFp_copy_Processes[RealSortedProcesses[i, 0], 1];
                        finishTime[RealSortedProcesses[i, 0]] = RealSortedProcesses[i, 2];
                        ProcessCounter[RealSortedProcesses[i, 0]]++;
                    }
                    else if (ProcessCounter[RealSortedProcesses[i, 0]] > 0)
                    {
                        waitingTime[RealSortedProcesses[i, 0]] += 
                            finishTime[RealSortedProcesses[i-1, 0]] - finishTime[RealSortedProcesses[i, 0]];
                        finishTime[RealSortedProcesses[i, 0]] = RealSortedProcesses[i, 2];
                        ProcessCounter[RealSortedProcesses[i, 0]]++;
                    }

                }
                
                else
                {
                    counter_Sorted += RealSortedProcesses[i, 1] - finishTime[RealSortedProcesses[i-1, 0] ];
                    i--;
                    //numberOfIdealTime++;
                    IdealInformation.Text += $"\n {numberOfIdealTime} " +
                        $"=> Between P{RealSortedProcesses[i, 0]} and P{RealSortedProcesses[i + 1, 0]}";
                }

                
            }

            for (int i = 0; i < n; i++)
            {
                turnAroundTime[i] 
                    = finishTime[i] - SJFp_copy_Processes[i, 1];
            }

            // حساب متوسط ​​وقت الانتظار لجميع العمليات
            double avgWaitingTime = waitingTime.Average();
            double avgTurnaroundTime = turnAroundTime.Average();

            AvgWaiting.Text = avgWaitingTime.ToString() + "ms";
            AvgTurnaround.Text = avgTurnaroundTime.ToString() + "ms";
            if (numberOfIdealTime != 0)
            {
                IdealInformation.Text += $"\n Total Ideal Times = {numberOfIdealTime.ToString()}";
            }
            else
            {
                IdealInformation.Text += "\n There Is No Ideal Times!!";
            }

        }

        private void btnSJFnp_Click(object sender, EventArgs e)
        {
            Reset_Scheduler();
            int currentTime = processes[0, 1];
            int[,] SJFnp_copy_Processes = processes;
            int[,] SortedProcesses = new int[processes.GetLength(0), processes.GetLength(1)];
            int n = SJFnp_copy_Processes.GetLength(0);
            int numberOfIdealTime = 0;
            int ProcessToRemove;
            
            for (int i = 0; i < n; i++)
            {
                int[,] arrivedProcesses = SortByBurstTime_CurrentTime_SJFnp(SJFnp_copy_Processes, currentTime);

                if (arrivedProcesses[0, 2] != 0)
                {
                    for (int j = 0; j < arrivedProcesses.GetLength(0); j++)
                    {
                        SortedProcesses[j + i, 0] = arrivedProcesses[j, 0];
                        SortedProcesses[j + i, 1] = arrivedProcesses[j, 1];
                        SortedProcesses[j + i, 2] = arrivedProcesses[j, 2];
                        currentTime += SortedProcesses[j + i, 2];     //arrivedProcesses[j, 2];
                    }
                }

                if (SJFnp_copy_Processes.GetLength(0) != 1)
                {
                    ProcessToRemove = SortedProcesses[i, 0];
                    //هشيل الصف الى دخل الSortedProcesses دلوقتى بس من المصفوفة الاساسية ال SJF بحيث المرة الجاية اشتغل من غيرها
                    SJFnp_copy_Processes = RemoveProcess(SJFnp_copy_Processes, ProcessToRemove);
                }

                if (arrivedProcesses[0, 2] == 0)
                {
                    currentTime += SJFnp_copy_Processes[0, 1] - currentTime;
                    numberOfIdealTime++;
                    i--;
                }

            }

            int processCount = SortedProcesses.GetLength(0);

            // إنشاء مصفوفة لحفظ أسماء العمليات في الترتيب الجديد
            string[] processNames = new string[processCount];

            // حفظ أسماء العمليات في الترتيب الجديد
            for (int i = 0; i < processCount; i++)
            {
                processNames[i] = "Process " + SortedProcesses[i, 0].ToString(); //$"{i}";
            }

            SchedulerTable.Controls.Clear();

            SchedulerTable.ColumnCount = processCount;
            SchedulerTable.RowCount = 1;

            if (SortedProcesses[0, 1] != 0)
            {
                numberOfIdealTime++;
                IdealInformation.Text += "\n 1 => Before P0";
            }

            // إضافة الـ Labels للـ TableLayoutPanel على أساس الترتيب الجديد
            for (int i = 0; i < processCount; i++)
            {
                Label label = new Label();
                label.Text = processNames[i];
                //label.Dock = DockStyle.Fill;
                label.TextAlign = ContentAlignment.MiddleCenter;
                SchedulerTable.Controls.Add(label, i, 0);
            }

            // حساب waitingTime و finishTime و turnAround لكل عملية
            int n_Sorted = SortedProcesses.GetLength(0);
            int[] waitingTime = new int[n];
            int[] finishTime = new int[n];
            int[] turnAroundTime = new int[n];

            // حساب waitingTime و finishTime لكل عملية
            waitingTime[0] = 0;
            finishTime[0] = SortedProcesses[0, 1] + SortedProcesses[0, 2];
            turnAroundTime[0] = finishTime[0] - SortedProcesses[0, 1];
            int counter_Sorted = 0;

            for (int i = 1; i < n_Sorted; i++)
            {
                if ((finishTime[i - 1] + counter_Sorted) >= SortedProcesses[i, 1])
                {
                    waitingTime[i] = (finishTime[i - 1] + counter_Sorted) - SortedProcesses[i, 1];
                    finishTime[i] = (finishTime[i - 1] + counter_Sorted) + SortedProcesses[i, 2];
                    turnAroundTime[i] = finishTime[i] - SortedProcesses[i, 1];
                    counter_Sorted = 0;
                }
                else
                {
                    counter_Sorted += SortedProcesses[i, 1] - finishTime[i - 1];
                    i--;
                    //numberOfIdealTime++;
                    IdealInformation.Text += $"\n {numberOfIdealTime} => Between P{SortedProcesses[i, 0]} and P{SortedProcesses[i + 1, 0]}";
                }
            }

            // حساب متوسط ​​وقت الانتظار لجميع العمليات
            double avgWaitingTime = waitingTime.Average();
            double avgTurnaroundTime = turnAroundTime.Average();

            AvgWaiting.Text = avgWaitingTime.ToString() + "ms";
            AvgTurnaround.Text = avgTurnaroundTime.ToString() + "ms";
            if (numberOfIdealTime != 0)
            {
                IdealInformation.Text += $"\n Total Ideal Times = {numberOfIdealTime.ToString()}";
            }
            else
            {
                IdealInformation.Text += "\n There Is No Ideal Times!!";
            }
        }
        
        
        private void btnRR_Click(object sender, EventArgs e)
        {
            Reset_Scheduler();

            int[,] RR_copy_Processes = processes;
            int[,] Backup_RR_copy_Processes = (int[,])processes.Clone();
            int[,] Updated_RR_copy_Processes = new int[30, 3];
            int[,] Last_Updated_RR_copy_Processes;
            int Quantum = int.Parse(txtQuantum.Text);
            int n = RR_copy_Processes.GetLength(0);
            SortByArrivalTime(RR_copy_Processes);
            int CurrentTime = RR_copy_Processes[0, 1];
            int i = 0;
            int processCounter = 0;
            int numberOfIdealTime = 0;
            bool ProcessesFinished;

            if (RR_copy_Processes[0, 1] != 0)
            {
                numberOfIdealTime++;
                IdealInformation.Text += "\n 1 => Before P0";
            }

            while (true)
            {
                if (RR_copy_Processes[0, 2] > 0)
                {
                    Updated_RR_copy_Processes[i, 0] = RR_copy_Processes[0, 0];
                    Updated_RR_copy_Processes[i, 1] = CurrentTime;
                    if (RR_copy_Processes[0, 2] >= Quantum)
                    {
                        Updated_RR_copy_Processes[i, 2] = CurrentTime + Quantum;
                        RR_copy_Processes[0, 2] -= Quantum;
                        CurrentTime += Quantum;
                        processCounter++;
                    }
                    else
                    {
                        Updated_RR_copy_Processes[i, 2] = CurrentTime + RR_copy_Processes[0,2];
                        CurrentTime += RR_copy_Processes[0, 2];
                        RR_copy_Processes[0, 2] -= RR_copy_Processes[0,2];
                        processCounter++;
                    }
                }
                i++;
                RemoveAndPushProcess(RR_copy_Processes);
                ProcessesFinished = true;
                for (int j = 0; j < RR_copy_Processes.GetLength(0); j++)
                {
                    if (RR_copy_Processes[j, 2] <= 0)
                    {
                        continue;
                    }
                    else
                    {
                        ProcessesFinished = false;
                        break;
                    }
                }

                if (ProcessesFinished)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }



            Last_Updated_RR_copy_Processes = new int[processCounter, 3];
            int index = 0;
            for (int j = 0; j < Updated_RR_copy_Processes.GetLength(0); j++)
            {
                if (Updated_RR_copy_Processes[j, 2] > 0)
                {
                    Last_Updated_RR_copy_Processes[index, 0] = Updated_RR_copy_Processes[j, 0];
                    Last_Updated_RR_copy_Processes[index, 1] = Updated_RR_copy_Processes[j, 1];
                    Last_Updated_RR_copy_Processes[index, 2] = Updated_RR_copy_Processes[j, 2];
                    index++;
                }
            }

            int processCount = Last_Updated_RR_copy_Processes.GetLength(0);

            // إنشاء مصفوفة لحفظ أسماء العمليات في الترتيب الجديد
            string[] processNames = new string[processCount];

            // حفظ أسماء العمليات في الترتيب الجديد
            for (int x = 0; x < processCount; x++)
            {
                processNames[x] = "Process " + Last_Updated_RR_copy_Processes[x, 0].ToString(); //$"{i}";
            }

            SchedulerTable.Controls.Clear();

            SchedulerTable.ColumnCount = processCount;
            SchedulerTable.RowCount = 1;

            
            // إضافة الـ Labels للـ TableLayoutPanel على أساس الترتيب الجديد
            for (int x = 0; x < processCount; x++)
            {
                Label label = new Label();
                label.Text = processNames[x];
                //label.Dock = DockStyle.Fill;
                label.TextAlign = ContentAlignment.MiddleCenter;
                SchedulerTable.Controls.Add(label, x, 0);
            }

            // حساب waitingTime و finishTime و turnAround لكل عملية
            int n_Sorted = Last_Updated_RR_copy_Processes.GetLength(0);
            int[] waitingTime = new int[n];
            int[] finishTime = new int[n];
            int[] turnAroundTime = new int[n];
            int[] ProcessCounter = new int[n];
            int counter_Sorted = 0;

            waitingTime[0] = 0;
            finishTime[0] = Last_Updated_RR_copy_Processes[1, 1];
            ProcessCounter[0]++;

            for (int z = 1; z < n_Sorted; z++)
            {

                if ((finishTime[Last_Updated_RR_copy_Processes[z - 1, 0]] + counter_Sorted) 
                    >= Last_Updated_RR_copy_Processes[z, 1])
                {
                    if (ProcessCounter[Last_Updated_RR_copy_Processes[z, 0]] == 0)
                    {
                        waitingTime[Last_Updated_RR_copy_Processes[z, 0]] = finishTime[Last_Updated_RR_copy_Processes[z - 1, 0]]
                        - Backup_RR_copy_Processes[Last_Updated_RR_copy_Processes[z, 0], 1];
                        finishTime[Last_Updated_RR_copy_Processes[z, 0]] = Last_Updated_RR_copy_Processes[z, 2];
                        ProcessCounter[Last_Updated_RR_copy_Processes[z, 0]]++;
                    }
                    else
                    {
                        waitingTime[Last_Updated_RR_copy_Processes[z, 0]] += finishTime[Last_Updated_RR_copy_Processes[z - 1, 0]]
                        - finishTime[Last_Updated_RR_copy_Processes[z, 0]];
                        finishTime[Last_Updated_RR_copy_Processes[z, 0]] = Last_Updated_RR_copy_Processes[z, 2];
                        ProcessCounter[Last_Updated_RR_copy_Processes[z, 0]]++;
                    }
                }

                else
                {
                    counter_Sorted += Last_Updated_RR_copy_Processes[z, 1] - finishTime[Last_Updated_RR_copy_Processes[z - 1, 0]];
                    z--;
                    numberOfIdealTime++;
                    IdealInformation.Text += $"\n {numberOfIdealTime} " +
                        $"=> Between P{Last_Updated_RR_copy_Processes[z, 0]} " +
                        $"and P{Last_Updated_RR_copy_Processes[z + 1, 0]}";
                }


            }

            for (int z = 0; z < n; z++)
            {
                turnAroundTime[z]
                    = finishTime[z] - RR_copy_Processes[z, 1];
            }

            // حساب متوسط ​​وقت الانتظار لجميع العمليات
            double avgWaitingTime = waitingTime.Average();
            double avgTurnaroundTime = turnAroundTime.Average();

            AvgWaiting.Text = avgWaitingTime.ToString() + "ms";
            AvgTurnaround.Text = avgTurnaroundTime.ToString() + "ms";
            if (numberOfIdealTime != 0)
            {
                IdealInformation.Text += $"\n Total Ideal Times = {numberOfIdealTime.ToString()}";
            }
            else
            {
                IdealInformation.Text += "\n There Is No Ideal Times!!";
            }

        }

        private void txtQuantum_TextChanged(object sender, EventArgs e)
        {
            Reset_Scheduler();

            if (string.IsNullOrWhiteSpace(txtQuantum.Text)) // التحقق مما إذا كانت القيمة في TextBox فارغة أو تحتوي على مسافات فقط
            {
                btnRR.Enabled = false; // تعطيل الزر
            }
            else
            {
                int num;
                if (int.TryParse(txtQuantum.Text, out num) && num != 0) // التحقق من أن القيمة المدخلة هي رقم صحيح
                {
                    btnRR.Enabled = true; // تمكين الزر لفتح الفورم الجديدة
                }
                else
                {
                    btnRR.Enabled = false; // تعطيل الزر إذا لم يتم إدخال رقم صحيح
                }
            }
        }


        int[,] processes;  // تعريف مصفوفة تحتوي على 10 صفوف و 3 أعمدة
        private void btnSave_Click(object sender, EventArgs e)
        {
            int rowCount;
            if (!int.TryParse(ProcessNum.Text, out rowCount))
            {
                rowCount = ProcessesData.RowCount;
            }

            else
            {
                if (rowCount <= 10 && rowCount != 0)
                {
                    processes = new int[rowCount, 3];

                    for (int i = 0; i < rowCount; i++)
                    {
                        if (!string.IsNullOrEmpty(ProcessesData.Rows[i].Cells[0].Value?.ToString()) 
                            && int.TryParse(ProcessesData.Rows[i].Cells[1].Value?.ToString(), out int arrivalTime) 
                            && int.TryParse(ProcessesData.Rows[i].Cells[2].Value?.ToString(), out int burstTime))
                        {
                            processes[i, 0] = int.Parse(ProcessesData.Rows[i].Cells[0].Value.ToString()); // حفظ اسم العملية كـ string
                            processes[i, 1] = arrivalTime; // حفظ Arrival Time كـ int
                            processes[i, 2] = burstTime; // حفظ Burst Time كـ int
                        }
                        else
                        {
                            // رسالة خطأ أو إجراء بديل
                            MessageBox.Show("Please enter full data of processes!!", "Error"
                                , MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }
            }     
            

        }

        private void btnScheduleReset_Click(object sender, EventArgs e)
        {
            Reset_Scheduler();
        }

    }
}
