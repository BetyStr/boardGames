using BusinessLayer.Errors;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IPublisherService
{ 
    Task<IEnumerable<PublisherDetail>> GetPublishersAsync(string? name);
    Task<IEnumerable<PublisherDetail>> GetSearchPublishersAsync(string? query);
    Task<Result<PublisherDetail, (Error err, string message)>> GetPublisherByIdAsync(int id);
    Task<PublisherDetail> CreatePublisherAsync(PublisherCreate publisherCreate);
    Task<Result<PublisherDetail, (Error err, string message)>> UpdatePublisherAsync(int id, PublisherUpdate publisherUpdate);
    Task<Result<bool, (Error err, string message)>> DeletePublisherAsync(int id);
}