using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Dtos
{
    public class UplaodFileDto
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string? Url { get; set; }

        public UplaodFileDto (bool isSuccess, string message, string url = " ")
        {
            IsSuccess = isSuccess;
            Message = message;
            Url = url;
        }
    }
}
