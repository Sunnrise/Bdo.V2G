﻿using Volo.Abp.Bundling;

namespace Bdo.V2G.Blazor.WebApp.Client;

/* Add your global styles/scripts here.
 * See https://docs.abp.io/en/abp/latest/UI/Blazor/Global-Scripts-Styles to learn how to use it
 */
public class V2GBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
