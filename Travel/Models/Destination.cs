using System.ComponentModel.DataAnnotations;

namespace Travel.Models
{
  public class Destination
  {
    public int DestinationId { get; set; }
    [Required]
    [StringLength(85)]
    public string City { get; set; }
    [Required]
    [StringLength(50)]
    public string State { get; set; }
    [Required]
    [StringLength(56)]
    public string Country { get; set; }
    [Required]
    [StringLength(50)]
    public string VisitDate { get; set; }
    [Required]
    [Range(0,5, ErrorMessage = "Rating must be between 0 and 5.")]
    public string Rating { get; set; }
    [Required]
    [StringLength(200)]
    public string Review { get; set; }
    [Required]
    [StringLength(10)]
    public string UserName { get; set; }
  }
}