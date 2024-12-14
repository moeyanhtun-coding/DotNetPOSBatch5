using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPOS.Domain.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public bool IsError => !IsSuccess;

        public bool IsValidationError { get { return Type == EnumRespType.ValidationError; } }

        private EnumRespType Type { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public static Result<T> Success(T data, string message = "Success")
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Type = EnumRespType.Success,
                Data = data,
                Message = message
            };
        }

        public static Result<T> NoProductFound(string message)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Message = message
            };
        }


    }
}
