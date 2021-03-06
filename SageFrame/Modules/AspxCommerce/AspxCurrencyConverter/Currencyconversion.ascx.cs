﻿/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2014 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using AspxCommerce.Core;
using SageFrame.Web;
using SageFrame.Core;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;


public partial class Modules_AspxCommerce_AspxCurrencyConverter_Currencyconversion : BaseAdministrationUserControl
{
    public string MainCurrency = "";
    public string SelectedCurrency = "";
    public string Region;
    public int StoreID, PortalID;
    public string CultureName;
    public double currencyRate = 1;


    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
            aspxCommonObj.StoreID = GetStoreID;
            aspxCommonObj.PortalID = GetPortalID;
            aspxCommonObj.UserName = GetUsername;
            aspxCommonObj.CultureName = GetCurrentCultureName;
            if (!IsPostBack)
            {

                StoreSettingConfig ssc = new StoreSettingConfig();
                if (Session["CurrencyCode"] != null && Session["CurrencyCode"] != "")
                {
                    SelectedCurrency = Session["CurrencyCode"].ToString();
                }
                else
                {
                    SelectedCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, GetStoreID, GetPortalID, GetCurrentCultureName);
                }
                IncludeJs("Currencyconversion", "/js/FancyDropDown/fancyDropDown.js");//
                IncludeCss("Currencyconversion", "/Templates/" + TemplateName + "/css/FancyDropDown/fancy.css");
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                CultureName = GetCurrentCultureName;
                MainCurrency = ssc.GetStoreSettingsByKey(StoreSetting.MainCurrency, StoreID, PortalID, CultureName);
                if (Session["Region"] != null && Session["Region"] != "")
                {
                    Region = Session["Region"].ToString();
                }
                else
                {
                    Region = StoreSetting.GetRegionFromCurrencyCode(SelectedCurrency, StoreID, PortalID);
                }            
                AspxCoreController acc = new AspxCoreController();
                acc.GetCurrencyRateOnChange(aspxCommonObj, MainCurrency, SelectedCurrency, Region);
                if (Session["CurrencyRate"] != null && Session["CurrencyRate"] != "")
                {
                    currencyRate=Math.Round(double.Parse(Session["CurrencyRate"].ToString()),4);
                }
            }
            IncludeLanguageJS();
            BindCurrencyList();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
    public void BindCurrencyList()
    {
        AspxCommonInfo aspxCommonObj = new AspxCommonInfo();
        aspxCommonObj.StoreID = GetStoreID;
        aspxCommonObj.PortalID = GetPortalID;
        aspxCommonObj.UserName = GetUsername;
        aspxCommonObj.CultureName = GetCurrentCultureName;
        string aspxRootPath = ResolveUrl("~/");
        List<CurrencyInfo> lstCurrency = AspxCurrencyController.BindCurrencyList(aspxCommonObj);
        if (lstCurrency != null && lstCurrency.Count > 0)
        {
            StringBuilder options = new StringBuilder();
            options.Append("<select id=\"ddlCurrency\" class=\"makeMeFancy\">");
           

            foreach (CurrencyInfo item in lstCurrency)
            {
                if (item.CurrencyCode == SelectedCurrency)
                {
                    options.Append("<option selected=\"selected\" data-icon=\"");
                    options.Append(aspxRootPath);
                    options.Append("images/flags/");
                    options.Append(item.BaseImage);
                    options.Append("\" data-img-text=\"");
                    options.Append(item.CurrencyName);
                    options.Append("\" data-html-text=\"");
                    options.Append(item.CurrencySymbol);
                    options.Append("-");
                    options.Append(item.CurrencyCode);
                    options.Append("\" region=");
                    options.Append(item.Region);
                    options.Append("  value=\"");
                    options.Append(item.CurrencyCode);
                    options.Append("\" >");
                    options.Append(item.CurrencySymbol);
                    options.Append("-");
                    options.Append(item.CurrencyCode);
                    options.Append("</option>");
                }
                else
                {
                    options.Append("<option data-icon=\"");
                    options.Append(aspxRootPath);
                    options.Append("images/flags/");
                    options.Append(item.BaseImage);
                    options.Append("\" data-img-text=\"");
                    options.Append(item.CurrencyName);
                    options.Append("\" data-html-text=\"");
                    options.Append(item.CurrencySymbol);
                    options.Append("-");
                    options.Append(item.CurrencyCode);
                    options.Append("\" region=");
                    options.Append(item.Region);

                    options.Append("  value=\"");
                    options.Append(item.CurrencyCode);
                    options.Append("\" >");
                    options.Append(item.CurrencySymbol);
                    options.Append("-");
                    options.Append(item.CurrencyCode);
                    options.Append("</option>");
                }
            }
            options.Append("</select>");
            litCurrency.Text = options.ToString();

        }
    }
}
