using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public class StatusBox : System.Windows.Forms.RichTextBox
{
    [DllImport("user32.dll")]
    private static extern int HideCaret(IntPtr hwnd);

    public StatusBox()
    {
        MouseUp += new System.Windows.Forms.MouseEventHandler(StatusBox_Mouse);
        MouseDown += new System.Windows.Forms.MouseEventHandler(StatusBox_Mouse);
        KeyUp += new System.Windows.Forms.KeyEventHandler(StatusBox_Key);
        KeyDown += new System.Windows.Forms.KeyEventHandler(StatusBox_Key);
    }

    protected override void OnGotFocus(EventArgs e) => HideCaret(this.Handle);

    protected override void OnEnter(EventArgs e) => HideCaret(this.Handle);

    private void StatusBox_Mouse(object sender, System.Windows.Forms.MouseEventArgs e) => HideCaret(this.Handle);

    private void StatusBox_Key(object sender, System.Windows.Forms.KeyEventArgs e) => HideCaret(this.Handle);
}