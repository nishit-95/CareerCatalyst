﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CareerCatalyst.Models
{
    public class UserLoginMV
    {
        [Required(ErrorMessage="Required*")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string Password { get; set; }
    }
}