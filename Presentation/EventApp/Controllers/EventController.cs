using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Application.Repositories;
using EventApp.Domain.Entities;
using EventApp.Domain.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventApp
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventReadRepository _eventReadRepository;
        private readonly IEventWriteRepository _eventWriteRepository;
        private readonly ICompanyReadRepository _companyReadRepository;

        public EventController(
            IEventReadRepository eventReadRepository,
            IEventWriteRepository eventWriteRepository,
            ICompanyReadRepository companyReadRepository)
        {
            _eventReadRepository = eventReadRepository;
            _eventWriteRepository = eventWriteRepository;
            _companyReadRepository = companyReadRepository;
        }

        /// <summary>
        /// Tüm etkinlikleri listeler
        /// </summary>
        /// <returns>Etkinlik listesi</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Event>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var events = _eventReadRepository.GetAll(false).ToList();
            return Ok(events);
        }

        /// <summary>
        /// Belirli bir etkinliği ID'ye göre getirir
        /// </summary>
        /// <param name="id">Etkinlik ID</param>
        /// <returns>Etkinlik detayları</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            var @event = await _eventReadRepository.GetByIdAsync(id);
            if (@event == null)
                return NotFound($"Etkinlik bulunamadı: {id}");
                
            return Ok(@event);
        }

        /// <summary>
        /// Bir şirkete ait tüm etkinlikleri listeler
        /// </summary>
        /// <param name="companyId">Şirket ID</param>
        /// <returns>Etkinlik listesi</returns>
        [HttpGet("company/{companyId}")]
        [ProducesResponseType(typeof(List<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCompanyId(string companyId)
        {
            // Önce şirketin var olup olmadığını kontrol et
            var company = await _companyReadRepository.GetByIdAsync(companyId);
            if (company == null)
                return NotFound($"Şirket bulunamadı: {companyId}");

            var companyGuid = Guid.Parse(companyId);
            var events = _eventReadRepository.GetWhere(e => e.CompanyId == companyGuid, false).ToList();
            
            return Ok(events);
        }

        /// <summary>
        /// Yeni bir etkinlik ekler
        /// </summary>
        /// <param name="eventDto">Etkinlik bilgileri</param>
        /// <returns>Oluşturulan etkinlik</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Event), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] EventCreateDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Şirketin var olup olmadığını kontrol et
            var company = await _companyReadRepository.GetByIdAsync(eventDto.CompanyId.ToString());
            if (company == null)
                return NotFound($"Şirket bulunamadı: {eventDto.CompanyId}");

            // Başlangıç tarihi bitiş tarihinden önce olmalı
            if (eventDto.StartDate >= eventDto.EndDate)
                return BadRequest("Başlangıç tarihi bitiş tarihinden önce olmalıdır.");

            var @event = new Event
            {
                Id = Guid.NewGuid(),
                Title = eventDto.Title,
                Description = eventDto.Description,
                EventType = eventDto.EventType,
                Location = eventDto.Location,
                StartDate = eventDto.StartDate,
                EndDate = eventDto.EndDate,
                Capacity = eventDto.Capacity,
                CompanyId = eventDto.CompanyId
            };

            await _eventWriteRepository.AddAsync(@event);
            await _eventWriteRepository.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = @event.Id }, @event);
        }

        /// <summary>
        /// Mevcut bir etkinliği günceller
        /// </summary>
        /// <param name="id">Etkinlik ID</param>
        /// <param name="eventDto">Güncellenmiş etkinlik bilgileri</param>
        /// <returns>Güncellenen etkinlik</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string id, [FromBody] EventUpdateDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var @event = await _eventReadRepository.GetByIdAsync(id);
            if (@event == null)
                return NotFound($"Etkinlik bulunamadı: {id}");

            // Şirketin var olup olmadığını kontrol et
            if (eventDto.CompanyId != @event.CompanyId)
            {
                var company = await _companyReadRepository.GetByIdAsync(eventDto.CompanyId.ToString());
                if (company == null)
                    return NotFound($"Şirket bulunamadı: {eventDto.CompanyId}");
            }

            // Başlangıç tarihi bitiş tarihinden önce olmalı
            if (eventDto.StartDate >= eventDto.EndDate)
                return BadRequest("Başlangıç tarihi bitiş tarihinden önce olmalıdır.");

            @event.Title = eventDto.Title;
            @event.Description = eventDto.Description;
            @event.EventType = eventDto.EventType;
            @event.Location = eventDto.Location;
            @event.StartDate = eventDto.StartDate;
            @event.EndDate = eventDto.EndDate;
            @event.Capacity = eventDto.Capacity;
            @event.CompanyId = eventDto.CompanyId;

            _eventWriteRepository.Update(@event);
            await _eventWriteRepository.SaveAsync();

            return Ok(@event);
        }

        /// <summary>
        /// Bir etkinliği siler
        /// </summary>
        /// <param name="id">Silinecek etkinlik ID</param>
        /// <returns>İşlem sonucu</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var @event = await _eventReadRepository.GetByIdAsync(id);
            if (@event == null)
                return NotFound($"Etkinlik bulunamadı: {id}");

            _eventWriteRepository.Remove(@event);
            await _eventWriteRepository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Test amaçlı örnek etkinlikler ekler
        /// </summary>
        [HttpPost("seed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SeedSampleData(string companyId)
        {
            // Şirketin var olup olmadığını kontrol et
            var company = await _companyReadRepository.GetByIdAsync(companyId);
            if (company == null)
                return NotFound($"Şirket bulunamadı: {companyId}");

            var companyGuid = Guid.Parse(companyId);
            var now = DateTime.UtcNow;

            await _eventWriteRepository.AddRangeAsync(new() {
                new() { 
                    Id = Guid.NewGuid(),
                    Title = "Düğün Organizasyonu",
                    Description = "Örnek düğün organizasyonu",
                    EventType = EventType.Wedding,
                    Location = "İstanbul",
                    StartDate = now.AddDays(30),
                    EndDate = now.AddDays(30).AddHours(8),
                    Capacity = 200,
                    CompanyId = companyGuid
                },
                new() { 
                    Id = Guid.NewGuid(),
                    Title = "Nişan Töreni",
                    Description = "Örnek nişan töreni",
                    EventType = EventType.Engagement,
                    Location = "Ankara",
                    StartDate = now.AddDays(15),
                    EndDate = now.AddDays(15).AddHours(5),
                    Capacity = 100,
                    CompanyId = companyGuid
                },
                new() { 
                    Id = Guid.NewGuid(),
                    Title = "Kına Gecesi",
                    Description = "Örnek kına gecesi",
                    EventType = EventType.Henna,
                    Location = "İzmir",
                    StartDate = now.AddDays(29),
                    EndDate = now.AddDays(29).AddHours(6),
                    Capacity = 150,
                    CompanyId = companyGuid
                },
             }
            );
            await _eventWriteRepository.SaveAsync();
            
            return Ok("Örnek etkinlikler eklendi");
        }
    }

    /// <summary>
    /// Etkinlik oluşturma için DTO
    /// </summary>
    public class EventCreateDto
    {
        /// <summary>
        /// Etkinlik başlığı
        /// </summary>
        public required string Title { get; set; }
        
        /// <summary>
        /// Etkinlik açıklaması
        /// </summary>
        public required string Description { get; set; }
        
        /// <summary>
        /// Etkinlik türü
        /// </summary>
        public EventType EventType { get; set; }
        
        /// <summary>
        /// Etkinlik lokasyonu
        /// </summary>
        public required string Location { get; set; }
        
        /// <summary>
        /// Başlangıç tarihi
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Bitiş tarihi
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// Kapasite
        /// </summary>
        public int Capacity { get; set; }
        
        /// <summary>
        /// Şirket ID
        /// </summary>
        public Guid CompanyId { get; set; }
    }

    /// <summary>
    /// Etkinlik güncelleme için DTO
    /// </summary>
    public class EventUpdateDto
    {
        /// <summary>
        /// Etkinlik başlığı
        /// </summary>
        public required string Title { get; set; }
        
        /// <summary>
        /// Etkinlik açıklaması
        /// </summary>
        public required string Description { get; set; }
        
        /// <summary>
        /// Etkinlik türü
        /// </summary>
        public EventType EventType { get; set; }
        
        /// <summary>
        /// Etkinlik lokasyonu
        /// </summary>
        public required string Location { get; set; }
        
        /// <summary>
        /// Başlangıç tarihi
        /// </summary>
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Bitiş tarihi
        /// </summary>
        public DateTime EndDate { get; set; }
        
        /// <summary>
        /// Kapasite
        /// </summary>
        public int Capacity { get; set; }
        
        /// <summary>
        /// Şirket ID
        /// </summary>
        public Guid CompanyId { get; set; }
    }
}
