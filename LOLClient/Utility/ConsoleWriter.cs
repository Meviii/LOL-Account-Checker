using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOLClient.Utility;

public class ConsoleWriter : TextWriter
{

    private RichTextBox _textBox;

    public ConsoleWriter(RichTextBox textBox)
    {
        _textBox = textBox;
    }

    public override void Write(string value)
    {
        _textBox.AppendText(value);
    }

    public override void Write(char value)
    {
        _textBox.AppendText(value.ToString());
    }

    public override Encoding Encoding => Encoding.Unicode;
}
