﻿using Microsoft.AspNetCore.Http;
using shopecommerce.Domain.Commons.Commands;
using shopecommerce.Domain.Models;

namespace shopecommerce.Application.Commands.ProductCommand.AddImageProduct
{
    public class AddImageProductCommand : CommandBase<BaseResponseDto>
    {
        public List<IFormFile> file_image { get; set; }
    }
}
