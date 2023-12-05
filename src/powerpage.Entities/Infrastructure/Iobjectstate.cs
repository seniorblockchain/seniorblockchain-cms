
using System.ComponentModel.DataAnnotations.Schema;

namespace powerpage.Entities;
     public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
 