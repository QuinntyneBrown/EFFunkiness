using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFFunkiness.Server.Models
{
    public class Client
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public Guid CreatedByUserId { get; set; }
    }
}
