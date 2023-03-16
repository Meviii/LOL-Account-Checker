using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AccountChecker.Utility;

// This class represents a custom console output writer that can write to a WinForms RichTextBox control.
public class ConsoleWriter : TextWriter
{

    // This field holds the RichTextBox control that the writer writes to.
    private RichTextBox _textBox;

    // This constructor takes a RichTextBox control as a parameter and initializes the _textBox field.
    public ConsoleWriter(RichTextBox textBox)
    {
        _textBox = textBox;
    }

    // This method is called when the writer writes a string to the console output.
    public override void Write(string value)
    {

        // If the RichTextBox control is being accessed from a different thread than the one that created it,
        // then it must be accessed using the Invoke method of the control's parent form.
        if (_textBox.InvokeRequired)
        {
            // Invoke the AppendText method of the RichTextBox control to add the specified text to the end of its contents.
            _textBox.Invoke(new Action(() => _textBox.AppendText(value)));
        }
        else
        {
            // Append the specified text to the end of the RichTextBox control's contents.
            _textBox.AppendText(value);
        }
    }

    // This property returns the encoding used by the writer.
    public override Encoding Encoding => Encoding.Unicode;
}
