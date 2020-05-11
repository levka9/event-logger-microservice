using System;
using System.Collections.Generic;

namespace EventLogger.Entities
{
    public partial class EventType
    {
        public EventType()
        {
            Event = new HashSet<Event>();
        }

        public byte Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
