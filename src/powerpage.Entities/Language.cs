using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace powerpage.Entities;

public class Language : Entity
{
    public Language()
    {
        StringResources = new HashSet<StringResource>();
    }

    [HiddenInput]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    [Required]
    public string Culture { get; set; }


    public virtual ICollection<StringResource> StringResources { get; set; }

}
