using System.ComponentModel.DataAnnotations;

namespace TASKHIVE.Model
{
    public enum addLabel
    {
        Urgent, 
        Bug, 
        Feature
    }
    public class Label
    {
        [Key]
        public int labelId { get; set; }

        [Required]
        public addLabel lableName { get; set; }

        public ICollection<WorkLabel> WorkLabels { get; set; }
    }
}
