using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NamesRecommender.Models
{
    public class NameDetail
    {
        public int NameDetailId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50)]

        public string NameText { get; set; }

        //[DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Meaning for the name is required.")]
        [MaxLength(250)]
        public string Meaning { get; set; }

        //[DataType(DataType.MultilineText)]

        [Required(ErrorMessage = "Please provide some information for the name.")]

        [MaxLength(250)]
        public string NamesInfo { get; set; }

        [Timestamp]
        public byte[] stamp { get; set; }

        public int NameGenderId { get; set; }
        public int NameCategoryId { get; set; }
        public int NameTypeId { get; set; }
        public int NameOriginId { get; set; }
        public int NameLengthId { get; set; }
        public virtual NameGender gender { get; set; }
        public virtual NameCategory category { get; set; }
        public virtual NameType type { get; set; }
        public virtual NameOrigin origin { get; set; }
        public virtual NameLength length { get; set; }

    }
}