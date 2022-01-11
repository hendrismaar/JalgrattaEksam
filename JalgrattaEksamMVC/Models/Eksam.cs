using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JalgrattaEksamMVC.Models
{
    public class Eksam
    {
        public int Id { get; set; }
        [StringLength(64)]
        [Required]
        public string Eesnimi { get; set; }
        [StringLength(64)]
        [Required]
        public string Perenimi { get; set; }
        [Range(-1, 10)]
        public int Teooria { get; set; }
        public int Slaalom { get; set; }
        public int Ringtee { get; set; }
        public int Tänav { get; set; }
        public int Luba { get; set; }
    }
}
