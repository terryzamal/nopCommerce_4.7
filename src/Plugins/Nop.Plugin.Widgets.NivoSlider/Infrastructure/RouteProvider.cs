using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Infrastructure;

namespace Nop.Plugin.Widgets.NivoSlider.Infrastructure;
public class RouteProvider : BaseRouteProvider, IRouteProvider
{
    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllerRoute(NivoSliderDefaults.NivoSliderRoute,
            "api/WidgetsNivoSlider/GetSlider",
            new { controller = "WidgetsNivoSliderApi", action = "GetSlider" });
    }
    public int Priority => 1;
}
