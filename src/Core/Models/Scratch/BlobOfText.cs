using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Scratch
{
    [Table("BlobOfText", Schema = "scratch")]
    public class BlobOfText
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }

    }
}