using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotnetCoreMongoDbSample.Models.ViewModels
{
    public class NameValueCreateViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
