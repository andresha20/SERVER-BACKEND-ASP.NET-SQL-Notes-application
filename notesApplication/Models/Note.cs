namespace notesApplication.Models
{
    public class Note {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Boolean IsActive { get; set; }
        public Note() { 
            
        }
    }
}
