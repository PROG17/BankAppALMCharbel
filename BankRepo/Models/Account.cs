﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BankRepo.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
    }
}