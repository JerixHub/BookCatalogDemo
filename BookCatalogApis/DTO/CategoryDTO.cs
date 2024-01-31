using System.ComponentModel.DataAnnotations;

namespace BookCatalogApis.DTO;

public record CategoryDTO(
    int Id,
    string Name
);

public record CreateCategoryDTO(
    [Required] int Name
);

public record UpdateCategoryDTO(
    [Required] int Name
);