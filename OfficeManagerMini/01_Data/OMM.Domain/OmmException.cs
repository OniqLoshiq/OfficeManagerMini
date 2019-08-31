using System;

namespace OMM.Domain
{
    public class OmmException
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public string ExceptionType { get; set; }

        public string ExceptionMessage { get; set; }

        public string CallingMethod { get; set; }

        public DateTime ExceptionDate { get; set; }
    }
}
