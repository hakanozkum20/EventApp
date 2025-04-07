using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly ICompanyWriteRepository _companyWriteRepository;

        public CompanyController(ICompanyReadRepository companyReadRepository, ICompanyWriteRepository companyWriteRepository)
        {
            _companyReadRepository = companyReadRepository;
            _companyWriteRepository = companyWriteRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            await _companyWriteRepository.AddRangeAsync(new() {
                new() { Name = "Company 1" , Id = Guid.NewGuid()},
                new() { Name = "Company 2" , Id = Guid.NewGuid()},
                new() { Name = "Company 3" , Id = Guid.NewGuid()},
             }
            );
            await _companyWriteRepository.SaveAsync();
            
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _companyReadRepository.GetByIdAsync(id));
        }
    }
}