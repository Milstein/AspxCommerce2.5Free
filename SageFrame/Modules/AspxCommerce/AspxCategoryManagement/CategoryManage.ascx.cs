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
using System.Web.UI;
using SageFrame.Web;
using AspxCommerce.Core;
using SageFrame.Core;

public partial class Modules_AspxCategoryManagement_CategoryManage : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName;
    public int CategoryLargeThumbImageHeight,CategoryLargeThumbImageWidth, CategoryMediumThumbImageHeight,CategoryMediumThumbImageWidth, CategorySmallThumbImageHeight,CategorySmallThumbImageWidth, BannerWidth = 800, BannerHeight = 250;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IncludeCss("CategoryManage", "/Templates/" + TemplateName + "/css/GridView/tablesort.css",
                           "/Templates/" + TemplateName + "/css/TreeView/ui.tree.css",
                           "/Templates/" + TemplateName + "/css/MessageBox/style.css",
                          
                           "/Templates/" + TemplateName + "/css/DateTime/jquery.ptTimeSelect.css",
                           "/Templates/" + TemplateName + "/css/jCrop/jCrop.css",				
						   "/Modules/AspxCommerce/AspxCategoryManagement/css/module.css");
                IncludeJs("CategoryManage", "/js/GridView/jquery.grid.js",
                          "/js/GridView/SagePaging.js",
                          "/js/GridView/jquery.global.js", "/js/GridView/jquery.dateFormat.js",
                           "/js/TreeView/ui.tree.js",
                          "/js/TreeView/jquery.json-2.2.js", "/js/DateTime/date.js",
                          "/js/MessageBox/jquery.easing.1.3.js", "/js/MessageBox/alertbox.js",
                          "/js/AjaxFileUploader/ajaxupload.js", "/js/FormValidation/jquery.ui.datepicker.validation.js",
                          "/js/FormValidation/jquery.validate.js",
                          "/Modules/AspxCommerce/AspxCategoryManagement/js/CategoryManage.js",
                          "/js/CurrencyFormat/jquery.formatCurrency-1.4.0.js",
                          "/js/PopUp/custom.js", "/js/CurrencyFormat/jquery.formatCurrency.all.js",
                          "/js/DateTime/jquery.ptTimeSelect.js", "/js/jCrop/jquery.Jcrop.js");
               
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                StoreSettingConfig ssc = new StoreSettingConfig();
                CategoryLargeThumbImageHeight =
                    int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.CategoryLargeThumbnailImageHeight, StoreID, PortalID,
                                                        CultureName));
                CategoryLargeThumbImageWidth =
                   int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.CategoryLargeThumbnailImageWidth, StoreID, PortalID,
                                                       CultureName));
                CategoryMediumThumbImageHeight =
                    int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.CategoryMediumThumbnailImageHeight, StoreID, PortalID,
                                                        CultureName));
                CategoryMediumThumbImageWidth =
                    int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.CategoryMediumThumbnailImageWidth, StoreID, PortalID,
                                                        CultureName));
                CategorySmallThumbImageHeight =
                    int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.CategorySmallThumbnailImageHeight, StoreID, PortalID,
                                                        CultureName));
                CategorySmallThumbImageWidth =
                   int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.CategorySmallThumbnailImageWidth, StoreID, PortalID,
                                                       CultureName));
                
                 BannerWidth = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.CategoryBannerImageWidth, StoreID, PortalID, CultureName));
                 BannerHeight = int.Parse(ssc.GetStoreSettingsByKey(StoreSetting.CategoryBannerImageHeight, StoreID, PortalID, CultureName));

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ckEditorUserModuleID", " var ckEditorUserModuleID='" + SageUserModuleID + "';", true);
            IncludeJs("CategoryManageCk", "/Editors/ckeditor/ckeditor.js", "/Editors/ckeditor/adapters/jquery.js");
            IncludeLanguageJS();
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            
            string modulePath = ResolveUrl(this.AppRelativeTemplateSourceDirectory);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "globalVariables", " var aspxCatModulePath='" + ResolveUrl(modulePath) + "';", true);
            InitializeJS();     
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }

    private void InitializeJS()
    {
        Page.ClientScript.RegisterClientScriptInclude("JTablesorter", ResolveUrl("~/js/GridView/jquery.tablesorter.js"));
        Page.ClientScript.RegisterClientScriptInclude("JQueryFormValidate", ResolveUrl("~/js/FormValidation/jquery.form-validation-and-hints.js"));       
        Page.ClientScript.RegisterClientScriptInclude("J12", ResolveUrl("~/js/encoder.js"));
    }
}
