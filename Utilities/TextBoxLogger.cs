using static System.Net.Mime.MediaTypeNames;

namespace SeminarskaPraksa.Utilities
{
    internal class TextBoxLogger
    {
        private readonly TextBox _textBox;

        public TextBoxLogger(TextBox textBox)
        {
            _textBox = textBox;
        }

        internal void Log(string message)
        {
            _textBox.BeginInvoke(new Action(() =>
            {
                _textBox.Text += message + Environment.NewLine;
            }));
        }
    }
}
