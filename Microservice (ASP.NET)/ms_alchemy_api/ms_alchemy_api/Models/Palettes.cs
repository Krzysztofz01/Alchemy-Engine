using System;
using System.Collections.Generic;

namespace ms_alchemy_api.Models
{
    public partial class Palettes
    {
        public int Id { get; set; }
        public string Base { get; set; }
        public string ColorArray { get; set; }
        public bool? Display { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
