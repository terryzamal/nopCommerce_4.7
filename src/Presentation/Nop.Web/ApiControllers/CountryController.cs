using Microsoft.AspNetCore.Mvc;
using Nop.Web.Factories;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Web.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public partial class CountryController : ControllerBase
{
    #region Fields

    protected readonly ICountryModelFactory _countryModelFactory;
        
    #endregion

    #region Ctor

    public CountryController(ICountryModelFactory countryModelFactory)
    {
        _countryModelFactory = countryModelFactory;
    }

    #endregion

    #region States / provinces

    //available even when navigation is not allowed
    [CheckAccessPublicStore(ignore: true)]
    //ignore SEO friendly URLs checks
    [CheckLanguageSeoCode(ignore: true)]
    [HttpGet("GetStatesByCountryId/{countryId}/{addSelectStateItem}")]
    public virtual async Task<IActionResult> GetStatesByCountryId(int countryId, bool addSelectStateItem)
    {
        var model = await _countryModelFactory.GetStatesByCountryIdAsync(countryId, addSelectStateItem);

        return Ok(model);
    }

    #endregion
}