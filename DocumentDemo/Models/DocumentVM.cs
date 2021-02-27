using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentDemo.Models
{
    public class DocumentListVM
    {
        public DocumentVM[] DocumentList { get; set; }
    }
    public class DocumentVM
    {
        public string DocumentName { get; set; }
        public string ContentType { get; set; }
        public string DocumentContent { get; set; }
    }
}
