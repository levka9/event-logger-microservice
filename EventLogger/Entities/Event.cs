using System;
using System.Collections.Generic;

namespace EventLogger.Entities
{
    public partial class Event
    {
        public Event()
        {
            EventDetails = new HashSet<EventDetails>();
        }

        public long Id { get; set; }
        public byte EventTypeId { get; set; }
        public string AppicationName { get; set; }
        public string UserId { get; set; }
        public string ProccessId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual EventType EventType { get; set; }
        public virtual ICollection<EventDetails> EventDetails { get; set; }
    }
}
