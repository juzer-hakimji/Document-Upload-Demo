using System;
using System.Collections.Generic;

#nullable disable

namespace DocumentDemo.Models
{
    public partial class Document
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public byte[] DocumentContent { get; set; }
        public string ContentType { get; set; }
    }
}
