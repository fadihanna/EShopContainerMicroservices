using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Application.Dtos
{
    public record ProviderDto(
       int Id,
       string NameEN,
       string NameAR,
       bool IsActive
        );
}
