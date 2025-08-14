using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerAPI.DB
{
    public class Dumps
    {
        [Key]
        public int Id { get; set; }
        public int ErrorId { get; set; }
        public string ProcessName { get; set; }
        public int ProcessId { get; set; }
        public long MemoryUsage { get; set; }
        public byte[] DumpFile { get; set; }

        public virtual Errors Error { get; set; }
    }
}
