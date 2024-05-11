using SeminarskaPraksa.Utilities;
using System.Drawing.Text;

namespace SeminarskaPraksa.Tasks
{
    internal class Threading9_TasksInForms
    {
        private readonly TextBoxLogger _textBoxLogger;
        private readonly TextBox _tbOutputBox;

        public Threading9_TasksInForms(TextBoxLogger textBoxLogger, TextBox tbOutputBox)
        {
            _textBoxLogger = textBoxLogger;
            _tbOutputBox = tbOutputBox;

            _textBoxLogger.Log("Threading9_TasksInForms Started");
            ColorTextBox();
            SimulateLoading();
        }

        #region INVOKE METHODS
        private void TextBoxAccess(string text)
        {
            if (_tbOutputBox.InvokeRequired)
                _tbOutputBox.Invoke(new Action<string>(TextBoxAccess), text);
            else
                _tbOutputBox.Text = text;
        }

        private void TextBoxAccess(Color color)
        {
            if (_tbOutputBox.InvokeRequired)
                _tbOutputBox.Invoke(new Action<Color>(TextBoxAccess), color);
            else
                _tbOutputBox.BackColor = color;
        }
        #endregion

        private void ColorTextBox()
        {
            _textBoxLogger.Log("Recolor start");
            var oldColor = _tbOutputBox.BackColor;
            TextBoxAccess(Color.DarkGray);
            Task.Run(() =>
            {
                Task.Delay(300).Wait();
                TextBoxAccess(oldColor);
                _textBoxLogger.Log("Recolor end");
            });
            
        }

        private void SimulateLoading()
        {
            Task.Run(() =>
            {
                int counter = 0;
                while (counter < 20)
                {
                    Task.Delay(100).Wait();
                    TextBoxAccess(_tbOutputBox.Text + "_");
                    counter++;
                }
                _textBoxLogger.Log("Simulate loading done");
            });
        }
    }
}
