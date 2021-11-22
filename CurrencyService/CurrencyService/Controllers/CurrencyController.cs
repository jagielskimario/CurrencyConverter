using CurrencyService.Api;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CurrencyService.Controllers
{
    [ApiController]
    public class CurrencyController : ControllerBase
    {

        [HttpGet]
        [Route("api/getcurrencyword/{currency}")]
        public string GetCurrencyWord(string currency)
        {
            try
            {
                return CurrencyConverter.ConvertNumbersIntoWords(currency);
            }
            catch (Exception)
            {
                //log ex
                return string.Empty;
            }
        }
    }
}
