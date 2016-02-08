using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        //internal code, unique indetificator
          [Required]
        public string IntCode { get; set; }

         [Required]
        public string ValueType { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Value { get; set; }

        public string AlternativeValue { get; set; }

        public int? ParentSettingId { get; set; }

        public virtual Setting  ParentSetting { get; set; }

    }
}
