using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Application.Repositories;
using EventApp.Application.ViewModels.Companies;
using EventApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventApp
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

        /// <summary>
        /// Tüm şirketleri listeler
        /// </summary>
        /// <returns>Şirket listesi</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Company>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var companies = _companyReadRepository.GetAll(false).ToList();
            return Ok(companies);
        }

        /// <summary>
        /// Belirli bir şirketi ID'ye göre getirir
        /// </summary>
        /// <param name="id">Şirket ID</param>
        /// <returns>Şirket detayları</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            var company = await _companyReadRepository.GetByIdAsync(id);
            if (company == null)
                return NotFound($"Şirket bulunamadı: {id}");

            return Ok(company);
        }

        /// <summary>
        /// Yeni bir şirket ekler
        /// </summary>
        /// <param name="model">Şirket bilgileri</param>
        /// <returns>Oluşturulan şirket</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Company), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] VM_Create_Company model)
        {
            // ModelState kontrolü artık ValidationFilter tarafından otomatik olarak yapılıyor
            // Fluent Validation ile doğrulama yapılıyor

            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                CustomerId = model.CustomerId
            };

            await _companyWriteRepository.AddAsync(company);
            await _companyWriteRepository.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
        }

        /// <summary>
        /// Mevcut bir şirketi günceller
        /// </summary>
        /// <param name="id">Şirket ID</param>
        /// <param name="model">Güncellenmiş şirket bilgileri</param>
        /// <returns>Güncellenen şirket</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string id, [FromBody] VM_Update_Company model)
        {
            // ModelState kontrolü artık ValidationFilter tarafından otomatik olarak yapılıyor
            // Fluent Validation ile doğrulama yapılıyor

            var company = await _companyReadRepository.GetByIdAsync(id);
            if (company == null)
                return NotFound($"Şirket bulunamadı: {id}");

            company.Name = model.Name;
            company.CustomerId = model.CustomerId;

            _companyWriteRepository.Update(company);
            await _companyWriteRepository.SaveAsync();

            return Ok(company);
        }

        /// <summary>
        /// Bir şirketi siler
        /// </summary>
        /// <param name="id">Silinecek şirket ID</param>
        /// <returns>İşlem sonucu</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var company = await _companyReadRepository.GetByIdAsync(id);
            if (company == null)
                return NotFound($"Şirket bulunamadı: {id}");

            _companyWriteRepository.Remove(company);
            await _companyWriteRepository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Test amaçlı örnek şirketler ekler
        /// </summary>
        [HttpPost("seed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SeedSampleData()
        {
            await _companyWriteRepository.AddRangeAsync(new() {
                new() { Name = "Company 1" , Id = Guid.NewGuid()},
                new() { Name = "Company 2" , Id = Guid.NewGuid()},
                new() { Name = "Company 3" , Id = Guid.NewGuid()},
             }
            );
            await _companyWriteRepository.SaveAsync();

            return Ok("Örnek şirketler eklendi");
        }
    }
}