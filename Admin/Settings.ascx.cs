// --- Copyright (c) notice NevoWeb ---
//  Copyright (c) 2014 SARL NevoWeb.  www.nevoweb.com. The MIT License (MIT).
// Author: D.C.Lee
// ------------------------------------------------------------------------
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// ------------------------------------------------------------------------
// This copyright notice may NOT be removed, obscured or modified without written consent from the author.
// --- End copyright notice --- 

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using NBrightCore.common;
using NBrightCore.providers;
using NBrightCore.render;
using NBrightDNN;

using Nevoweb.DNN.NBrightBuy.Base;
using Nevoweb.DNN.NBrightBuy.Components;
using DataProvider = DotNetNuke.Data.DataProvider;

namespace Nevoweb.DNN.NBrightBuy.Admin
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The ViewNBrightGen class displays the content
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : NBrightBuyAdminBase
    {


        #region Event Handlers


        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                base.OnInit(e);
                // inject any pageheader we need
                var nbi = new NBrightInfo();
                nbi.Lang = Utils.GetCurrentCulture();
                nbi.PortalId = PortalId;

                var pageheaderTempl = NBrightBuyUtils.RazorTemplRender("Admin_Settings_head.cshtml", 0, "", nbi, "/DesktopModules/NBright/NBrightBuy", "config", Utils.GetCurrentCulture(), StoreSettings.Current.Settings());
                PageIncludes.IncludeTextInHeader(Page, pageheaderTempl);
            }
            catch (Exception exc)
            {
                //display the error on the template (don;t want to log it here, prefer to deal with errors directly.)
                var l = new Literal();
                l.Text = exc.ToString();
                phData.Controls.Add(l);
            }

        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                if (Page.IsPostBack == false)
                {
                    PageLoad();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                //display the error on the template (don;t want to log it here, prefer to deal with errors directly.)
                var l = new Literal();
                l.Text = exc.ToString();
                phData.Controls.Add(l);
            }
        }

        private void PageLoad()
        {

            if (NBrightBuyUtils.CheckRights()) // limit module data to NBS security roles
            {
                var settings = ModCtrl.GetPluginSinglePageData("NBrightBuySettings", "SETTINGS",Utils.GetCurrentCulture());

                RazorTemplate = "Admin_Settings.cshtml";

                var strOut = NBrightBuyUtils.RazorTemplRender(RazorTemplate, 0, "", settings, "/DesktopModules/NBright/NBrightBuy", "config", Utils.GetCurrentCulture(), StoreSettings.Current.Settings());
                var lit = new Literal();
                lit.Text = strOut;
                phData.Controls.Add(lit);

            }

        }

        #endregion

    }

}
