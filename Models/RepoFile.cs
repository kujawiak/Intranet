using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Models
{
    public class RepoFile
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Typ")]
        public string Extension { get; set; }
        [NotMappedAttribute]
        [Display(Name = "Pełna nazwa pliku")]
        public string FullName { 
            get {
                return String.Format("{0}.{1}", Name, Extension);
            } 
        }
        [Display(Name = "Nazwa wyświetlana")]
        public string ShownName { get; set; }
        public RepoDir Dir { get; set; }
        [Display(Name = "Rozmiar")]
        public int Size { get; set; }
        [NotMappedAttribute]
        public IFormFile File { set; get; }
        [Display(Name = "Wersja")]
        public int Version { get; set; }
        public Guid GUID { get; set; }
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
        [Display(Name = "Właściciel")]
        public string Owner { get; set; }
    }
}