using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NamesRecommender.Models
{
    public class Details
    {
        public int DetailsId { get; set; }


        public string? SubmittedBy { get; set; }

        public int? ThumbsUp { get; set; }
        public int? ThumbDown { get; set; }


    }
}