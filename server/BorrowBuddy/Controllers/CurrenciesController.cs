using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BorrowBuddy.Dto;
using BorrowBuddy.Models;
using BorrowBuddy.Models.Resources;
using BorrowBuddy.Services;
using Microsoft.AspNetCore.Mvc;

namespace BorrowBuddy.Controllers {
  [ApiController]
  [Route("api/[controller]")]
  public class CurrenciesController : ControllerBase {
    private readonly CurrencyService _currencyService;

    public CurrenciesController(CurrencyService currencyService) {
      _currencyService = currencyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Currency>>> GetCurrency() {
      return (await _currencyService.GetAsync()).Select(Mapper.Map).ToList();
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<Currency>> GetCurrency(string code) {
      var currency = await _currencyService.GetAsync(code);

      if(currency == null) {
        return NotFound();
      }

      return Mapper.Map(currency);
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> PutCurrency([FromRoute] string code, [FromBody] Currency model) {
      if (model.Code != code) {
        return BadRequest();
      }

      if(!await CurrencyExistsAsync(code)) {
        return NotFound();
      }

      var flow = _currencyService.UpdateAsync(code,
        new CurrencyDto {
          Code = model.Code,
          Scale = model.Scale,
          Symbol = model.Symbol
        });

      return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<Flow>> PostCurrency(Currency model) {
      var currency = await _currencyService.AddAsync(new CurrencyDto {
        Code = model.Code,
        Scale = model.Scale,
        Symbol = model.Symbol
      });

      return CreatedAtAction(nameof(GetCurrency), new { code = currency.Code }, Mapper.Map(currency));
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> DeleteCurrency(string code) {
      if(!await CurrencyExistsAsync(code)) {
        return NotFound();
      }

      await _currencyService.DeleteAsync(code);
      return NoContent();
    }

    private async Task<bool> CurrencyExistsAsync(string code) {
      return (await _currencyService.GetAsync(code)) != null;
    }
  }
}
