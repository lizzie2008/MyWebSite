using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace MyWebSite.Extensions
{
    public static class EmumExtensions
    {
        public static SelectList GetSelectListByEnum<TEnum>(this TEnum enumObj, int? selectedItem = null)
        {
            if (Enum.GetValues(typeof(TEnum)).Length > 0)
            {
                List<SelectListItem> listResult = new List<SelectListItem>();
                foreach (TEnum e in Enum.GetValues(typeof(TEnum)))
                {
                    if (selectedItem != null && selectedItem == Convert.ToInt32(e)) // 选中
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = Convert.ToInt32(e).ToString(),    // 传输值
                            Text = e.ToString(),      // 显示值
                            Selected = true
                        };
                        listResult.Add(item);
                    }
                    else
                    {
                        SelectListItem item = new SelectListItem     // 不选中
                        {
                            Value = Convert.ToInt32(e).ToString(),     // 传输值
                            Text = e.ToString()      // 显示值
                        };
                        listResult.Add(item);
                    }
                }
                if (selectedItem != null)
                    return new SelectList(listResult, "Value", "Text", selectedItem);
                else
                    return new SelectList(listResult, "Value", "Text");
            }
            return null;
        }
    }
}
