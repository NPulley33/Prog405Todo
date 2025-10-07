using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Todo.Common
{
    //results can either be OK or Error
    //need to accomodate return data
    
    public class Result
    {
        private bool ok;
        private string error;


        public bool IsErr()
        {
            if (this.ok) return false;
            return true;
        }
        public bool IsOk()
        {
            if (!this.ok) return false;
            return true;
        }

        public string GetErr()
        {
            return this.error;
        }

        private Result()
        { 
            this.ok = true;
            this.error = string.Empty;
        }
        private Result(string error)
        {
            this.ok = false;
            this.error = error;
        }

        public static Result Ok()
        { 
            return new Result();
        }

        public static Result Err(string error)
        {
            return new Result(error);
        }
    }

    public class Result<T> where T : class //sine nullable needs to be a reference type
    {
        private bool ok;
        private string Error;
        private T? Value;
        public bool IsErr()
        {
            if (this.ok) return false;
            return true;
        }

        public bool IsOk()
        {
            if (!this.ok) return false;
            return true;
        }

        public string GetErr()
        {
            return this.Error;
        }
        public T? GetVal()
        {
            return this.Value;
        }

        private Result(T val)
        {
            this.Value = val;
            this.ok = true;
            this.Error = string.Empty;
        }
        private Result(string error)
        {
            this.Value = null;
            this.ok = false;
            this.Error = error;
        }

        public static Result<T> Ok(T val)
        {
            return new Result<T>(val);
        }

        public static Result<T> Err(string error)
        {
            return new Result<T>(error);
        }
    }
}
