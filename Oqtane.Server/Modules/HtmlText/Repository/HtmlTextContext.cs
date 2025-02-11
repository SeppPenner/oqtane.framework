﻿using Microsoft.EntityFrameworkCore;
using Oqtane.Shared.Modules.HtmlText.Models;
using Oqtane.Repository;
using Oqtane.Modules;
using Microsoft.AspNetCore.Http;

namespace Oqtane.Server.Modules.HtmlText.Repository
{
    public class HtmlTextContext : DBContextBase, IService
    {
        public virtual DbSet<HtmlTextInfo> HtmlText { get; set; }

        public HtmlTextContext(ITenantResolver TenantResolver, IHttpContextAccessor accessor) : base(TenantResolver, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}
