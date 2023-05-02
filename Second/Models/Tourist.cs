using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Documents;

namespace Second.Models
{
    public class Tourist
    {
        [Key]
        [Required]
        public int TouristId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        [InverseProperty(nameof(Models.TouristInfo.Tourist))]
        public virtual List<TouristInfo> TouristInfos { get; set; } = new List<TouristInfo>();

    }
}