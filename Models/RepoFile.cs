using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Intranet.Models
{
    public class RepoFile
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Extension { get; set; }
        [NotMappedAttribute]
        public string FullName { 
            get {
                return String.Format("{0}.{1}", Name, Extension);
            } 
        }
        public string ShownName { get; set; }
        public Guid UUID { get; set; }
        public string Location { get; set; }
        public int Size { get; set; }
        [NotMappedAttribute]
        public IFormFile File { set; get; }
        public int Version { get; set; }
        public Guid ParentUUID { get; set; }
    }
}