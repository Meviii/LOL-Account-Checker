using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AccountChecker.Utility;

public class ConsoleWriter : TextWriter
{

    private RichTextBox _textBox;

    public ConsoleWriter(RichTextBox textBox)
    {
        _textBox = textBox;
    }

    public override void Write(string value)
    {

        if (_textBox.InvokeRequired)
        {
            _textBox.Invoke(new Action(() => _textBox.AppendText(value)));
        }
        else
        {
            _textBox.AppendText(value);
        }
    }


    public override Encoding Encoding => Encoding.Unicode;
}
