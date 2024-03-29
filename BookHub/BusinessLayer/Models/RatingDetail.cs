namespace BusinessLayer.Models;

public class RatingDetail
{
    public required int Id { get; set; }
    public required ModelRelated User { get; init; }
    public required ModelRelated Book { get; init; }
    public int Value { get; init; }
    public string? Comment { get; init; }
}