using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Caching;
using Nop.Core;
using Nop.Plugin.Widgets.NivoSlider.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Plugin.Widgets.NivoSlider.Infrastructure.Cache;

namespace Nop.Plugin.Widgets.NivoSlider.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WidgetsNivoSliderApiController : ControllerBase
{
    protected readonly IStoreContext _storeContext;
    protected readonly IStaticCacheManager _staticCacheManager;
    protected readonly ISettingService _settingService;
    protected readonly IPictureService _pictureService;
    protected readonly IWebHelper _webHelper;

    public WidgetsNivoSliderApiController(IStoreContext storeContext,
        IStaticCacheManager staticCacheManager,
        ISettingService settingService,
        IPictureService pictureService,
        IWebHelper webHelper)
    {
        _storeContext = storeContext;
        _staticCacheManager = staticCacheManager;
        _settingService = settingService;
        _pictureService = pictureService;
        _webHelper = webHelper;
    }

    [HttpGet("GetSlider")]
    public async Task<IActionResult> GetSlider()
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        var nivoSliderSettings = await _settingService.LoadSettingAsync<NivoSliderSettings>(store.Id);

        var model = new PublicInfoModel
        {
            Picture1Url = await GetPictureUrlAsync(nivoSliderSettings.Picture1Id),
            Text1 = nivoSliderSettings.Text1,
            Link1 = nivoSliderSettings.Link1,
            AltText1 = nivoSliderSettings.AltText1,

            Picture2Url = await GetPictureUrlAsync(nivoSliderSettings.Picture2Id),
            Text2 = nivoSliderSettings.Text2,
            Link2 = nivoSliderSettings.Link2,
            AltText2 = nivoSliderSettings.AltText2,

            Picture3Url = await GetPictureUrlAsync(nivoSliderSettings.Picture3Id),
            Text3 = nivoSliderSettings.Text3,
            Link3 = nivoSliderSettings.Link3,
            AltText3 = nivoSliderSettings.AltText3,

            Picture4Url = await GetPictureUrlAsync(nivoSliderSettings.Picture4Id),
            Text4 = nivoSliderSettings.Text4,
            Link4 = nivoSliderSettings.Link4,
            AltText4 = nivoSliderSettings.AltText4,

            Picture5Url = await GetPictureUrlAsync(nivoSliderSettings.Picture5Id),
            Text5 = nivoSliderSettings.Text5,
            Link5 = nivoSliderSettings.Link5,
            AltText5 = nivoSliderSettings.AltText5
        };

        if (string.IsNullOrEmpty(model.Picture1Url) && string.IsNullOrEmpty(model.Picture2Url) &&
            string.IsNullOrEmpty(model.Picture3Url) && string.IsNullOrEmpty(model.Picture4Url) &&
            string.IsNullOrEmpty(model.Picture5Url))
            //no pictures uploaded
            return NotFound("");

        return Ok(model);
    }

    /// <returns>A task that represents the asynchronous operation</returns>
    protected async Task<string> GetPictureUrlAsync(int pictureId)
    {
        var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY,
            pictureId, _webHelper.IsCurrentConnectionSecured() ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);

        return await _staticCacheManager.GetAsync(cacheKey, async () =>
        {
            //little hack here. nulls aren't cacheable so set it to ""
            var url = await _pictureService.GetPictureUrlAsync(pictureId, showDefaultPicture: false) ?? "";
            return url;
        });
    }

}
