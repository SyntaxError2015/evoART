﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace evoART.Special
{
    public class CookieHelper
    {
        /// <summary>
        /// Add or replace a cookie
        /// </summary>
        /// <param name="name">The cookie name</param>
        /// <param name="value">The cookie value</param>
        /// <param name="expires">When it expires</param>
        public void SetCookie(string name, string value, DateTime expires)
        {
            var cookie = new HttpCookie(name)
            {
                Value = value,
                Expires = expires
            };
            HttpContext.Current.Response.SetCookie(cookie);
        }

        /// <summary>
        /// Delete a cookie
        /// </summary>
        /// <param name="name">The name of the cookie to be deleted</param>
        public void DeleteCookie(string name)
        {
            var cookie = new HttpCookie(name)
            {
                Domain = "/",
                Value = null,
                Expires = DateTime.Now.AddDays(-1)
            };
            HttpContext.Current.Response.SetCookie(cookie);
        }

        /// <summary>
        /// Get the cookie value
        /// </summary>
        /// <param name="name">The cookie name</param>
        /// <returns>The cookie's value</returns>
        public string GetCookie(string name)
        {

            return HttpContext.Current.Request.Cookies[name] != null ? HttpContext.Current.Request.Cookies[name].Value : "";
        }
    }
}