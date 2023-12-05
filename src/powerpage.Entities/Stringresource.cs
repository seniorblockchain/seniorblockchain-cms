using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#nullable disable

namespace powerpage.Entities;
     public class StringResource : Entity
    {
        [HiddenInput]
        public int Id { get; set; }
        public int? LanguageId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual Language Language { get; set; }
    }
 