using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaApp.API.Entities
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
            => CreatedOn = DateTime.Today;
        

        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedOn{ get; }
        public DateTime? UpdatedOn { get; set; }
    }
}