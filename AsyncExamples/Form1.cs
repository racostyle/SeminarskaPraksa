using AsyncExamples.Tasks;

namespace AsyncExamples
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
                await timerTask.WaitForComplete();
            }
        }

        private void OnButton3_Click(object sender, EventArgs e)
        {

        }

        private void OnButton4_Click(object sender, EventArgs e)
        {

        }

        private void OnButton5_Click(object sender, EventArgs e)
        {

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
