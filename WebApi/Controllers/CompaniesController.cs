using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly CompniesServer _service = new();

    [HttpGet]
    public async Task<ActionResult<List<Company>>> GetCompanies()
    {
        var result = await _service.GetCompanies();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetCompanyById(int id)
    {
        var result = await _service.GetCompaniebyId(id);

        if (result == null)
        {
            return NotFound("Company not found");
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> AddCompany(Company company)
    {
        var result = await _service.AddCompany(company);

        if (!result)
        {
            return BadRequest("Company not added");
        }

        return Ok("Company added");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCompany(int id, Company company)
    {
        company.Id = id;

        var result = await _service.UpdateCompany(company);

        if (!result)
        {
            return BadRequest("Company not updated");
        }

        return Ok("Company updated");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCompany(int id)
    {
        var result = await _service.DeletCompany(id);

        if (!result)
        {
            return NotFound("Company not found");
        }

        return Ok("Company deleted");
    }
}