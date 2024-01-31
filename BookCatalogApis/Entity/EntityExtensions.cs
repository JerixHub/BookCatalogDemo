using BookCatalogApis.DTO;
using BookCatalogApis.Models;

namespace BookCatalogApis.Entity;

public static class EntityExtensions
{
    public static BookDTO AsDto(this Book book)
    {
        return new BookDTO(
            book.Id,
            book.CategoryId,
            book.Title,
            book.Description,
            book.PublishDateUtc
        );
    }

    public static CategoryDTO AsDto(this Category category)
    {
        return new CategoryDTO(
            category.Id,
            category.Name
        );
    }
}
