﻿using System.ComponentModel.DataAnnotations;

namespace WebBookStore.ViewModels
{
    public class UserVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
