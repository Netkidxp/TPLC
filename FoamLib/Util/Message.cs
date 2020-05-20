using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoamLib.Util
{
    public class Message
    {
        public enum MessageType
        {
            Error,
            Information,
            Progress,
            FoamLog,
            FoamError
        }
        MessageType type;
        Object sender = null;
        DateTime time = DateTime.Now;
        string msg = "";
        float progress = 0.0f;
        Object data = null;

        public MessageType Type { get => type; set => type = value; }
        public object Sender { get => sender; set => sender = value; }
        public DateTime Time { get => time; set => time = value; }
        public string Msg { get => msg; set => msg = value; }
        public float Progress { get => progress; set => progress = value; }
        public object Data { get => data; set => data = value; }

        public Message(MessageType type)
        {
            this.Type = type;
        }
        public static Message FoamErrorMessage(string msg, Object sender = null, Object data = null)
        {
            Message m = new Message(MessageType.FoamError);
            m.msg = msg;
            m.sender = sender;
            m.data = data;
            m.time = DateTime.Now;
            return m;
        }
        public static Message FoamLogMessage(string msg, Object sender = null, Object data = null)
        {
            Message m = new Message(MessageType.FoamLog);
            m.msg = msg;
            m.sender = sender;
            m.data = data;
            m.time = DateTime.Now;
            return m;
        }
        public static Message ErrorMessage(string msg, Object sender = null, Object data = null)
        {
            Message m = new Message(MessageType.Error);
            m.msg = msg;
            m.sender = sender;
            m.data = data;
            m.time = DateTime.Now;
            return m;
        }
        public static Message InformationMessage(string msg, Object sender = null, Object data = null)
        {
            Message m = new Message(MessageType.Information);
            m.msg = msg;
            m.sender = sender;
            m.data = data;
            m.time = DateTime.Now;
            return m;
        }
        public static Message ProgressMessage(float progress, Object sender = null, Object data = null)
        {
            Message m = new Message(MessageType.Progress);
            m.progress = progress;
            m.sender = sender;
            m.data = data;
            m.time = DateTime.Now;
            return m;
        }

    }
}
