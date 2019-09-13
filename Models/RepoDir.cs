using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Models
{
    public class RepoDir
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        //public RepoDir Dir { get; set; }
        public Guid GUID { get; set; }
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
        [Display(Name = "Właściciel")]
        public string Owner { get; set; }
        public ICollection<RepoFile> Files { get; set; }
    }
}