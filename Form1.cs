using SeminarskaPraksa.AsyncTasks;
using SeminarskaPraksa.Tasks;

namespace SeminarskaPraksa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region Print in TextBox
        internal void PrintInTextbox(string text)
        {
            tbOutputBox.BeginInvoke(new Action(() =>
            {
                tbOutputBox.Text += text + Environment.NewLine;
            }));
        }
        #endregion

        #region Buttons
        private void OnBtnThreading1_Tasks_Click(object sender, EventArgs e)
        {
            var simpleTasks = new Threading1_Tasks(PrintInTextbox);
        }

        private async void OnBtnThreading2_Timer_Click(object sender, EventArgs e)
        {
            int time = 2500;
            using (var timerTask = new Threading2_Timer(PrintInTextbox, 250, time))
            {
                PrintInTextbox($"Čakamo da se timer konča po {(float)time / 1000} sekundah");
                await timerTask.WaitForCompletionAsync();
            }
        }

        private void OnBtnThreading3_Token_Click(object sender, EventArgs e)
        {
            new Threading3_Token(PrintInTextbox, 500, 3000);
        }

        private void OnBtnThreading4_RaceCondition_Click(object sender, EventArgs e)
        {
            new Threading4_RaceCondition(PrintInTextbox, 15, 50);
        }

        private async void OnThreading5_TaskFactory_Click(object sender, EventArgs e)
        {
            var task5 = new Threading5_TaskBuilder(PrintInTextbox);
            await task5.StartTasks(
                new List<ITasks>
                {
                    new HttpAsyncTask(),
                    new HttpAsyncTask(),
                    new VeryLongTaskAsync()
                }, 5); 
        }

        private void OnButton6_Click(object sender, EventArgs e)
        {

        }

        private void OnButton7_Click(object sender, EventArgs e)
        {

        }

        private void OnButton8_Click(object sender, EventArgs e)
        {

        }

        private void OnButton9_Click(object sender, EventArgs e)
        {

        }

        private void OnButton10_Click(object sender, EventArgs e)
        {

        }

        private void OnButton11_Click(object sender, EventArgs e)
        {

        }

        private void OnButton12_Click(object sender, EventArgs e)
        {

        }

        private void OnButton13_Click(object sender, EventArgs e)
        {

        }

        private void OnButton14_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Auxillary
        private void OnBtnClearText_Click(object sender, EventArgs e)
        {
            tbOutputBox.Text = string.Empty;
        }
        #endregion
    }
}
