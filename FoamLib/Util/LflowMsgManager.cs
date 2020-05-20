using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoamLib.Util
{
    class LflowMsgManager : MessageManager<Message>
    {
        RichTextBox richTextBox = null;
        TextBox textBox = null;
        ProgressBar progressBar = null;
        Color logTextColor = Color.Black;
        Color errTextColor = Color.Red;
        Color foamLogTextColor = Color.Black;
        Color foamErrTextColor = Color.Red;
        int maxLogSize = 100000;
        public LflowMsgManager(RichTextBox richTextBox = null, TextBox textBox = null, ProgressBar progressBar = null)
        {
            this.richTextBox = richTextBox;
            this.textBox = textBox;
            this.progressBar = progressBar;
        }

        public RichTextBox RichTextBox { get => richTextBox; set => richTextBox = value; }
        public TextBox TextBox { get => textBox; set => textBox = value; }
        public ProgressBar ProgressBar { get => progressBar; set => progressBar = value; }
        public int MaxLogSize { get => maxLogSize; set => maxLogSize = value; }
        public Color LogTextColor { get => logTextColor; set => logTextColor = value; }
        public Color ErrTextColor { get => errTextColor; set => errTextColor = value; }
        public Color FoamLogTextColor { get => foamLogTextColor; set => foamLogTextColor = value; }
        public Color FoamErrTextColor { get => foamErrTextColor; set => foamErrTextColor = value; }

        public override void Processor(Message msg)
        {
            if (msg.Type == Message.MessageType.Error)
            {
                ProcessError(msg);
            }
            else if (msg.Type == Message.MessageType.Information)
            {
                ProcessInformation(msg);
            }
            else if (msg.Type == Message.MessageType.Progress)
            {
                ProcessProgress(msg);
            }
            if(msg.Type == Message.MessageType.FoamLog)
            {
                ProcessFoamLog(msg);
            }
            if(msg.Type == Message.MessageType.FoamError)
            {
                ProcessFoamError(msg);
            }
        }
        private void ProcessFoamLog(Message msg)
        {
            //AppendRichBoxText(msg.Msg + "\n", foamLogTextColor);
            AppendTextBoxText(msg.Msg + "\r\n");
        }

        private delegate void DgAppendTextBoxText(string txt);
        private void AppendTextBoxText(string text)
        {
            if (TextBox == null)
                return;
            if (TextBox.InvokeRequired)
            {
                DgAppendTextBoxText dg = new DgAppendTextBoxText(AppendTextBoxText);
                TextBox.Invoke(dg, new object[] { text });
            }
            else
            {
                string newtxt = TextBox.Text + text;
                if (newtxt.Length > maxLogSize)
                    newtxt = newtxt.Substring(newtxt.Length - maxLogSize);
                TextBox.Text = newtxt;
            }
        }
        private delegate void DgAppendRichBoxText(string txt, Color color);
        private void AppendRichBoxText(string text, Color color)
        {
            if (richTextBox == null)
                return;
            if(richTextBox.InvokeRequired)
            {
                DgAppendRichBoxText dg = new DgAppendRichBoxText(AppendRichBoxText);
                richTextBox.Invoke(dg, new object[] { text, color });
            }
            else
            {
                string newtxt = richTextBox.Text + text;
                if(text.Length > maxLogSize)
                {
                    text = text.Substring(text.Length - maxLogSize);
                    richTextBox.Clear();
                }
                else if (newtxt.Length > maxLogSize)
                {
                    int cutlen = newtxt.Length - maxLogSize;
                    richTextBox.SelectionStart = 0;
                    richTextBox.SelectionLength = cutlen;
                    richTextBox.SelectedText = "";
                }
                richTextBox.SelectionStart = richTextBox.TextLength;
                richTextBox.SelectionLength = 0;
                richTextBox.SelectionColor = color;
                richTextBox.AppendText(text);
            }
        }
        private void ProcessFoamError(Message msg)
        {
            //AppendRichBoxText(msg.Msg + "\n", foamErrTextColor);
            AppendTextBoxText(msg.Msg + "\r\n");
        }
        private void ProcessError(Message msg)
        {
            string err = msg.Msg.Replace("\n", "\n--");
            string txt = string.Format("[{0}][{1}]\n--{2}\n", "error", msg.Time, err);
            AppendRichBoxText(txt, ErrTextColor);
        }
        private void ProcessInformation(Message msg)
        {
            string err = msg.Msg.Replace("\n", "\n--");
            string txt = string.Format("[{0}][{1}]\n--{2}\n", "information", msg.Time, err);
            AppendRichBoxText(txt, LogTextColor);
        }
        private void ProcessProgress(Message msg)
        {
            if (progressBar == null)
                return;
            new Thread(() =>
            {
                Action action = () =>
                {
                    progressBar.Value = (int)(msg.Progress * (progressBar.Maximum - progressBar.Maximum));
                };
                progressBar.Invoke(action);
            }).Start();
        }
    }
}
