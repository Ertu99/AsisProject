using System.ComponentModel.DataAnnotations;

namespace AsisProject.UrunModels
{
    public class Irsaliye
    {
        [Key]
        public int IrsaliyeId { get; set; }
        public int IrsaliyeNo { get; set; }
    }
}
