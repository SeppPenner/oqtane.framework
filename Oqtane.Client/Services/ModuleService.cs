﻿using Oqtane.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Oqtane.Shared;

namespace Oqtane.Services
{
    public class ModuleService : ServiceBase, IModuleService
    {
        private readonly HttpClient http;
        private readonly SiteState sitestate;
        private readonly IUriHelper urihelper;

        public ModuleService(HttpClient http, SiteState sitestate, IUriHelper urihelper)
        {
            this.http = http;
            this.sitestate = sitestate;
            this.urihelper = urihelper;
        }

        private string apiurl
        {
            get { return CreateApiUrl(sitestate.Alias, urihelper.GetAbsoluteUri(), "Module"); }
        }

        public async Task<List<Module>> GetModulesAsync(int PageId)
        {
            List<Module> modules = await http.GetJsonAsync<List<Module>>(apiurl + "?pageid=" + PageId.ToString());
            modules = modules
                .OrderBy(item => item.Order)
                .ToList();
            return modules;
        }

        public async Task<List<Module>> GetModulesAsync(int SiteId, string ModuleDefinitionName)
        {
            List<Module> modules = await http.GetJsonAsync<List<Module>>(apiurl + "?siteid=" + SiteId.ToString() + "&moduledefinitionname=" + ModuleDefinitionName);
            return modules.ToList();
        }

        public async Task<Module> GetModuleAsync(int ModuleId)
        {
            return await http.GetJsonAsync<Module>(apiurl + "/" + ModuleId.ToString());
        }

        public async Task AddModuleAsync(Module module)
        {
            await http.PostJsonAsync(apiurl, module);
        }

        public async Task UpdateModuleAsync(Module module)
        {
            await http.PutJsonAsync(apiurl + "/" + module.ModuleId.ToString(), module);
        }

        public async Task DeleteModuleAsync(int ModuleId)
        {
            await http.DeleteAsync(apiurl + "/" + ModuleId.ToString());
        }
    }
}
