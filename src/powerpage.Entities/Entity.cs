using System;
using System.ComponentModel.DataAnnotations.Schema;
using powerpage.Entities;


namespace powerpage.Entities;
public abstract class Entity : IObjectState
{
    [NotMapped]
    public ObjectState ObjectState { get; set; }

    public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.UtcNow;


}
