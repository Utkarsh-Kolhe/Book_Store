﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Layer.Models
{
    public class UserLoginModel
    {
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
