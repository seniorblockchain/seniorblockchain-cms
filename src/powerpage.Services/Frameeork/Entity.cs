using System.ComponentModel.DataAnnotations.Schema;
using powerpage.Entities;
using powerpage.Services;


namespace powerpage.Services;
public abstract class Entity : IObjectState
{
    [NotMapped]
    public ObjectState ObjectState { get; set; }
}
