﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Layer.Models
{
    public class BookModel
    {
        public string BookName { get; set; }

        public string Author { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}
