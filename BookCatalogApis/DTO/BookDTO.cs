using System.ComponentModel.DataAnnotations;

namespace BookCatalogApis.DTO;

public record BookDTO(
    int Id,
    int CategoryId,
    string Title,
    string Description,
    DateTime PublishDateUtc
);

public record CreateBookDTO(
    [Required] int CategoryId,
    [Required] string Title,
    [Required] string Description,
    DateTime PublishDateUtc
);

public record UpdateBookDTO(
    [Required] int CategoryId,
    [Required] string Title,
    [Required] string Description,
    DateTime PublishDateUtc
);