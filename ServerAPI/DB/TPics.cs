using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerAPI.DB
{
    public class Pics
    {
        [Key]
        public int Id { get; set; }
        public int ErrorId { get; set; }
        public DateTime CreateTime { get; set; }
        public byte[] PicFile { get; set; }

        public virtual Errors Error { get; set; }
    }
}
