using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
            //for (int j = 0; j < processes.GetLength(1); j++)
            //{
            //    processes[rowIndex, j] = -1; // set the values of the row to -1
            //}
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

        private int[,] SortByBurstTime_CurrentTime(int[,] processes, int currentTime)
        {
            //int rowCount = processes.GetLength(0);
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
                    // هحذف الصف بكل قيمه الى طلع اول صف بعد فنكشن السورت عشان اللفة الجاية اشتغل من غيره
                    //processes = RemoveProcess(processes, i); 
                    //i--; // decrement i to process the same row again if necessary
                    //n--; // decrement n since the array is now one row shorter

                    //int currentProcess = arrivedProcesses[i, 0];
                    //int[,] newProcesses = new int[processes.GetLength(0) - 1, processes.GetLength(1)];
                    //int index = 0;
                    //for (int x = 0; x < processes.GetLength(0); x++)
                    //{
                    //    if (processes[x, 0] != currentProcess)
                    //    {
                    //        for (int j = 0; j < processes.GetLength(1); j++)
                    //        {
                    //            newProcesses[index, j] = processes[x, j];
                    //        }
                    //        index++;
                    //    }
                    //}
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

                //int ProcessToRemove = last_updated_arrivedProcesses[0, 0];
                //هحذف الصف بكل قيمه الى طلع اول صف بعد فنكشن السورت عشان اللفة الجاية اشتغل من غيره
                //processes = RemoveProcess(processes, ProcessToRemove);

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
                label.Dock = DockStyle.Fill;
                label.TextAlign = ContentAlignment.MiddleCenter;
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
            
        }

        private void btnSJFnp_Click(object sender, EventArgs e)
        {
            int currentTime = processes[0, 1];
            int[,] SJFnp_copy_Processes = processes;
            int[,] SortedProcesses = new int[processes.GetLength(0), processes.GetLength(1)];
            int n = SJFnp_copy_Processes.GetLength(0);
            int numberOfIdealTime = 0;
            int ProcessToRemove;
            
            for (int i = 0; i < n; i++)
            {
                int[,] arrivedProcesses = SortByBurstTime_CurrentTime(SJFnp_copy_Processes, currentTime);

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
                label.Dock = DockStyle.Fill;
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
            
        }

        private void txtQuantum_TextChanged(object sender, EventArgs e)
        {
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
            SchedulerTable.Controls.Clear();
            AvgWaiting.Text = "0ms";
            AvgTurnaround.Text = "0ms";
            IdealInformation.Text = "Information about CPU Ideal Time";
        }

    }
}
