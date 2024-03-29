namespace BusinessLayer.Models;

public class RatingCreate
{
    public required ModelRelated User { get; set; }
    public required ModelRelated Book { get; set; }
    public int Value { get; set; }
    public string? Comment { get; set; }
}