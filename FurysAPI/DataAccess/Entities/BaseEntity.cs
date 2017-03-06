using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurysAPI.DataAccess.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}