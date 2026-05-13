using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController
{
    private readonly CompaniesService _service;

    public CompaniesController(CompaniesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Company>>> GetCompanies()
    {
        return await _service.GetCompanies();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetCompanyById(int id)
    {
        var result = await _service.GetCompaniebyId(id);

        return result;
    }

    [HttpPost]
    public async Task<bool> AddCompany(Company company)
    {
        return await _service.AddCompany(company);
    }

    [HttpPut("{id}")]
    public async Task<bool> UpdateCompany(int id, Company company)
    {
        company.Id = id;

        return await _service.UpdateCompany(company);
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteCompany(int id)
    {
        return await _service.DeletCompany(id);
    }
}