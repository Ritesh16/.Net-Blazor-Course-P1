﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models.Dtos
{
    public class ErrorModelDto
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
