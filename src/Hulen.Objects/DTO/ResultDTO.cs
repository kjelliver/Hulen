﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.Objects.DTO
{
    public class ResultDTO
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public string Comment { get; set; }
        public int UsedBudget { get; set; }
    }
}
