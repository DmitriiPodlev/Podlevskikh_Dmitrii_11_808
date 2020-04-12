using Microsoft.EntityFrameworkCore;
using System;

namespace Vk_MVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);


    }
    
}
