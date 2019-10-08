using System;
using System.Collections.Generic;

namespace SpacePortal.Application.Models
{
    public class CoreResult<T> where T: class
    {
        private bool v;

        public List<T> TObjects { get; set; }
        public T Object { get; set; }

        public Error Err { get; private set; }
        public class Error
        {
            public Exception Exception { get; set; }
            public string Message { get; set; }
        }
        public bool Successful { get; private set; }

        internal CoreResult<T> AddResult(bool result)
        {
            return new CoreResult<T> {
                Successful = result
            };
        }

        internal CoreResult<T> AddError(Exception ex)
        {
            return new CoreResult<T>
            {
                Err = new Error
                {
                    Exception = ex,
                    Message = ex.Message
                }
            };
        }
    }
}
