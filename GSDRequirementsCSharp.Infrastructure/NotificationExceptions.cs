using GSDRequirementsCSharp.Infrastructure.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSDRequirementsCSharp.Infrastructure
{
    public class NotificationException : Exception
    {
        private readonly string _title;
        public string Title { get { return _title; } }

        private readonly IEnumerable<string> _messages;
        public IEnumerable<string> Messages { get { return _messages; } }

        private readonly NoteType _noteType;
        public NoteType NoteType { get { return _noteType; } }

        public NotificationException(string title, IEnumerable<string> messages, NoteType noteType)
        {
            _title = title;
            _messages = messages;
            _noteType = noteType;
        }

        public NotificationException(string title, string message, NoteType noteType)
        {
            _title = title;
            _messages = new[] { message };
            _noteType = noteType;
        }
    }
}
