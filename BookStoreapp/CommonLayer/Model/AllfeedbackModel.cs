using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class AllfeedbackModel
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }


    }
}
