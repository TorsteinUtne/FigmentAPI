using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerService.Data.Attributes;
using PowerService.Util;

namespace PowerService.Data.Models.RequestResponseObjects
{
    public class CaseRequest
    {
        [SwaggerIgnore]
        [DoNotPatch]
        public Guid Id { get; set; }
        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 chars length", MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 chars ")]
        public string Description { get; set; }
       

    
        public async Task<ActionResult<CaseRequest>> GetRequest(Guid id, PowerServiceContext context)
        {
            var cCase = await context.Cases.FindAsync(id);
            if (cCase == null)
                return null;
            var request = new CaseRequest
            {
                Id = id,
                Name = cCase.Name,
                Description = cCase.Description,
            };
            return request;
        }
    }
}
