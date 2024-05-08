namespace SeminarskaPraksa.Utilities
{
    internal class TextBoxLogger
    {
        private Action<string> _printInTextbox;

        public TextBoxLogger(Action<string> printInTextbox)
        {
            _printInTextbox = printInTextbox;
        }

        internal void Log(string message)
        {
            _printInTextbox(message);
        }
    }
}
