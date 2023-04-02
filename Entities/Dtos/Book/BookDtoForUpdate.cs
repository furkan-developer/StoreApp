using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Book
{
    public record BookDtoForUpdate: BookDtoForManupilation
    {
        public int Id { get; set; }
    }
}
