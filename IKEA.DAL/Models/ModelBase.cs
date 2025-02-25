namespace IKEA.DAL.Models
{
    public class ModelBase
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModeficationBy { get; set; }
        public DateTime LastModeficationOn { get; set; }


    }
}
