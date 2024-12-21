using Miwen.Linq.Dynamic.Queryable;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Miwen.Abp.Dynamic.Queryable;

public class GetListByDynamicQueryableInput : PagedAndSortedResultRequestDto
{
    [Required]
    public DynamicQueryable Queryable { get; set; }
}
