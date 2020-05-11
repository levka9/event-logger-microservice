using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventLogger.Entities
{
    public partial class EventDetails
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public string TagName { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Event Event { get; set; }
    }
}
