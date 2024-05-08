﻿using SeminarskaPraksa.AsyncTasks;
using SeminarskaPraksa.Tasks;
using SeminarskaPraksa.Utilities;

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
                tbOutputBox.Text += Environment.NewLine;
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

        private async void OnThreading5_ProxyAndDecorator(object sender, EventArgs e)
        {
            var tasks5 = new Threading5_ProxyAndDecorator(
                PrintInTextbox,
                new CheckNetworkWithPing(),
                new HttpAsyncTask()); ;

            await tasks5.Start();
        }

        private async void OnThreading6_TaskBuilder_Click(object sender, EventArgs e)
        {
            var task6 = new Threading6_TaskBuilder(PrintInTextbox);
            await task6.StartTasks(
                new List<IAsyncTask>
                {
                    new HttpAsyncTask(),
                    new HttpAsyncTask(),
                    new VeryLongTaskAsync()
                }, 5);
        }

        private async void OnThreading7_Barrier_Click(object sender, EventArgs e)
        {
            var logger = new TextBoxLogger(PrintInTextbox);
            var tasks = new Threading7_Barrier(logger);
            await tasks.Start();
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
